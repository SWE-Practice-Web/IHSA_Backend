using AutoMapper;
using BCrypt.Net;
using IHSA_Backend.Collections;
using IHSA_Backend.Helpers;
using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public class AuthRequestHandler : IAuthRequestHandler
    {
        private readonly IUserCollection _userCollection;
        private readonly IMapper _mapper;
        private IJWTUtils _jwtUtils;

        public AuthRequestHandler(
            IUserCollection userCollection,
            IMapper mapper,
            IJWTUtils jwtUtils)
        {
            _userCollection = userCollection;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }
        public async Task<LoginResponseModel?> AuthenticateAsync(LoginRequestModel request)
        {
            // if (request.UserName.Length < 8) error out
            // other password & username policy

            if (request.Username == null)
                return null;

            if (request.Password == null)
                return null;

            
            var user = await _userCollection.GetByUsernameAsync(request.Username);

            if (user == null || user.Equals(default(UserModel)))
                return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return null;

            var response = new LoginResponseModel
            {
                Token = _jwtUtils.GenerateToken(user),
                User = _mapper.Map<UserResponseModel>(user)
            };

            return response;
        }

        public async Task<UserResponseModel?> RegisterAsync(UserRequestModel request)
        {
            if (request.Username == null)
                return null;

            if (request.Password == null)
                return null;

            var user = await _userCollection.GetByUsernameAsync(request.Username);

            if (!(user == null || user.Equals(default(UserModel))))
                return null;

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var userModel = _mapper.Map<UserModel>(request);

            var userDb = await _userCollection.AddAsync(userModel);

            return _mapper.Map<UserResponseModel>(userDb);
        }
    }
}
