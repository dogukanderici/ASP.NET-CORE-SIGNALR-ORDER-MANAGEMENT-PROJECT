using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Authorize(Policy = "AdminPermissionPolicy")]
    [Area("Admin")]
    [Route("Admin/Dashboard")]
    public class DashboardController : AdminBaseController
    {
        private readonly ApiSettings _apiSettings;
        private readonly IMemoryCache _memoryCache;

        public DashboardController(IOptions<ApiSettings> apiSettings, IMemoryCache memoryCache)
        {
            _apiSettings = apiSettings.Value;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            ViewBag.ApiBaseUrl = _apiSettings.ApiBaseUrl + "SignalRHub";
            ViewBag.UserAccessToken = _memoryCache.Get("SignalRCookie");

            return View();
        }
    }
}
