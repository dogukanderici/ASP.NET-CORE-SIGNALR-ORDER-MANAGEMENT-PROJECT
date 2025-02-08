using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR.IdentityServerApi.Dto;
using SignalR.IdentityServerApi.Models;
using static Duende.IdentityServer.IdentityServerConstants;

namespace SignalR.IdentityServerApi.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SettingsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo(string userID)
        {
            ApplicationUser userInfo = await GetUser(userID);

            if (userInfo == null)
            {
                return BadRequest("Kullanıcı Bulunamadı!");
            }

            return Ok(userInfo);
        }

        [HttpPost]
        [Route("EditPersonalInfo")]
        public async Task<IActionResult> EditPersonalInfo(UserEditDto userEditDto)
        {
            ApplicationUser userInfo = await GetUser(userEditDto.UserData.Id);

            if (userInfo == null)
            {
                return NotFound("Kullanıcı Bulunamadı!");
            }

            userInfo.Name = userEditDto.UserData.Name;
            userInfo.Surname = userEditDto.UserData.Surname;
            userInfo.UserName = userEditDto.UserData.UserName;

            var result = await _userManager.UpdateAsync(userInfo);

            return Ok("Kullanıcı Bilgileri Güncellendi.");
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(UserEditDto userEditDto)
        {
            ApplicationUser userInfo = await GetUser(userEditDto.UserData.Id);

            if (userInfo == null)
            {
                return BadRequest("Kullanıcı Bulunamadı!");
            }

            userInfo.PasswordHash = _userManager.PasswordHasher.HashPassword(userInfo, userEditDto.EdittedPassword);

            var result = await _userManager.UpdateAsync(userInfo);

            return Ok("Kullanıcı Şifresi Güncellendi.");
        }

        private async Task<ApplicationUser> GetUser(string id)
        {
            ApplicationUser userData = await _userManager.FindByIdAsync(id);

            return userData;
        }
    }
}
