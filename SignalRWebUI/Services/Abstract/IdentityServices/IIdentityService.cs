using SignalR.Dtos.Dtos.IdentityDtos;

namespace SignalRWebUI.Services.Abstract.IdentityServices
{
    public interface IIdentityService
    {
        Task<bool> Login(LoginDto loginDto);
        Task<bool> Logout();
        Task<bool> SignUp(RegisterDto registerDto);
    }
}
