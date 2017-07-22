using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CrossCutting.Identity.Authorization
{
   public class CustomJwtSecurityToken : ICustomJwtSecurityToken
    {
        public IConfigurationRoot Config { get; private set; }
        private  IConfigurationSection Section { get; set; }

        public CustomJwtSecurityToken()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Config = builder.Build();
             
            Section = Config.GetSection("Token");

        }

        public async Task<JwtSecurityToken> GerarToken(List<Claim> claims)
        {
            var token = new JwtSecurityToken(issuer:Section["Audience"],audience:Section["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(Section["ValidadeEmMinutos"])),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Section["secretKey"])), SecurityAlgorithms.HmacSha256)

            );

            return await Task.FromResult(token);
        }
    }
}
