using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface IAdminCollection : IBaseCollection<AdminModel>
    {
        public new Task<IEnumerable<AdminModel>> GetAllAsync();
        public new Task<AdminModel?> GetAsync(int id);
        public new Task<AdminModel> AddAsync(AdminModel entity);
        public new Task<IList<AdminModel>> AddBatchAsync(IList<AdminModel> entities);
        public new Task<AdminModel> UpdateAsync(AdminModel entity);
        public new Task<IList<AdminModel>> UpdateBatchAsync(IList<AdminModel> entities);
        public new Task DeleteAsync(int id);
        public new Task<bool> ExistsAsync(int id);
        public Task<AdminModel?> GetByUsernameAsync(string Adminname);
        public Task<AdminModel?> TryCacheAsync(int id);
        public Task<AdminModel?> TryCacheUsernameAsync(string Adminname);
    }
}
