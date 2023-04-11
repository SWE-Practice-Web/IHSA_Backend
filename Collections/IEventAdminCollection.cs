using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface IEventAdminCollection : IBaseCollection<EventAdminModel>
    {
        public new Task<IEnumerable<EventAdminModel>> GetAllAsync();
        public new Task<EventAdminModel?> GetAsync(int id);
        public new Task<EventAdminModel> AddAsync(EventAdminModel entity);
        public new Task<EventAdminModel> UpdateAsync(EventAdminModel entity);
        public new Task DeleteAsync(int id);
    }
}
