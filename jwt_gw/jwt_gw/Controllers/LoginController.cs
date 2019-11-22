using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace jwt_gw.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public string GetToken()
        {
            // 3 + 2
            SecurityToken securityToken = new JwtSecurityToken(
                issuer: "issuer",
                audience: "audience",
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("laozhanglaozhanglaozhanglaozhang")),SecurityAlgorithms.HmacSha256),


                expires:DateTime.Now.AddHours(1),
                claims:new Claim[] { 
                    new Claim("laozhang","laoli"),
                    new Claim(ClaimTypes.Role,"User"),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim(ClaimTypes.Email,"Admin@qq.com"),
                }
                );
            // eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJsYW96aGFuZyI6Imxhb2xpIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJBZG1pbkBxcS5jb20iLCJleHAiOjE1NzMwNTEyNjYsImlzcyI6Imlzc3VlciIsImF1ZCI6ImF1ZGllbmNlIn0.HMl5Z638HvYrdgK513oFBY0q7QaLUD2b_ESh3R-BXPM

            // session["laozhang"]="laoli";

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}