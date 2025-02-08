using Microsoft.AspNetCore.Identity;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract.IdentityServices
{
    public interface IUserRoleDefinitonServicePS
    {
        Task<IHttpResponseMessageSettings<List<IdentityUser>>> GetAllUserAsync();
        Task<IHttpResponseMessageSettings<List<string>>> GetUserAsync(string id);
        Task<IHttpResponseMessageSettings<IdentityUser>> GetUserDataAsync(string id);
        Task<IHttpResponseMessageSettings<string>> CreateNewUserRole(string id, string newUserRole);
        Task<IHttpResponseMessageSettings<string>> DeleteUserRole(string id, string userRoleName);
    }
}
