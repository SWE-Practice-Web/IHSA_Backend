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
        private readonly IBaseCollection _baseCollection;
        public UserCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _collectionRef = firestore.GetCollection(appSettings.UserCollection);
            _baseCollection = new BaseCollection(_collectionRef);
        }
        public Task<IEnumerable<UserModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync<UserModel>();
        public Task<UserModel?> GetAsync(int id) =>
            _baseCollection.GetAsync<UserModel>(id);
        public Task<UserModel> AddAsync(UserModel entity) =>
            _baseCollection.AddAsync<UserModel>(entity);
        public Task<UserModel> UpdateAsync(UserModel entity) =>
            _baseCollection.UpdateAsync<UserModel>(entity);
        public Task DeleteAsync(int id) =>
            _baseCollection.DeleteAsync<UserModel>(id);
    }
}
