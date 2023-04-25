using AutoMapper;
using IHSA_Backend.Collections;
using IHSA_Backend.Helper;
using IHSA_Backend.Models;

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
    }
}
