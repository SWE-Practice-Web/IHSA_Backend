using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using System.Collections;

namespace IHSA_Backend.Collections
{
    public class SchoolCollection : ISchoolCollection
    {
        private readonly IAppSettings _appSettings;
        private readonly CollectionReference _collectionRef;
        private readonly IBaseCollection<SchoolModel> _baseCollection;
        public SchoolCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _collectionRef = firestore.GetCollection(appSettings.SchoolCollection);
            _baseCollection = new BaseCollection<SchoolModel>(_collectionRef);
        }
        public Task<IEnumerable<SchoolModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync();
        public Task<SchoolModel?> GetAsync(int id) =>
            _baseCollection.GetAsync(id);
        public Task<SchoolModel> AddAsync(SchoolModel entity) =>
            _baseCollection.AddAsync(entity);
        public Task<SchoolModel> UpdateAsync(SchoolModel entity) =>
            _baseCollection.UpdateAsync(entity);
        public Task DeleteAsync(int id) =>
            _baseCollection.DeleteAsync(id);
        public Task<bool> ExistsAsync(int id) =>
            _baseCollection.ExistsAsync(id);
    }
}
