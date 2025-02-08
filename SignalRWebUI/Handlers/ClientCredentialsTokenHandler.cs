using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SignalRWebUI.Services.Abstract.IdentityServices;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;

namespace SignalRWebUI.Handlers
{
    public class ClientCredentialsTokenHandler : DelegatingHandler
    {
        private readonly IClientCredentialsTokenService _clientCredentialsTokenService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ClientCredentialsTokenHandler(IClientCredentialsTokenService clientCredentialsTokenService, IHttpContextAccessor contextAccessor)
        {
            _clientCredentialsTokenService = clientCredentialsTokenService;
            _contextAccessor = contextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Token Handler Başladı");

            bool isAuthenticated = _contextAccessor.HttpContext.User?.Identity?.IsAuthenticated ?? false;

            if (!isAuthenticated)
            {
                string token = await _clientCredentialsTokenService.GetToken();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                // Kullanıcının sistemdeki access token'ını alır ve Header'a yazar.
                var accessToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                Debug.WriteLine($"Process Failed Due To: {response.StatusCode}-{response.Content}");

                string token = await _clientCredentialsTokenService.GetToken();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                response = await base.SendAsync(request, cancellationToken);

                string responseStringData = await response.Content.ReadAsStringAsync();
            }

            Debug.WriteLine($"Response Status: {response.StatusCode}");

            return response;
        }
    }
}
