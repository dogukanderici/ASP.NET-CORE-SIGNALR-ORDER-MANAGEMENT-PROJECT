using Microsoft.AspNetCore.Authorization;
using SignalR.Core.Utilities.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.Utilities.Handlers
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            bool IsRoleExists = false;
            if (context.User.Claims.Any(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" || c.Type == "role"))
            {
                IsRoleExists = true;
            }

            if (IsRoleExists)
            {
                if (context.User.Claims.Any(c => (c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" || c.Type == "role") && (c.Value == requirement.Role)))
                {
                    context.Succeed(requirement);
                }

            }
            else
            {
                if (context.User.Claims.Any(c => c.Type == "scope" && (c.Value == requirement.Role)))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
