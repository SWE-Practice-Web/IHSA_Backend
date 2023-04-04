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
        public RiderCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _collectionRef = firestore.GetCollection(appSettings.RiderCollection);
            _baseCollection = new BaseCollection<RiderModel>(_collectionRef);
        }
        public Task<IEnumerable<RiderModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync();
        public Task<RiderModel?> GetAsync(int id) =>
            _baseCollection.GetAsync(id);
        public Task<RiderModel> AddAsync(RiderModel entity) =>
            _baseCollection.AddAsync(entity);
        public Task<RiderModel> UpdateAsync(RiderModel entity) =>
            _baseCollection.UpdateAsync(entity);
        public Task DeleteAsync(int id) =>
            _baseCollection.DeleteAsync(id);
    }
}
