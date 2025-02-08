using Microsoft.AspNetCore.Mvc;
using SignalRWebUI.Dtos.IdentityDtos;
using SignalRWebUI.Services.Abstract.IdentityServices;
using System.Security.Claims;

namespace SignalRWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProfileSetting")]
    public class ProfileSettingController : AdminBaseController
    {
        private readonly IUserProfileChangeServicePS _userProfileChangeServicePS;

        public ProfileSettingController(IUserProfileChangeServicePS userProfileChangeServicePS)
        {
            _userProfileChangeServicePS = userProfileChangeServicePS;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.MainTitle = "Kullanıcı Bilgileri Güncelleme";

            UserEditDto model = new UserEditDto();

            var userID = User?.FindFirstValue("sub");

            var values = await _userProfileChangeServicePS.GetUserInfoAsync(userID);

            model.UserData = values.GetData;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserEditDto userEditDto)
        {
            var values = await _userProfileChangeServicePS.ChangeUserInfoAsync(userEditDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            ViewBag.MainTitle = "Şifre Güncelleme";

            UserEditDto model = new UserEditDto();

            return View(model);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(UserEditDto userEditDto)
        {

            var userID = User?.FindFirstValue("sub");

            var responseUserData = await _userProfileChangeServicePS.GetUserInfoAsync(userID);

            userEditDto.UserData = responseUserData.GetData;

            var values = await _userProfileChangeServicePS.ChangeUserPasswordAsync(userEditDto);

            return RedirectToAction("Index");
        }
    }
}
