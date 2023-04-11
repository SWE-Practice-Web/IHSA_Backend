using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface IUserCollection : IBaseCollection<UserModel>
    {
        public new Task<IEnumerable<UserModel>> GetAllAsync();
        public new Task<UserModel?> GetAsync(int id);
        public new Task<UserModel> AddAsync(UserModel entity);
        public new Task<UserModel> UpdateAsync(UserModel entity);
        public new Task DeleteAsync(int id);
        public new Task<bool> ExistsAsync(int id);
    }
}