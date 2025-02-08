using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.Utilities.Requirements
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Role { get; }
        public PermissionRequirement(string role)
        {
            Role = role;
        }
    }
}
