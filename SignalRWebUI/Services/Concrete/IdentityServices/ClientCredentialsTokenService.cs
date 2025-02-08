using IdentityModel.Client;
using Microsoft.Extensions.Options;
using SignalR.IdentityServerApi.Settings;
using SignalRWebUI.Services.Abstract.IdentityServices;
using System.Net;

namespace SignalRWebUI.Services.Concrete.IdentityServices
{
    public class ClientCredentialsTokenService : IClientCredentialsTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenCacheManagementService _tokenCacheManagementService;
        private readonly CustomUrlSettings _urlSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientCredentialsTokenService(HttpClient httpClient, ITokenCacheManagementService tokenCacheManagementService, IOptions<CustomUrlSettings> urlSettings, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _tokenCacheManagementService = tokenCacheManagementService;
            _urlSettings = urlSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetToken()
        {
            bool isAuthenticated = _httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated ?? false;

            string currentToken = _tokenCacheManagementService.GetToken("SignalRCookie");

            if (currentToken != null)
            {
                return currentToken;
            }

            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _urlSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });

            // Kullanıcı Adı / Şifre olmadan Genel Kullanıcı İçin Alınacak Token İsteği
            // IdentityServer'a Gönderilecek Token İsteği Oluşturur.
            var passwordTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = "SignalRCustomer",
                ClientSecret = "signalrcustomersecret",
                Address = discoveryEndpoint.TokenEndpoint
            };

            // IdentityServer'a Bir İstek Gönderir ve Token Alır.
            var token = await _httpClient.RequestClientCredentialsTokenAsync(passwordTokenRequest);

            if (token.HttpStatusCode != HttpStatusCode.OK)
            {
                return "false";
            }

            _tokenCacheManagementService.SetToken("SignalRCookie", token.AccessToken, token.ExpiresIn);

            return token.AccessToken;
        }
    }
}
