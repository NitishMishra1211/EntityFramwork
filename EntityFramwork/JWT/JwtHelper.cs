using System.IdentityModel.Tokens.Jwt;

namespace web.JWT
{
    public class JwtHelper
    {
        public static JwtSecurityToken DecodeJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            return jwtToken;
        }

        public static string GetClaimValue(JwtSecurityToken token, string claimType)
        {
            return token?.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }
    }
}
