using IHSA_Backend.Models;

namespace IHSA_Backend.BLL
{
    public interface IBaseRequestHandler<T, R> 
        where T : class
        where R : class
    {
        public Task<R> Create(T request);
        public Task<IList<R>> BatchCreate(IList<T> requests);
        public Task<R?> Get(int id);
        public Task<R?> Update(int id, T request);
        public Task Delete(int id);
        public Task<IEnumerable<R>> GetAll();
        public bool IsInvalidId(int id);
    }
}
