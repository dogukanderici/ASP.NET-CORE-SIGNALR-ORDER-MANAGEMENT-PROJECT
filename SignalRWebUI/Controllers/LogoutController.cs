using Microsoft.AspNetCore.Mvc;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract.IdentityServices;

namespace SignalRWebUI.Controllers
{
    public class LogoutController : Controller
    {

        private readonly IIdentityService _identityService;
        private readonly ITokenCacheManagementService _tokenCacheManagementService;

        public LogoutController(IIdentityService identityService, ITokenCacheManagementService tokenCacheManagementService)
        {
            _identityService = identityService;
            _tokenCacheManagementService = tokenCacheManagementService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string token = _tokenCacheManagementService.GetToken("SignalRCookie");

            if (token != null)
            {
                _tokenCacheManagementService.RemoveToken("SignalRCookie");
            }

            var result = await _identityService.Logout();

            if (!result)
            {
                ViewBag.InvalidProcess = "Çıkış Yapılırken Bir Hata Oluştu!";
                return View();
            }

            return RedirectToAction("Index", "Default");
        }
    }
}
