

using System.IdentityModel.Tokens.Jwt;

namespace Microservices.FluxoCaixa.API.Services
{
    public class Autorizacao : IAutorizacao
    {
        public string GetJWTTokenClaim(string? token, string claimName)
        {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == claimName)?.Value;
                return claimValue;
        }
    }
}
