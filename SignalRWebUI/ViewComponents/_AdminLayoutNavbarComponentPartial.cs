using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SignalRWebUI.Settings;

namespace SignalRWebUI.ViewComponents
{
    public class _AdminLayoutNavbarComponentPartial : ViewComponent
    {

        private readonly ApiSettings _apiSettings;
        private readonly IMemoryCache _memoryCache;

        public _AdminLayoutNavbarComponentPartial(IOptions<ApiSettings> apiSettings, IMemoryCache memoryCache)
        {
            _apiSettings = apiSettings.Value;
            _memoryCache = memoryCache;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.ApiBaseUrl = _apiSettings.ApiBaseUrl + "SignalRHub";
            ViewBag.UserAccessToken = _memoryCache.Get("SignalRCookie");

            string username = HttpContext.User.FindFirst("username")?.Value;
            string nameSurname = HttpContext.User.FindFirst("namesurname")?.Value;

            ViewBag.UserName = username;
            ViewBag.NameSurname = nameSurname;

            return View();
        }
    }
}
