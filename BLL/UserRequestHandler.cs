using AutoMapper;
using IHSA_Backend.Collections;
using IHSA_Backend.Constants;
using IHSA_Backend.Filters;
using IHSA_Backend.Helper;
using IHSA_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using static Google.Rpc.Context.AttributeContext.Types;

namespace IHSA_Backend.BLL
{
    public class UserRequestHandler :
        RequestHandler<IUserCollection, UserRequestModel, UserResponseModel, UserModel>,
        IUserRequestHandler
    {
        private readonly IUserCollection _collection;
        private readonly IMapper _mapper;
        public UserRequestHandler(
            IUserCollection collection,
            IMapper mapper) : base(collection, mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public async Task<UserResponseModel> UpdateAsync(string username, UserRequestModel userRequest)
        {
            if (userRequest.Username == null || userRequest.Username.Length < 3)
                throw new APIExceptions.UsernamePolicyException(
                StatusCodes.Status400BadRequest, Constant.UsernamePolicy);
            
            var user = await _collection.GetByUsernameAsync(username);

            if (user == null)
                throw new APIExceptions.HttpResponseException(StatusCodes.Status400BadRequest);

            if (userRequest.Password == null)
                userRequest.Password = user.Password;
            else
            {
                if (userRequest.Password.Length < 8)
                    throw new APIExceptions.PasswordPolicyException(
                        StatusCodes.Status400BadRequest, Constant.PasswordPolicy);

                userRequest.Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);
            }

            var userModel = _mapper.Map<UserModel>(userRequest);

            await _collection.UpdateAsync(userModel);

            return _mapper.Map<UserResponseModel>(userModel);
        }
    }
}
