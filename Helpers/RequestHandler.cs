using AutoMapper;
using IHSA_Backend.Collections;
using IHSA_Backend.Models;

namespace IHSA_Backend.Helper
{
    public abstract class RequestHandler<C, T, M>
        where C : IBaseCollection<M>
        where T : class
        where M : IBaseModel
    {
        private readonly C _collection;
        private readonly IMapper _mapper;

        public RequestHandler(
            C collection,
            IMapper mapper)
        {
            _collection = collection;
            _mapper = mapper;
        }

        public static bool IsInvalidId(int id)
        {
            return id < 0;
        }

        public abstract M PreHandle(T request);

        public abstract T PostHandle(M entity);

        public async Task<T> Create(T request)
        {
            M entity = PreHandle(request);

            await _collection.AddAsync(entity);

            return PostHandle(entity);
        }

        public async Task<T?> Get(int id)
        {
            M? entity = await _collection.GetAsync(id);

            if (entity == null)
                return null;

            return PostHandle(entity);
        }

        public async Task<T?> Update(int id, T request)
        {
            M? existingEntity = await _collection.GetAsync(id);

            if (existingEntity == null)
                return null;

            M entity = PreHandle(request);
            entity.Id = id;

            await _collection.UpdateAsync(entity);

            return PostHandle(entity);
        }

        public async Task Delete(int id)
        {
            M? existingEntity = await _collection.GetAsync(id);

            if (existingEntity != null)
                await _collection.DeleteAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var entities = await _collection.GetAllAsync();
            return entities.Select(e => PostHandle(e)).ToList();
        }
    }
}
