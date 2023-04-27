using IHSA_Backend.Constants;
using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public interface IAuthRequestHandler
    {
        public UserResponseModel MapUser(UserModel user);
        public Task<LoginResponseModel> AuthenticateAdminAsync(LoginRequestModel request);
        public Task<LoginResponseModel> AuthenticateAsync(LoginRequestModel request, Role role);
        public Task<LoginResponseModel> AuthenticateAsync(LoginRequestModel request);
        public Task<UserResponseModel> RegisterAsync(RegisterUserRequestModel request);
    }
}
