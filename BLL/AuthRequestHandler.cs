using AutoMapper;
using BCrypt.Net;
using IHSA_Backend.Collections;
using IHSA_Backend.Constants;
using IHSA_Backend.Filters;
using IHSA_Backend.Helpers;
using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public class AuthRequestHandler : IAuthRequestHandler
    {
        private readonly IAdminCollection _adminCollection;
        private readonly IMapper _mapper;
        private IJWTUtils _jwtUtils;

        public AuthRequestHandler(
            IAdminCollection adminCollection,
            IMapper mapper,
            IJWTUtils jwtUtils)
        {
            _adminCollection = adminCollection;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }
        public async Task<LoginResponseModel> AuthenticateAdminAsync(LoginRequestModel request, Role role)
        {
            if (request.Username == null)
                throw new APIExceptions.HttpResponseException(StatusCodes.Status400BadRequest);

            var user = await _adminCollection.GetByUsernameAsync(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new APIExceptions.HttpResponseException(StatusCodes.Status400BadRequest);

            var userResponse = (AuthUserResponseModel)_mapper.Map<AdminResponseModel>(user);

            userResponse.Role = Role.Admin;

            var response = new LoginResponseModel
            {
                Token = _jwtUtils.GenerateToken(user),
                User = userResponse
            };

            return response;
        }
        public async Task<LoginResponseModel> AuthenticateAsync(LoginRequestModel request, Role role)
        {
            if (request.Username == null || request.Username.Length < 3)
                throw new APIExceptions.UsernamePolicyException(
                    StatusCodes.Status400BadRequest, Constant.UsernamePolicy);

            if (request.Password == null || request.Password.Length < 8)
                throw new APIExceptions.PasswordPolicyException(
                    StatusCodes.Status400BadRequest, Constant.PasswordPolicy);
            
            if (role == Role.Admin)
                return await AuthenticateAdminAsync(request, role);
            else
                throw new APIExceptions.HttpResponseException(
                    StatusCodes.Status400BadRequest, Constant.InvalidRole);
        }
        /*public async Task<UserResponseModel?> RegisterAsync(UserRequestModel request)
        {
            if (request.Username == null || request.Username.Length < 8)
                throw new APIExceptions.UsernamePolicyException(
                    StatusCodes.Status400BadRequest, Constant.UsernamePolicy);

            if (request.Password == null || request.Password.Length < 8)
                throw new APIExceptions.PasswordPolicyException(
                    StatusCodes.Status400BadRequest, Constant.PasswordPolicy);

            var user = await _userCollection.GetByUsernameAsync(request.Username);

            if (!(user == null || user.Equals(default(UserModel))))
                return null;

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var userModel = _mapper.Map<UserModel>(request);

            var userDb = await _userCollection.AddAsync(userModel);

            return _mapper.Map<UserResponseModel>(userDb);
        }*/
    }
}
