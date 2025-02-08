using AutoMapper;
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
    public class LoginsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public LoginsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginDto userLoginDto)
        {
            var userCheck = await _userManager.FindByNameAsync(userLoginDto.Username);

            if (userCheck == null)
            {
                return NotFound(new { success = false, message = "Kullanıcı Bulunamadı!" });
            }

            var result = await _signInManager.PasswordSignInAsync(userLoginDto.Username, userLoginDto.Password, false, false);

            if (result.Succeeded)
            {
                return Ok(new { success = true, message = "Kullanıcı Girişi Başarılı" });
            }

            return BadRequest(new { success = false, message = "Kullanıcı Adı / Şifre Hatalı!" });
        }
    }
}
