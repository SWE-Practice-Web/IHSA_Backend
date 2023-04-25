using IHSA_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace IHSA_Backend.BLL
{
    public interface IUserRequestHandler : IBaseRequestHandler<UserRequestModel, UserResponseModel>
    {
        public Task<UserResponseModel> UpdateAsync(string username, UserRequestModel userRequest);
    }
}
