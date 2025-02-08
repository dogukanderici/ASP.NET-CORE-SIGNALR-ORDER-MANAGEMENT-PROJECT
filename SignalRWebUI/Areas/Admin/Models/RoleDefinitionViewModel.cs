using Microsoft.AspNetCore.Identity;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class RoleDefinitionViewModel : GenericViewModel
    {
        public List<IdentityRole> RoleDefinitionList { get; set; }
        public IdentityRole RoleDefinition { get; set; }
        public IdentityRole CreateRoleDefinition { get; set; }
        public IdentityRole UpdateRoleDefinition { get; set; }
    }
}
