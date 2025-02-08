using Microsoft.AspNetCore.Identity;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract.IdentityServices
{
    public interface IRoleDefinitionServicePS
    {
        Task<IHttpResponseMessageSettings<List<IdentityRole>>> GetAllRoleListAsync();
        Task<IHttpResponseMessageSettings<string>> AddNewRole(string newRoleName);
    }
}
