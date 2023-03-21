namespace IHSA_Backend.BLL
{
    public interface IBaseRequestHandler
    {
        public Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
        public Task<T?> GetAsync<T>(int id) where T : class;
        public Task<T> AddAsync<T>(T entity) where T : class;
        public Task<T> UpdateAsync<T>(T entity) where T : class;
        public Task DeleteAsync<T>(T entity) where T : class;
        public Task DeleteByIdAsync<T>(int id) where T : class;
    }
}
