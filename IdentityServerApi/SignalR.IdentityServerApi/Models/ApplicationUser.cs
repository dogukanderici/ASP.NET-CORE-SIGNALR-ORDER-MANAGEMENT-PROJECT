using Microsoft.AspNetCore.Identity;

namespace SignalR.IdentityServerApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
