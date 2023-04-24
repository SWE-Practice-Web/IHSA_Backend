using IHSA_Backend.Models;

namespace IHSA_Backend.Helpers
{
    public interface IJWTUtils
    {
        public string GenerateToken(UserModel user);
        public int? ValidateToken(string token);
    }
}
