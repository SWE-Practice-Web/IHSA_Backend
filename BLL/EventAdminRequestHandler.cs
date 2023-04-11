using AutoMapper;
using IHSA_Backend.Collections;
using IHSA_Backend.Helper;
using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public class EventAdminRequestHandler :
        RequestHandler<IEventAdminCollection, EventAdminRequestModel, EventAdminResponseModel, EventAdminModel>,
        IEventAdminRequestHandler
    {
        private readonly IEventAdminCollection _collection;
        private readonly IMapper _mapper;
        public EventAdminRequestHandler(
            IEventAdminCollection collection,
            IMapper mapper) : base(collection, mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }
    }
}
