using SignalR.IdentityServerApi.Models;
using SignalRWebUI.Dtos.IdentityDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract.IdentityServices
{
    public interface IUserProfileChangeServicePS
    {
        Task<IHttpResponseMessageSettings<ApplicationUser>> GetUserInfoAsync(string userid);
        Task<IHttpResponseMessageSettings<ApplicationUser>> ChangeUserInfoAsync(UserEditDto userEditDto);
        Task<IHttpResponseMessageSettings<ApplicationUser>> ChangeUserPasswordAsync(UserEditDto userEditDto);
    }
}
