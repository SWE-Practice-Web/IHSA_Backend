using AutoMapper;
using IHSA_Backend.Collections;
using IHSA_Backend.Helper;
using IHSA_Backend.Models;
using IHSA_Backend.Filters;
using IHSA_Backend.Constants;

namespace IHSA_Backend.BLL
{
    public class EventRequestHandler :
        RequestHandler<IEventCollection, EventRequestModel, EventResponseModel, EventModel>,
        IEventRequestHandler
    {
        private readonly IEventCollection _collection;
        private readonly IRiderCollection _riderCollection;
        private readonly IMapper _mapper;

        public EventRequestHandler(
            IEventCollection collection,
            IRiderCollection riderCollection,
            IMapper mapper) : base(collection, mapper)
        {
            _collection = collection;
            _riderCollection = riderCollection;
            _mapper = mapper;
        }
        private EventElementOrderModel PreHandle(EventElementOrderRequestModel request)
        {
            return _mapper.Map<EventElementOrderModel>(request);
        }

        public new async Task<EventResponseModel> Create(EventRequestModel request)
        {
            EventModel entity = PreHandle(request);

            await _collection.AddAsync(entity);

            var responseEntity = PostHandle(entity);

            responseEntity.EventOrder = responseEntity.EventOrder ?? new List<EventElementOrderResponseModel>();

            foreach (EventElementOrderResponseModel eventOrderElementResponse in responseEntity.EventOrder)
            {
                foreach (EventPairResponseModel pair in eventOrderElementResponse.Pairs ?? new List<EventPairResponseModel>())
                {
                    var rider = await _riderCollection.GetByRiderIdAsync(pair.RiderId);

                    if (rider != null && !rider.Equals(default(RiderModel)))
                    {
                        pair.RiderName = rider.FirstName + " " + rider.LastName;
                        pair.RiderSchool = rider.PlaysFor;
                    }
                }
            }

            return responseEntity;
        }
        public async Task<IList<EventElementOrderResponseModel>?> AddEventOrder(
            int id, EventElementOrderRequestModel request)
        {
            EventModel? entity = await _collection.GetAsync(id);

            if (entity == null)
                return default;

            entity.Id = id;

            EventElementOrderModel eventOrderElement = PreHandle(request);

            if (entity.EventOrder == null)
                entity.EventOrder = new List<EventElementOrderModel>();

            foreach (EventPairModel pair in eventOrderElement.Pairs ?? new List<EventPairModel>())
            {
                var rider = await _riderCollection.GetByRiderIdAsync(pair.RiderId);

                if (rider != null && rider.Equals(default(RiderModel)))
                    throw new APIExceptions.RiderIdNotFoundException(
                        StatusCodes.Status404NotFound, Constant.RiderIdNotFound);
            }

            var existingEventOrder = entity.EventOrder.FirstOrDefault(
                e => e.ShowClass == eventOrderElement.ShowClass && e.Section == eventOrderElement.Section);

            if (existingEventOrder != null)
                entity.EventOrder.Remove(existingEventOrder);

            entity.EventOrder.Add(eventOrderElement);

            await _collection.UpdateAsync(entity);

            var responseEntity = PostHandle(entity).EventOrder ?? new List<EventElementOrderResponseModel>();

            foreach (EventElementOrderResponseModel eventOrderElementResponse in responseEntity)
            {
                foreach (EventPairResponseModel pair in eventOrderElementResponse.Pairs ?? new List<EventPairResponseModel>())
                {
                    var rider = await _riderCollection.GetByRiderIdAsync(pair.RiderId);

                    if (rider != null && !rider.Equals(default(RiderModel)))
                    {
                        pair.RiderName = rider.FirstName + " " + rider.LastName;
                        pair.RiderSchool = rider.PlaysFor;
                    }
                }
            }

            return responseEntity;
        }
        public async Task<IList<EventElementOrderResponseModel>?> BatchAddEventOrder(
            int id, IList<EventElementOrderRequestModel> request)
        {
            EventModel? entity = await _collection.GetAsync(id);

            if (entity == null)
                return default;

            IList<EventElementOrderModel> eventOrderElements = new List<EventElementOrderModel>();

            foreach (EventElementOrderRequestModel eventOrderElement in request)
                eventOrderElements.Add(PreHandle(eventOrderElement));

            entity.Id = id;

            if (entity.EventOrder == null)
                entity.EventOrder = new List<EventElementOrderModel>();
            
            foreach (EventElementOrderModel eventOrderElement in eventOrderElements)
            {
                var existingEventOrder = entity.EventOrder.FirstOrDefault(
                    e => e.ShowClass == eventOrderElement.ShowClass && e.Section == eventOrderElement.Section);

                foreach (EventPairModel pair in eventOrderElement.Pairs ?? new List<EventPairModel>())
                {
                    var rider = await _riderCollection.GetByRiderIdAsync(pair.RiderId);

                    if (rider != null && rider.Equals(default(RiderModel)))
                        throw new APIExceptions.RiderIdNotFoundException(
                            StatusCodes.Status404NotFound, Constant.RiderIdNotFound);
                }

                if (existingEventOrder != null)
                    entity.EventOrder.Remove(existingEventOrder);
                
                entity.EventOrder.Add(eventOrderElement);
            }

            await _collection.UpdateAsync(entity);

            var responseEntity = PostHandle(entity).EventOrder ?? new List<EventElementOrderResponseModel>();

            foreach (EventElementOrderResponseModel eventOrderElement in responseEntity)
            {
                foreach (EventPairResponseModel pair in eventOrderElement.Pairs ?? new List<EventPairResponseModel>())
                {
                    var rider = await _riderCollection.GetByRiderIdAsync(pair.RiderId);

                    if (rider != null && !rider.Equals(default(RiderModel)))
                    {
                        pair.RiderName = rider.FirstName + " " + rider.LastName;
                        pair.RiderSchool = rider.PlaysFor;
                    }
                }
            }

            return responseEntity;
        }
    }
}
