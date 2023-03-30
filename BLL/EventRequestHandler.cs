using AutoMapper;
using IHSA_Backend.Collections;
using IHSA_Backend.Helper;
using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public class EventRequestHandler :
        RequestHandler<IEventCollection, EventRequestModel, EventResponseModel, EventModel>,
        IEventRequestHandler
    {
        private readonly IEventCollection _collection;
        private readonly IMapper _mapper;

        public EventRequestHandler(
            IEventCollection collection,
            IMapper mapper) : base(collection, mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }
    }
}
