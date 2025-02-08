using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SignalR.Dtos.Dtos.IdentityDtos;
using System.Net;
using System.Security.Claims;
using SignalRWebUI.Services.Abstract.IdentityServices;
using SignalRWebUI.Settings;
using SignalR.IdentityServerApi.Settings;
using Microsoft.Extensions.Options;

namespace SignalRWebUI.Services.Concrete.IdentityServices
{
    public class IdentityManager : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITokenCacheManagementService _tokenCacheManagementService;
        private readonly CustomUrlSettings _customUrlSettings;

        public IdentityManager(HttpClient httpClient, IHttpContextAccessor contextAccessor, ITokenCacheManagementService tokenCacheManagementService, IOptions<CustomUrlSettings> customUrlSettings)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
            _tokenCacheManagementService = tokenCacheManagementService;
            _customUrlSettings = customUrlSettings.Value;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            // IdentityServer'dan gerekli yetkilendirme ve kimlik doğrulama endpoint'lerini alır.

            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _customUrlSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });

            // IdentityServer'a Gönderilecek Token İsteği Oluşturur.
            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = "SignalRSubscribedCustomer",
                ClientSecret = "signalrsubscribedcustomersecret",
                UserName = loginDto.Username,
                Password = loginDto.Password,
                Address = discoveryEndpoint.TokenEndpoint
            };

            // Kullanıcı Adı / Şifre olmadan Genel Kullanıcı İçin Alınacak Token İsteği
            // IdentityServer'a Gönderilecek Token İsteği Oluşturur.
            //var passwordTokenRequest = new ClientCredentialsTokenRequest
            //{
            //    ClientId = "SignalRSubscribedCustomer",
            //    ClientSecret = "signalrsubscribedcustomer",
            //    Address = discoveryEndpoint.TokenEndpoint
            //};

            // IdentityServer'a Bir İstek Gönderir ve Token Alır.
            //var token = await _httpClient.RequestClientCredentialsTokenAsync(passwordTokenRequest);

            var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

            if (token.HttpStatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            // userinfo endpoint'ine Kullanıcı Bilgilerini Almak İçin İstek Oluşturulur.
            var userInforequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = discoveryEndpoint.UserInfoEndpoint
            };

            // Yukarıdaki İstek Nesnesi Kullanılarak IdentityServer'ın UserInfo Endpoint'ine istek atılır.
            var userValues = await _httpClient.GetUserInfoAsync(userInforequest);

            // Kullanıcı Bilgileri (userValues.Claims), Yetkilerini ve Rollerini Tarayıcıda Cookie'de Saklamak İçin ClaimIdentity Nesnesi Oluşturulur.
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userValues.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

            // Oluşturulan ClaimsIdentity Nesnesi Tarayıcıda Saklamak İçin ClaimsPrincipal Nesnesine Dönüştürülür.
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Token'ların Tarayıcıda Nasıl Saklanacağını Belirlemek İçin AuthenticationProperties Nesnesi Oluşturulur.
            var authenticationProperties = new AuthenticationProperties();

            authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value=token.AccessToken
                },
                new AuthenticationToken
                {
                    Name= OpenIdConnectParameterNames.RefreshToken,
                    Value=token.RefreshToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.Now.AddMinutes(token.ExpiresIn).ToString()
                }
            });

            // false Olarak Ayarlanırsa Token Bilgileri Tarayıcı Kapatıldığında Tarayıcıda Saklanmaz.
            authenticationProperties.IsPersistent = false;

            // claimsPrincipal ve authenticationProperties Kullanılarak Tarayıcıda Cookie Oluşturarak Kullacının Oturum Açmasını Sağlar.
            await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            _tokenCacheManagementService.SetToken("SignalRCookie", token.AccessToken, token.ExpiresIn);

            return true;

        }

        public async Task<bool> Logout()
        {
            await _contextAccessor.HttpContext.SignOutAsync();

            _tokenCacheManagementService.RemoveToken("SignalRCookie");

            return true;

        }

        public Task<bool> SignUp(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
