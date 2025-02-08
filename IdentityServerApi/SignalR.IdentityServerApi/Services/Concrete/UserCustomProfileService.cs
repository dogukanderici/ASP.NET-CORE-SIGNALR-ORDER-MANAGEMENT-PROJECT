using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using SignalR.IdentityServerApi.Models;
using System.Security.Claims;

namespace SignalR.IdentityServerApi.Services.Concrete
{
    public class UserCustomProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
            if (user == null)
            {
                return;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(role => new Claim("role", role));

            var userİnfoClaims = new List<Claim>
            {
                new Claim("email",user.Email),
                new Claim("username",user.UserName),
                new Claim("namesurname",$"{user.Name} {user.Surname}")
            };

            context.IssuedClaims.AddRange(roleClaims);

            context.IssuedClaims.AddRange(userİnfoClaims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
            context.IsActive = user != null;
        }
    }
}
