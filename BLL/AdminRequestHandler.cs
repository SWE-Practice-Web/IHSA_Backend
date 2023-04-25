using AutoMapper;
using IHSA_Backend.Collections;
using IHSA_Backend.Constants;
using IHSA_Backend.Filters;
using IHSA_Backend.Helper;
using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public class AdminRequestHandler : 
        RequestHandler<IAdminCollection, AdminRequestModel, AdminResponseModel, AdminModel>,
        IAdminRequestHandler
    {
        private readonly IAdminCollection _collection;
        private readonly IMapper _mapper;
        public AdminRequestHandler(
            IAdminCollection collection,
            IMapper mapper) : base(collection, mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }
        public AdminRequestModel VerifyAdminRequest(AdminRequestModel request)
        {
            if (request.Username == null || request.Username.Length < 3)
                throw new APIExceptions.UsernamePolicyException(
                    StatusCodes.Status400BadRequest, Constant.UsernamePolicy);

            if (request.Password == null || request.Password.Length < 8)
                throw new APIExceptions.PasswordPolicyException(
                    StatusCodes.Status400BadRequest, Constant.PasswordPolicy);

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            return request;
        }
    }
}
