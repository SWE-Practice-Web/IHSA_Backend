using IHSA_Backend.Models;

namespace IHSA_Backend.Collections
{
    public interface IBaseCollection
    {
        public Task<IEnumerable<T>> GetAllAsync<T>() where T : IBaseModel;
        public Task<T?> GetAsync<T>(int id) where T : IBaseModel;
        public Task<T> AddAsync<T>(T entity) where T : IBaseModel;
        public Task<T> UpdateAsync<T>(T entity) where T : IBaseModel;
        public Task DeleteAsync<T>(int id) where T : IBaseModel;
    }
}
