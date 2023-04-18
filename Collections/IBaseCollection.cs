using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface IBaseCollection<T> where T : IBaseModel
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetAsync(int id);
        public Task<T> AddAsync(T entity);
        public Task<IList<T>> AddBatchAsync(IList<T> entities);
        public Task<T> UpdateAsync(T entity);
        public Task<IList<T>> UpdateBatchAsync(IList<T> entities);
        public Task DeleteAsync(int id);
        public Task<bool> ExistsAsync(int id);
    }
}
