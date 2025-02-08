using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR.Business.Abstract;
using SignalR.Core.Utilities.Result;
using SignalR.Entities.Concrete;
using SignalRWebUI.Models;
using SignalRWebUI.Services.Abstract.IdentityServices;

namespace SignalRWebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenCacheManagementService _tokenCacheManagementService;

        public LoginController(IIdentityService identityService, ITokenCacheManagementService tokenCacheManagementService)
        {
            _identityService = identityService;
            _tokenCacheManagementService = tokenCacheManagementService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string token = _tokenCacheManagementService.GetToken("SignalRCookie");

            if (token != null)
            {
                _tokenCacheManagementService.RemoveToken("SignalRCookie");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            var result = await _identityService.Login(loginViewModel.LoginProperty);

            if (!result)
            {
                ViewBag.InvalidUserPassword = "Kullanıcı Adı veya Şifre Hatalı!";
                return View();
            }

            return RedirectToAction("Index", "Redirect");
        }
    }
}
