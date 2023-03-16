using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using System.Collections;

namespace IHSA_Backend.Collections
{
    public class RiderCollection : IRiderCollection
    {
        private readonly IAppSettings _appSettings;
        private readonly CollectionReference _collectionRef;
        private readonly IBaseCollection _baseCollection;
        public RiderCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _collectionRef = firestore.GetCollection(appSettings.RiderCollection);
            _baseCollection = new BaseCollection(_collectionRef);
        }
        public Task<IEnumerable<RiderModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync<RiderModel>();
        public Task<RiderModel?> GetAsync(int id) =>
            _baseCollection.GetAsync<RiderModel>(id);
        public async Task<RiderModel> AddAsync(RiderModel entity)
        {
            var docReference = _collectionRef.Document(entity.Id.ToString());
            await docReference.CreateAsync(entity);

            return entity;
        }
        public Task<RiderModel> UpdateAsync(RiderModel entity) =>
            _baseCollection.UpdateAsync<RiderModel>(entity);
        public Task DeleteAsync(int id) =>
            _baseCollection.DeleteAsync<RiderModel>(id);
    }
}
