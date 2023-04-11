using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using System.Collections;
using System.Reflection;

namespace IHSA_Backend.Collections
{
    public class EventAdminCollection : IEventAdminCollection
    {
        private readonly IAppSettings _appSettings;
        private readonly CollectionReference _collectionRef;
        private readonly IBaseCollection<EventAdminModel> _baseCollection;
        public EventAdminCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _collectionRef = firestore.GetCollection(appSettings.EventAdminCollection);
            _baseCollection = new BaseCollection<EventAdminModel>(_collectionRef);
        }
        public Task<IEnumerable<EventAdminModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync();
        public Task<EventAdminModel?> GetAsync(int id) =>
            _baseCollection.GetAsync(id);
        public Task<EventAdminModel> AddAsync(EventAdminModel entity) =>
            _baseCollection.AddAsync(entity);
        public Task<EventAdminModel> UpdateAsync(EventAdminModel entity) =>
            _baseCollection.UpdateAsync(entity);
        public Task DeleteAsync(int id) =>
            _baseCollection.DeleteAsync(id);
    }
}
