using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface ISchoolCollection : IBaseCollection<SchoolModel>
    {
        public new Task<IEnumerable<SchoolModel>> GetAllAsync();
        public new Task<SchoolModel?> GetAsync(int id);
        public new Task<SchoolModel> AddAsync(SchoolModel entity);
        public new Task<SchoolModel> UpdateAsync(SchoolModel entity);
        public new Task DeleteAsync(int id);
        public new Task<bool> ExistsAsync(int id);
    }
}