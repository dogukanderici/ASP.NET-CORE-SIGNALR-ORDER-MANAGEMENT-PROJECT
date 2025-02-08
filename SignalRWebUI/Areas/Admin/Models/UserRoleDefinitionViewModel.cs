using Microsoft.AspNetCore.Identity;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class UserRoleDefinitionViewModel : GenericViewModel
    {
        public List<IdentityUser> UserDataList { get; set; }
        public IdentityUser UserData { get; set; }
        public List<string> UserRoleData { get; set; }

        public UserViewModel UserViewModelData { get; set; }
    }
}
