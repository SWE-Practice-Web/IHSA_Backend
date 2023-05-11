using IHSA_Backend.Models;

namespace IHSA_Backend.Helpers
{
    public interface IJWTUtils
    {
        public string GenerateToken(AuthUserBaseModel user);
        public TokenModel? ValidateToken(string token);
    }
}
