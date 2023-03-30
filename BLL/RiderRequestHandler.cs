using AutoMapper;
using IHSA_Backend.Collections;
using IHSA_Backend.Helper;
using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public class RiderRequestHandler :
        RequestHandler<IRiderCollection, RiderRequestModel, RiderResponseModel, RiderModel>,
        IRiderRequestHandler
    {
        private readonly IRiderCollection _collection;
        private readonly IMapper _mapper;
        public RiderRequestHandler(
            IRiderCollection collection,
            IMapper mapper) : base(collection, mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }
    }
}
