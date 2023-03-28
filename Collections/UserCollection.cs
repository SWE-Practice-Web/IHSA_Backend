using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using System.Collections;
using System.Reflection;

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
        public async Task<UserModel> UpdateAsync(UserModel entity)
        {
            var properties = typeof(UserModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = (string[])properties.Where(p => p.Name != nameof(UserModel.Password)).Select(p => p.Name);

            var docReference = _collectionRef.Document(entity.Id.ToString());
            await docReference.SetAsync(entity, SetOptions.MergeFields(fields));

            return entity;
        }
        public Task DeleteAsync(int id) =>
            _baseCollection.DeleteAsync(id);
    }
}
