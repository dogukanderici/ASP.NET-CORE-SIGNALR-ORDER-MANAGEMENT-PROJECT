using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Dtos.Dtos.IdentityDtos
{
    public class AppUserDto : IdentityUser<int>
    {
        public string UserRoleName { get; set; }
    }
}
