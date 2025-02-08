using Microsoft.AspNetCore.Identity;
using SignalR.Dtos.Dtos.IdentityDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class UserViewModel
    {
        public List<IdentityUser> SystemUsers { get; set; }
        public IdentityUser UpdateUser { get; set; }
    }
}
