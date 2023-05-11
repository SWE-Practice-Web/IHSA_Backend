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
        private IDictionary<int, UserModel> _userCache;
        public UserCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _collectionRef = firestore.GetCollection(appSettings.UserCollection);
            _baseCollection = new BaseCollection<UserModel>(_collectionRef);

            _userCache = new Dictionary<int, UserModel>();
            foreach (var user in _baseCollection.GetAllAsync().Result)
            {
                _userCache.Add(user.Id, user);
            }
        }
        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await _baseCollection.GetAllAsync();

            _userCache = new Dictionary<int, UserModel>();
            foreach (var user in await _baseCollection.GetAllAsync())
            {
                _userCache.Add(user.Id, user);
            }

            return users;
        }
        public async Task<UserModel?> GetAsync(int id)
        {
            var user = await _baseCollection.GetAsync(id);

            if (user != null && !user.Equals(default(UserModel)))
            {
                if (_userCache.ContainsKey(user.Id))
                    _userCache.Remove(user.Id);

                _userCache.Add(user.Id, user);
            }

            return user;
        }
        public async Task<UserModel> AddAsync(UserModel entity)
        {
            var user = await _baseCollection.AddAsync(entity);

            if (user != null && !user.Equals(default(UserModel)))
            {
                if (_userCache.ContainsKey(user.Id))
                    _userCache.Remove(user.Id);

                _userCache.Add(user.Id, user);
            }

            return user ?? new UserModel();
        }
        public async Task<IList<UserModel>> AddBatchAsync(IList<UserModel> entities)
        {
            var users = await _baseCollection.AddBatchAsync(entities);

            foreach (var user in users)
            {
                if (_userCache.ContainsKey(user.Id))
                    _userCache.Remove(user.Id);

                _userCache.Add(user.Id, user);
            }

            return users;
        }
        public async Task<UserModel> UpdateAsync(UserModel entity)
        {
            var user = await _baseCollection.UpdateAsync(entity);


            if (user != null && !user.Equals(default(UserModel)))
            {
                if (_userCache.ContainsKey(user.Id))
                    _userCache.Remove(user.Id);

                _userCache.Add(user.Id, user);
            }

            return user ?? new UserModel();
        }
        public async Task<IList<UserModel>> UpdateBatchAsync(IList<UserModel> entities)
        {
            var users = await _baseCollection.UpdateBatchAsync(entities);

            foreach (var user in users)
            {
                if (_userCache.ContainsKey(user.Id))
                    _userCache.Remove(user.Id);

                _userCache.Add(user.Id, user);
            }

            return users;
        }
        public async Task DeleteAsync(int id)
        {
            await _baseCollection.DeleteAsync(id);

            if (_userCache.ContainsKey(id))
                _userCache.Remove(id);
        }
        public Task<bool> ExistsAsync(int id) =>
            _baseCollection.ExistsAsync(id);
        public async Task<UserModel?> GetByUsernameAsync(string username)
        {
            var users = await GetAllAsync();

            foreach (var user in users)
            {
                var currentUsername = user.Username;

                if (currentUsername != null && currentUsername.Equals(username))
                    return user;
            }

            return default;
        }
        public async Task<UserModel?> TryCacheAsync(int id)
        {
            if (_userCache.ContainsKey(id))
                return await Task.FromResult(_userCache[id]);

            return await GetAsync(id);
        }
        public async Task<UserModel?> TryCacheUsernameAsync(string username)
        {
            foreach (var key in _userCache.Keys)
            {
                var currentUsername = _userCache[key].Username;

                if (currentUsername != null && currentUsername.Equals(username))
                {
                    return _userCache[key];
                }
            }

            return await GetByUsernameAsync(username);
        }
    }
}
