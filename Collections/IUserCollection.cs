using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface IUserCollection : IBaseCollection<UserModel>
    {
        public new Task<IEnumerable<UserModel>> GetAllAsync();
        public new Task<UserModel?> GetAsync(int id);
        public new Task<UserModel> AddAsync(UserModel entity);
        public new Task<IList<UserModel>> AddBatchAsync(IList<UserModel> entities);
        public new Task<UserModel> UpdateAsync(UserModel entity);
        public new Task<IList<UserModel>> UpdateBatchAsync(IList<UserModel> entities);
        public new Task DeleteAsync(int id);
        public new Task<bool> ExistsAsync(int id);
        public Task<UserModel?> GetByUsernameAsync(string username);
        public Task<UserModel?> TryCacheAsync(int id);
        public Task<UserModel?> TryCacheUsernameAsync(string username);
    }
}
