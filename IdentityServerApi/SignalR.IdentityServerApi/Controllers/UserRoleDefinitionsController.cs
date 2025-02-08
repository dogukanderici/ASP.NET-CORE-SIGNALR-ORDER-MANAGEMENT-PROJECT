using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.IdentityServerApi.Models;

namespace SignalR.IdentityServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleDefinitionsController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleDefinitionsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<ApplicationUser> result = await _userManager.Users.ToListAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserDataById")]
        public async Task<IActionResult> GetUserDataById(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            return Ok(user);
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.GetRolesAsync(user);

            return Ok(result);
        }

        [HttpGet]
        [Route("AddNewRoleUser/{id}/{newUserRole}")]
        public async Task<IActionResult> AddNewRoleUser(string id, string newUserRole)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            bool userRoleCheck = await _userManager.IsInRoleAsync(user, newUserRole);

            if (!userRoleCheck)
            {
                IdentityResult result = await _userManager.AddToRoleAsync(user, newUserRole);

                return result.Succeeded ? Ok(new { StatusCode = 200, Message = result }) : BadRequest(new { StatusCode = 400, Message = result });
            }

            return Conflict(new { StatusCode = 409, Message = "Kullanıcı Eklenecek Rol Zaten Kayıtlı!" });
        }

        [HttpGet]
        [Route("DeleteRoleUser/{id}/{roleName}")]
        public async Task<IActionResult> DeleteUserRole(string id, string roleName)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            bool userRoleCheck = await _userManager.IsInRoleAsync(user, roleName);

            if (userRoleCheck)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);

                return result.Succeeded ? Ok(new { StatusCode = 200, Message = result }) : BadRequest(new { StatusCode = 400, Message = result });
            }

            return Conflict(new { StatusCode = 409, Message = "Kullanıcının Silinecek Role Kaydı Bulunamadı!" });
        }
    }
}
