using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SignalR.Business.Abstract;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProgressBars")]
    public class ProgressBarsController : AdminBaseController
    {
        private readonly ApiSettings _apiSettings;
        private readonly IMemoryCache _memoryCache;

        public ProgressBarsController(IOptions<ApiSettings> apiSettings, IMemoryCache memoryCache)
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
