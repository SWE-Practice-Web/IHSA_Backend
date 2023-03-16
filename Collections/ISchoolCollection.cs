using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface ISchoolCollection
    {
        public Task<IEnumerable<SchoolModel>> GetAllAsync();
        public Task<SchoolModel?> GetAsync(int id);
        public Task<SchoolModel> AddAsync(SchoolModel entity);
        public Task<SchoolModel> UpdateAsync(SchoolModel entity);
        public Task DeleteAsync(int id);
    }
}