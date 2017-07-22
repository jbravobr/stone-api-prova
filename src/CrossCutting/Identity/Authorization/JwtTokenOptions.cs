using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using  System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;

namespace CrossCutting.Identity.Authorization
{
    public class JwtTokenOptions : JwtBearerOptions
    {
        public IConfigurationRoot Config { get; private set; }
        public JwtTokenOptions(IConfigurationRoot config)
        {
             Config = config;
            var section = Config.GetSection("Token");
            Audience = section["Audience"];
            AutomaticAuthenticate = Convert.ToBoolean( section["AutomaticAuthenticate"]);
            TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section["secretKey"])),
                ValidateIssuer = Convert.ToBoolean(section["ValidateIssuer"]),
                ValidIssuer = section["ValidIssuer"]
            };

        }
    }
}