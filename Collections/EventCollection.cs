using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using System.Collections;

namespace IHSA_Backend.Collections
{
    public class EventCollection : IEventCollection
    {
        private readonly IAppSettings _appSettings;
        private readonly IBaseCollection _baseCollection;
        public EventCollection(
            IAppSettings appSettings,
            IFirestore firestore)
        {
            _appSettings = appSettings;
            _baseCollection = new BaseCollection(
                firestore, appSettings.EventCollection);
        }
        public Task<IEnumerable<EventModel>> GetAllAsync() =>
            _baseCollection.GetAllAsync<EventModel>();
        public Task<EventModel?> GetAsync(EventModel entity) =>
            _baseCollection.GetAsync<EventModel>(entity);
        public Task<EventModel> AddAsync(EventModel entity) =>
            _baseCollection.AddAsync<EventModel>(entity);
        public Task<EventModel> UpdateAsync(EventModel entity) =>
            _baseCollection.UpdateAsync<EventModel>(entity);
        public Task DeleteAsync(EventModel entity) =>
            _baseCollection.DeleteAsync<EventModel>(entity);
    }
}