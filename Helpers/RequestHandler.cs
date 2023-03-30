﻿using AutoMapper;
using IHSA_Backend.Collections;
using IHSA_Backend.Models;

namespace IHSA_Backend.Helper
{
    public abstract class RequestHandler<C, T, R, M>
        where C : IBaseCollection<M>
        where T : class
        where R : class
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
        public bool IsInvalidId(int id)
        {
            return id < 0;
        }
        public M PreHandle(T request)
        {
            return _mapper.Map<M>(request);
        }
        public R PostHandle(M entity)
        {
            return _mapper.Map<R>(entity);
        }
        public async Task<R> Create(T request)
        {
            M entity = PreHandle(request);

            await _collection.AddAsync(entity);

            return PostHandle(entity);
        }
        public async Task<R?> Get(int id)
        {
            M? entity = await _collection.GetAsync(id);

            if (entity == null)
                return null;

            return PostHandle(entity);
        }
        public async Task<R?> Update(int id, T request)
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
        public async Task<IEnumerable<R>> GetAll()
        {
            var entities = await _collection.GetAllAsync();
            return entities.Select(e => PostHandle(e)).ToList();
        }
    }
}