using Microsoft.AspNetCore.Identity;
using SignalR.Dtos.Dtos.IdentityDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public List<IdentityRole> SystemRoles { get; set; }
        public IdentityRole CreateRole { get; set; }
        public IdentityRole UpdateRole { get; set; }
    }
}
