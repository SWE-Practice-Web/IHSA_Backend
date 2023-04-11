using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using System.Collections;

namespace IHSA_Backend.Collections
{
    public class EventCollection : IEventCollection
    {
        private readonly IAppSettings _appSettings;
        private readonly CollectionReference _collectionRef;
        private readonly IBaseCollection<EventModel> _baseCollection;
        public EventCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _collectionRef = firestore.GetCollection(appSettings.EventCollection);
            _baseCollection = new BaseCollection<EventModel>(_collectionRef);
        }
        public Task<IEnumerable<EventModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync();
        public Task<EventModel?> GetAsync(int id) =>
            _baseCollection.GetAsync(id);
        public Task<EventModel> AddAsync(EventModel entity) =>
            _baseCollection.AddAsync(entity);
        public Task<EventModel> UpdateAsync(EventModel entity) =>
            _baseCollection.UpdateAsync(entity);
        public Task DeleteAsync(int id) =>
            _baseCollection.DeleteAsync(id);
        public Task<bool> ExistsAsync(int id) =>
            _baseCollection.ExistsAsync(id);
    }
}