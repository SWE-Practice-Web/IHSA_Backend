using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using System.Collections;
using System.Reflection;

namespace IHSA_Backend.Collections
{
    public class RiderCollection : IRiderCollection
    {
        private readonly IAppSettings _appSettings;
        private readonly CollectionReference _collectionRef;
        private readonly IBaseCollection<RiderModel> _baseCollection;
        private IDictionary<int, RiderModel> _riderCache;

        public RiderCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _collectionRef = firestore.GetCollection(appSettings.RiderCollection);
            _baseCollection = new BaseCollection<RiderModel>(_collectionRef);

            _riderCache = new Dictionary<int, RiderModel>();
            foreach (var rider in GetAllAsync().Result)
            {
                if (_riderCache.ContainsKey(rider.RiderId))
                {
                    _riderCache.Remove(rider.RiderId);
                    DeleteAsync(rider.Id).Wait();
                }

                _riderCache.Add(rider.RiderId, rider);
            }
        }
        public Task<IEnumerable<RiderModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync();
        public Task<RiderModel?> GetAsync(int id) =>
            _baseCollection.GetAsync(id);
        public async Task<RiderModel> AddAsync(RiderModel entity)
        {
            var currentRider = GetByRiderIdCache(entity.RiderId);

            if (currentRider != null && !currentRider.Equals(default(RiderModel)))
                await _baseCollection.DeleteAsync(currentRider.Id);

            if (_riderCache.ContainsKey(entity.RiderId))
                _riderCache.Remove(entity.RiderId);

            _riderCache.Add(entity.RiderId, entity);

            return await _baseCollection.AddAsync(entity);
        }
        public async Task<IList<RiderModel>> AddBatchAsync(IList<RiderModel> entities)
        {
            foreach (var entity in entities)
            {
                if (_riderCache.ContainsKey(entity.RiderId))
                {
                    var currentRider = GetByRiderIdCache(entity.RiderId);

                    if (currentRider != null && !currentRider.Equals(default(RiderModel)))
                        await _baseCollection.DeleteAsync(currentRider.Id);
                    
                    _riderCache.Remove(entity.RiderId);
                }
            }

            var riders = await _baseCollection.AddBatchAsync(entities);

            foreach (var rider in riders)
            {
                if (_riderCache.ContainsKey(rider.RiderId))
                    _riderCache.Remove(rider.RiderId);

                _riderCache.Add(rider.RiderId, rider);
            }

            return riders;
        }
        public async Task<RiderModel> UpdateAsync(RiderModel entity)
        {
            var rider = await _baseCollection.UpdateAsync(entity);

            if (rider != null && !rider.Equals(default(RiderModel)) 
                && entity != null && !entity.Equals(default(RiderModel)))
            {
                if (entity.RiderId != rider.RiderId)
                    _riderCache.Remove(rider.RiderId);

                _riderCache[rider.RiderId] = rider;
            }

            return rider ?? new RiderModel();
        }
        public async Task<IList<RiderModel>> UpdateBatchAsync(IList<RiderModel> entites)
        {
            var riders = await _baseCollection.UpdateBatchAsync(entites);

            foreach (var rider in riders)
            {
                if (_riderCache.ContainsKey(rider.RiderId))
                    _riderCache.Remove(rider.RiderId);

                _riderCache.Add(rider.RiderId, rider);
            }

            return riders;
        }
        public async Task DeleteAsync(int id)
        {
            await _baseCollection.DeleteAsync(id);

            foreach (var entry in _riderCache)
            {
                if (entry.Value.Id == id)
                    _riderCache.Remove(entry.Key);
            }
        }
        public Task<bool> ExistsAsync(int id) =>
            _baseCollection.ExistsAsync(id);
        public RiderModel? GetByRiderIdCache(int riderId)
        {
            if (_riderCache.ContainsKey(riderId))
                return _riderCache[riderId];
            return default;
        }
    }
}
