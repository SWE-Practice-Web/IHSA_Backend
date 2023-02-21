using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using System.Collections;

namespace IHSA_Backend.Collections
{
    public class SchoolCollection : ISchoolCollection
    {
        private readonly IAppSettings _appSettings;
        private readonly IBaseCollection _baseCollection;
        public SchoolCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _baseCollection = new BaseCollection(
                firestore, appSettings.SchoolCollection);
        }
        public Task<IEnumerable<SchoolModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync<SchoolModel>();
        public Task<SchoolModel?> GetAsync(SchoolModel entity) =>
            _baseCollection.GetAsync<SchoolModel>(entity);
        public Task<SchoolModel> AddAsync(SchoolModel entity) =>
            _baseCollection.AddAsync<SchoolModel>(entity);
        public Task<SchoolModel> UpdateAsync(SchoolModel entity) =>
            _baseCollection.UpdateAsync<SchoolModel>(entity);
        public Task DeleteAsync(SchoolModel entity) =>
            _baseCollection.DeleteAsync<SchoolModel>(entity);
    }
}
