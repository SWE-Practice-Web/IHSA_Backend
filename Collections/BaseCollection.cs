using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using IHSA_Backend.Constants;

namespace IHSA_Backend.Collections
{
    public class BaseCollection : IBaseCollection
    {
        private readonly CollectionReference _collectionRef;
        private int nextAvailableId = 0;
        public BaseCollection(IFirestore firestore, string collectionName)
        {
            _collectionRef = firestore.GetCollection(collectionName);

            var query = _collectionRef
                .OrderByDescending(Constant.DatabaseId)
                .Limit(1);

            var snapshot = query.GetSnapshotAsync().Result;
            if (snapshot.Count > 0)
                nextAvailableId = snapshot[0].GetValue<int>("Id") + 1;
            
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
                    entity.FirebaseId = documentSnapshot.Id;
                    list.Add(entity);
                }
            }

            return list;
        }
        public async Task<T?> GetAsync<T>(T entity) where T : IBaseModel
        {
            var snapshot = await _collectionRef.Document(entity.FirebaseId).GetSnapshotAsync();

            return snapshot.Exists ? snapshot.ConvertTo<T>() : default;
        }
        public async Task<T> AddAsync<T>(T entity) where T : IBaseModel
        {
            var documentReference = await _collectionRef.AddAsync(entity);

            entity.FirebaseId = documentReference.Id;
            entity.Id = nextAvailableId++;

            return entity;
        }
        public async Task<T> UpdateAsync<T>(T entity) where T : IBaseModel
        {
            await _collectionRef.Document(entity.FirebaseId).SetAsync(entity, SetOptions.MergeAll);

            return entity;
        }
        public async Task DeleteAsync<T>(T entity) where T : IBaseModel
        {
            await _collectionRef.Document(entity.FirebaseId).DeleteAsync();
        }
    }
}
