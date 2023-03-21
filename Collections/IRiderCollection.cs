using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface IRiderCollection
    {
        public Task<IEnumerable<RiderModel>> GetAllAsync();
        public Task<RiderModel?> GetAsync(int id);
        public Task<RiderModel> AddAsync(RiderModel entity);
        public Task<RiderModel> UpdateAsync(RiderModel entity);
        public Task DeleteAsync(int id);
    }
}
