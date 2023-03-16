using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface IEventCollection
    {
        public Task<IEnumerable<EventModel>> GetAllAsync();
        public Task<EventModel?> GetAsync(int id);
        public Task<EventModel> AddAsync(EventModel entity);
        public Task<EventModel> UpdateAsync(EventModel entity);
        public Task DeleteAsync(int id);
    }
}
