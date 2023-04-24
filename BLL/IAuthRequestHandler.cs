using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public interface IAuthRequestHandler
    {
        public Task<LoginResponseModel?> AuthenticateAsync(LoginRequestModel resquest);
        public Task<UserResponseModel?> RegisterAsync(UserRequestModel request);
    }
}
