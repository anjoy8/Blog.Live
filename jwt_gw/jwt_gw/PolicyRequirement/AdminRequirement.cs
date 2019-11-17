using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt_gw.PolicyRequirement
{
    public class AdminRequirement:IAuthorizationRequirement
    {
        public string Name { get; set; }
    }
}
