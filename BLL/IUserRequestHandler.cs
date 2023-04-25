using IHSA_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace IHSA_Backend.BLL
{
    public interface IUserRequestHandler : IBaseRequestHandler<UserRequestModel, UserResponseModel>
    {
        public Task<UserResponseModel> Update(string username, UserRequestModel userRequest);
        public new Task<UserResponseModel?> Update(int id, UserRequestModel userRequest);
        public Task DeleteByUsername(string username);
    }
}
