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
        public async Task<RiderModel> AddAsync(RiderModel entity)
        {
            return await _baseCollection.AddAsync(entity);
        }
        public async Task<RiderModel> UpdateAsync(RiderModel entity)
        {
            var properties = typeof(RiderModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = (string[])properties.Where(p => p.Name != nameof(RiderModel.Password)).Select(p => p.Name);

            var docReference = _collectionRef.Document(entity.Id.ToString());
            await docReference.SetAsync(entity, SetOptions.MergeFields(fields));

            return entity;
        }
        public Task DeleteAsync(int id) =>
            _baseCollection.DeleteAsync(id);
    }
}
