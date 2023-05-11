using IHSA_Backend.Constants;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IHSA_Backend.Helpers
{
    public class JWTUtils : IJWTUtils
    {
        private readonly IAppSettings _appSettings;

        public JWTUtils(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public string GenerateToken(AuthUserBaseModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = System.DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public TokenModel? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
                var roleClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "role")?.Value;

                if (userIdClaim != null && roleClaim != null)
                    return new TokenModel
                    {
                        UserId = int.Parse(userIdClaim),
                        Role = (Role)Enum.Parse(typeof(Role), roleClaim)
                    };
                
                return null;
            }
            catch
            {
                return null;
            }
        }

    }
}