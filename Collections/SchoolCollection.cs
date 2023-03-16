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
        private readonly IBaseCollection _baseCollection;
        public SchoolCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _collectionRef = firestore.GetCollection(appSettings.SchoolCollection);
            _baseCollection = new BaseCollection(_collectionRef);
        }
        public Task<IEnumerable<SchoolModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync<SchoolModel>();
        public Task<SchoolModel?> GetAsync(int id) =>
            _baseCollection.GetAsync<SchoolModel>(id);
        public Task<SchoolModel> AddAsync(SchoolModel entity) =>
            _baseCollection.AddAsync<SchoolModel>(entity);
        public Task<SchoolModel> UpdateAsync(SchoolModel entity) =>
            _baseCollection.UpdateAsync<SchoolModel>(entity);
        public Task DeleteAsync(int id) =>
            _baseCollection.DeleteAsync<SchoolModel>(id);
    }
}
