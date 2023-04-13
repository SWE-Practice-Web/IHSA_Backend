using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface IRiderCollection : IBaseCollection<RiderModel>
    {
        public new Task<IEnumerable<RiderModel>> GetAllAsync();
        public new Task<RiderModel?> GetAsync(int id);
        public new Task<RiderModel> AddAsync(RiderModel entity);
        public new Task<RiderModel> UpdateAsync(RiderModel entity);
        public new Task DeleteAsync(int id);
        public new Task<bool> ExistsAsync(int id);
        public RiderModel? GetByRiderIdCache(int riderId);
    }
}
