using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;

namespace IHSA_Backend.Collections
{
    public class BaseCollection
    {
        private readonly CollectionReference _collectionRef;
        public BaseCollection(IFirestore firestore, String collectionName)
        {
            _collectionRef = firestore.GetCollection(collectionName);
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : IBaseModel
        {
            var snapshot = await _collectionRef.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in snapshot.Documents)
            {
                if (!documentSnapshot.Exists)
                    continue;

                var entity = documentSnapshot.ConvertTo<T>();
                if (entity != null)
                {
                    entity.Id = documentSnapshot.Id;
                    list.Add(entity);
                }
            }

            return list;
        }
        public async Task<T?> GetAsync<T>(T entity) where T : IBaseModel
        {
            var snapshot = await _collectionRef.Document(entity.Id).GetSnapshotAsync();

            return snapshot.Exists ? snapshot.ConvertTo<T>() : default;
        }
        public async Task<T> AddAsync<T>(T entity) where T : IBaseModel
        {
            var documentReference = await _collectionRef.AddAsync(entity);

            entity.Id = documentReference.Id;

            return entity;
        }
        public async Task<T> UpdateAsync<T>(T entity) where T : IBaseModel
        {
            await _collectionRef.Document(entity.Id).SetAsync(entity, SetOptions.MergeAll);

            return entity;
        }
        public async Task DeleteAsync<T>(T entity) where T : IBaseModel
        {
            await _collectionRef.Document(entity.Id).DeleteAsync();
        }
    }
}
