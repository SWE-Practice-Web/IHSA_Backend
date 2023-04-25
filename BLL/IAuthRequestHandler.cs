using IHSA_Backend.Constants;
using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public interface IAuthRequestHandler
    {
        public Task<LoginResponseModel> AuthenticateAdminAsync(LoginRequestModel request, Role role);
        public Task<LoginResponseModel> AuthenticateAsync(LoginRequestModel request, Role role);
    }
}
