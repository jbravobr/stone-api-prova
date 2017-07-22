using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrossCutting.Identity.Authorization
{
    public interface ICustomJwtSecurityToken
    {
       Task< JwtSecurityToken> GerarToken(List<Claim> claims);
    }
}