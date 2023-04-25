using AutoMapper;
using IHSA_Backend.Collections;
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
    }
}
