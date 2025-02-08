using SignalR.IdentityServerApi.Models;

namespace SignalRWebUI.Dtos.IdentityDtos
{
    public class UserEditDto
    {
        public ApplicationUser UserData { get; set; }
        public string EdittedPassword { get; set; } = "";
    }
}
