using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;

namespace IHSA_Backend.Collections
{
    public class AdminCollection : IAdminCollection
    {
        private readonly IAppSettings _appSettings;
        private readonly CollectionReference _collectionRef;
        private readonly IBaseCollection<AdminModel> _baseCollection;
        private IDictionary<int, AdminModel> _AdminCache;
        public AdminCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _collectionRef = firestore.GetCollection(appSettings.AdminCollection);
            _baseCollection = new BaseCollection<AdminModel>(_collectionRef);

            _AdminCache = new Dictionary<int, AdminModel>();
            foreach (var Admin in _baseCollection.GetAllAsync().Result)
            {
                _AdminCache.Add(Admin.Id, Admin);
            }
        }
        public async Task<IEnumerable<AdminModel>> GetAllAsync()
        {
            var Admins = await _baseCollection.GetAllAsync();

            _AdminCache = new Dictionary<int, AdminModel>();
            foreach (var Admin in await _baseCollection.GetAllAsync())
            {
                _AdminCache.Add(Admin.Id, Admin);
            }

            return Admins;
        }
        public async Task<AdminModel?> GetAsync(int id)
        {
            var Admin = await _baseCollection.GetAsync(id);

            if (Admin != null && !Admin.Equals(default(AdminModel)))
            {
                if (_AdminCache.ContainsKey(Admin.Id))
                    _AdminCache.Remove(Admin.Id);

                _AdminCache.Add(Admin.Id, Admin);
            }

            return Admin;
        }
        public async Task<AdminModel> AddAsync(AdminModel entity)
        {
            var Admin = await _baseCollection.AddAsync(entity);

            if (Admin != null && !Admin.Equals(default(AdminModel)))
            {
                if (_AdminCache.ContainsKey(Admin.Id))
                    _AdminCache.Remove(Admin.Id);

                _AdminCache.Add(Admin.Id, Admin);
            }

            return Admin ?? new AdminModel();
        }
        public async Task<IList<AdminModel>> AddBatchAsync(IList<AdminModel> entities)
        {
            var Admins = await _baseCollection.AddBatchAsync(entities);

            foreach (var Admin in Admins)
            {
                if (_AdminCache.ContainsKey(Admin.Id))
                    _AdminCache.Remove(Admin.Id);

                _AdminCache.Add(Admin.Id, Admin);
            }

            return Admins;
        }
        public async Task<AdminModel> UpdateAsync(AdminModel entity)
        {
            var Admin = await _baseCollection.UpdateAsync(entity);


            if (Admin != null && !Admin.Equals(default(AdminModel)))
            {
                if (_AdminCache.ContainsKey(Admin.Id))
                    _AdminCache.Remove(Admin.Id);

                _AdminCache.Add(Admin.Id, Admin);
            }

            return Admin ?? new AdminModel();
        }
        public async Task<IList<AdminModel>> UpdateBatchAsync(IList<AdminModel> entities)
        {
            var Admins = await _baseCollection.UpdateBatchAsync(entities);

            foreach (var Admin in Admins)
            {
                if (_AdminCache.ContainsKey(Admin.Id))
                    _AdminCache.Remove(Admin.Id);

                _AdminCache.Add(Admin.Id, Admin);
            }

            return Admins;
        }
        public async Task DeleteAsync(int id)
        {
            await _baseCollection.DeleteAsync(id);

            if (_AdminCache.ContainsKey(id))
                _AdminCache.Remove(id);
        }
        public Task<bool> ExistsAsync(int id) =>
            _baseCollection.ExistsAsync(id);
        public async Task<AdminModel?> GetByUsernameAsync(string username)
        {
            var Admins = await GetAllAsync();

            foreach (var Admin in Admins)
            {
                var currentusername = Admin.Username;

                if (currentusername != null && currentusername.Equals(username))
                    return Admin;
            }

            return default;
        }
        public async Task<AdminModel?> TryCacheAsync(int id)
        {
            if (_AdminCache.ContainsKey(id))
                return await Task.FromResult(_AdminCache[id]);

            return await GetAsync(id);
        }
        public async Task<AdminModel?> TryCacheUsernameAsync(string username)
        {
            foreach (var key in _AdminCache.Keys)
            {
                var currentusername = _AdminCache[key].Username;

                if (currentusername != null && currentusername.Equals(username))
                {
                    return _AdminCache[key];
                }
            }

            return await GetByUsernameAsync(username);
        }
    }
}
