using IHSA_Backend.Models;
using IHSA_Backend.Services;

namespace IHSA_Backend.Collections
{
    public class RiderCollection : IRiderCollection
    {
        private readonly IAppSettings _appSettings;
        private readonly IBaseCollection _baseCollection;
        public RiderCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _baseCollection = new BaseCollection(
                firestore, appSettings.RiderCollection);
        }
        public Task<IEnumerable<RiderModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync<RiderModel>();
        public Task<RiderModel?> GetAsync(RiderModel entity) =>
            _baseCollection.GetAsync<RiderModel>(entity);
        public Task<RiderModel> AddAsync(RiderModel entity) =>
            _baseCollection.AddAsync<RiderModel>(entity);
        public Task<RiderModel> UpdateAsync(RiderModel entity) =>
            _baseCollection.UpdateAsync<RiderModel>(entity);
        public Task DeleteAsync(RiderModel entity) =>
            _baseCollection.DeleteAsync<RiderModel>(entity);
    }
}
