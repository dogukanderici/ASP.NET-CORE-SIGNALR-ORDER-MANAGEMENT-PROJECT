using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SignalR.IdentityServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleDefinitionsController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleDefinitionsController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoleList()
        {
            List<IdentityRole> result = await _roleManager.Roles.ToListAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("AddNewRole")]
        public async Task<IActionResult> AddNewRole(string newRoleName)
        {
            var checkRole = await _roleManager.RoleExistsAsync(newRoleName);

            if (!checkRole)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = newRoleName
                });

                return result.Succeeded ? Ok(new { StatusCode = 200, Message = result }) : BadRequest(new { StatusCode = 400, Message = result });
            }

            return Conflict(new { StatusCode = 409, Message = "Eklenmek İstenen Role Zaten Kayıtlı!" });
        }
    }
}
