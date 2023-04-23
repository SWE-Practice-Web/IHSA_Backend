using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface IEventCollection : IBaseCollection<EventModel>
    {
        public new Task<IEnumerable<EventModel>> GetAllAsync();
        public new Task<EventModel?> GetAsync(int id);
        public new Task<EventModel> AddAsync(EventModel entity);
        public new Task<IList<EventModel>> AddBatchAsync(IList<EventModel> entities);
        public new Task<EventModel> UpdateAsync(EventModel entity);
        public new Task<IList<EventModel>> UpdateBatchAsync(IList<EventModel> entities);
        public new Task DeleteAsync(int id);
        public new Task<bool> ExistsAsync(int id);
    }
}
