using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface IUserCollection
    {
        public Task<IEnumerable<UserModel>> GetAllAsync();
        public Task<UserModel?> GetAsync(UserModel entity);
        public Task<UserModel> AddAsync(UserModel entity);
        public Task<UserModel> UpdateAsync(UserModel entity);
        public Task DeleteAsync(UserModel entity);
        public Task<UserModel?> GetByIdAsync(int id);
    }
}