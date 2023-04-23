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
        public async Task DeleteByRiderId(int riderId)
        {
            var rider = _collection.GetByRiderIdCache(riderId);

            if (rider != null && !rider.Equals(default(RiderModel)))
                await _collection.DeleteAsync(rider.Id);
        }
        public RiderResponseModel? GetByRiderId(int riderId)
        {
            var rider = _collection.GetByRiderIdCache(riderId);

            if (rider == null || rider.Equals(default(RiderModel)))
                return default;

            return PostHandle(rider);
        }
        public async Task<RiderResponseModel?> UpdateByRiderId(int riderId, RiderRequestModel riderRequest)
        {
            var existingRider = _collection.GetByRiderIdCache(riderId);

            if (existingRider == null || existingRider.Equals(default(RiderModel)))
                return null;

            var rider = PreHandle(riderRequest);
            rider.Id = existingRider.Id;

            await _collection.UpdateAsync(rider);

            return PostHandle(rider);
        }
    }
}
