using Microsoft.Extensions.Caching.Memory;
using SignalRWebUI.Services.Abstract.IdentityServices;

namespace SignalRWebUI.Services.Concrete.IdentityServices
{
    public class TokenCacheManagementService : ITokenCacheManagementService
    {
        private IHttpContextAccessor _contextAccessor;
        private readonly IMemoryCache _memoryCache;

        public TokenCacheManagementService(IMemoryCache memoryCache, IHttpContextAccessor contextAccessor)
        {
            _memoryCache = memoryCache;
            _contextAccessor = contextAccessor;
        }

        public string GetToken(string clientName)
        {
            _memoryCache.TryGetValue(clientName, out string? token);
            return token;
        }

        public void SetToken(string clientName, string token, int expires)
        {
            _memoryCache.Set(clientName, token, DateTime.Now.AddMinutes(expires));
        }
        public bool RemoveToken(string clientName)
        {
            _memoryCache.Remove(clientName);

            _contextAccessor.HttpContext.Response.Cookies.Delete(clientName, new CookieOptions
            {
                Path = "/", // Cookie'nin oluşturulduğu path
                Domain = "localhost" // Cookie'nin oluşturulduğu domain (opsiyonel)
            });

            return true;
        }
    }
}
