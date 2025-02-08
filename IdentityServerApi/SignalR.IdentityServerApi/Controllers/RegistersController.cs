using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR.IdentityServerApi.Dto;
using SignalR.IdentityServerApi.Models;

namespace SignalR.IdentityServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RegistersController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterDto userRegisterDto)
        {
            ApplicationUser valueFromDto = _mapper.Map<ApplicationUser>(userRegisterDto);

            var result = await _userManager.CreateAsync(valueFromDto, userRegisterDto.Password);

            ApplicationUser user = await _userManager.FindByNameAsync(valueFromDto.UserName);

            await _userManager.AddToRoleAsync(user, "ReadPermission");
            await _userManager.AddToRoleAsync(user, "WritePermission");

            if (result.Succeeded)
            {
                return Ok(new { success = true, message = "Yeni Kullanıcı Kaydı Başarılı!" });
            }

            return BadRequest(new { success = false, message = result });
        }
    }
}
