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
        private readonly IUserCollection _userCollection;
        private readonly IMapper _mapper;
        private IJWTUtils _jwtUtils;

        public AuthRequestHandler(
            IAdminCollection adminCollection,
            IUserCollection userCollection,

            IMapper mapper,
            IJWTUtils jwtUtils)
        {
            _adminCollection = adminCollection;
            _userCollection = userCollection;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }
        public UserResponseModel MapUser(UserModel user)
        {
            return _mapper.Map<UserResponseModel>(user);
        }
        public async Task<LoginResponseModel> AuthenticateAdminAsync(LoginRequestModel request)
        {
            if (request.Username == null)
                throw new APIExceptions.HttpResponseException(StatusCodes.Status400BadRequest);

            var user = await _adminCollection.GetByUsernameAsync(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new APIExceptions.HttpResponseException(StatusCodes.Status400BadRequest);

            var userResponse = (AuthUserResponseBaseModel)_mapper.Map<AdminResponseModel>(user);
            var userAuth = (AuthUserBaseModel)user;

            userResponse.Role = Role.Admin;
            userAuth.Role = Role.Admin;

            var response = new LoginResponseModel
            {
                Token = _jwtUtils.GenerateToken(userAuth),
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
                return await AuthenticateAdminAsync(request);
            else
                throw new APIExceptions.HttpResponseException(
                    StatusCodes.Status400BadRequest, Constant.InvalidRole);
        }
        public async Task<LoginResponseModel> AuthenticateAsync(LoginRequestModel request)
        {
            if (request.Username == null || request.Username.Length < 3)
                throw new APIExceptions.UsernamePolicyException(
                    StatusCodes.Status400BadRequest, Constant.UsernamePolicy);

            if (request.Password == null || request.Password.Length < 8)
                throw new APIExceptions.PasswordPolicyException(
                    StatusCodes.Status400BadRequest, Constant.PasswordPolicy);

            var user = await _userCollection.GetByUsernameAsync(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new APIExceptions.HttpResponseException(StatusCodes.Status400BadRequest);

            var userResponse = (AuthUserResponseBaseModel)_mapper.Map<UserResponseModel>(user);
            var userAuth = (AuthUserBaseModel)user;

            userResponse.Role = user.Role;
            userAuth.Role = user.Role;

            var response = new LoginResponseModel
            {
                Token = _jwtUtils.GenerateToken(userAuth),
                User = userResponse
            };

            return response;
        }
        public async Task<UserResponseModel > RegisterAsync(UserRequestModel request)
        {
            if (request.Username == null || request.Username.Length < 3)
                throw new APIExceptions.UsernamePolicyException(
                    StatusCodes.Status400BadRequest, Constant.UsernamePolicy);

            if (request.Password == null || request.Password.Length < 8)
                throw new APIExceptions.PasswordPolicyException(
                    StatusCodes.Status400BadRequest, Constant.PasswordPolicy);

            var user = await _userCollection.GetByUsernameAsync(request.Username);

            if (!(user == null || user.Equals(default(UserModel))))
                throw new APIExceptions.HttpResponseException(StatusCodes.Status400BadRequest);

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var userModel = _mapper.Map<UserModel>(request);
            userModel.Role = Role.Default;

            var userDb = await _userCollection.AddAsync(userModel);

            return _mapper.Map<UserResponseModel>(userDb);
        }
    }
}
