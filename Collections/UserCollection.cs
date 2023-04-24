using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using System.Collections;

namespace IHSA_Backend.Collections
{
    public class UserCollection : IUserCollection
    {
        private readonly IAppSettings _appSettings;
        private readonly CollectionReference _collectionRef;
        private readonly IBaseCollection<UserModel> _baseCollection;
        public UserCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _collectionRef = firestore.GetCollection(appSettings.UserCollection);
            _baseCollection = new BaseCollection<UserModel>(_collectionRef);
        }
        public Task<IEnumerable<UserModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync();
        public Task<UserModel?> GetAsync(int id) =>
            _baseCollection.GetAsync(id);
        public Task<UserModel> AddAsync(UserModel entity) =>
            _baseCollection.AddAsync(entity);
        public Task<IList<UserModel>> AddBatchAsync(IList<UserModel> entities) =>
            _baseCollection.AddBatchAsync(entities);
        public Task<UserModel> UpdateAsync(UserModel entity) =>
            _baseCollection.UpdateAsync(entity);
        public Task<IList<UserModel>> UpdateBatchAsync(IList<UserModel> entities) =>
            _baseCollection.UpdateBatchAsync(entities);
        public Task DeleteAsync(int id) =>
            _baseCollection.DeleteAsync(id);
        public Task<bool> ExistsAsync(int id) =>
            _baseCollection.ExistsAsync(id);
    }
}
