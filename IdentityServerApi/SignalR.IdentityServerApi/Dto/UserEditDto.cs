using SignalR.IdentityServerApi.Models;

namespace SignalR.IdentityServerApi.Dto
{
    public class UserEditDto
    {
        public ApplicationUser UserData { get; set; }
        public string EdittedPassword { get; set; }
    }
}
