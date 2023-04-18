using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using IHSA_Backend.Constants;

namespace IHSA_Backend.Collections
{
    public class BaseCollection<T> : IBaseCollection<T>
        where T : IBaseModel
    {
        private readonly CollectionReference _collectionRef;
        private int nextAvailableId = 0;
        public BaseCollection(CollectionReference collectionRef)
        {
            _collectionRef = collectionRef;

            var maxDoc = _collectionRef.GetSnapshotAsync()
                .Result
                .Documents
                .MaxBy(doc => doc.GetValue<int>(Constant.DatabaseId) + 1);

            if (maxDoc != null)
                nextAvailableId = maxDoc.GetValue<int>(Constant.DatabaseId) + 1;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var snapshot = await _collectionRef.GetSnapshotAsync();
            var list = new List<T>();
            foreach (var documentSnapshot in snapshot.Documents)
            {
                if (!documentSnapshot.Exists)
                    continue;

                var entity = documentSnapshot.ConvertTo<T>();
                if (entity != null)
                    list.Add(entity);
            }

            return list;
        }
        public async Task<T?> GetAsync(int id)
        {
            var snapshot = await _collectionRef.Document(id.ToString()).GetSnapshotAsync();

            return snapshot.Exists ? snapshot.ConvertTo<T>() : default;
        }
        public async Task<T> AddAsync(T entity)
        {
            entity.Id = nextAvailableId++;

            var docReference = _collectionRef.Document(entity.Id.ToString());
            await docReference.CreateAsync(entity);

            return entity;
        }
        public async Task<IList<T>> AddBatchAsync(IList<T> entities)
        {
            var entitiesList = new List<T>();

            var batch = _collectionRef.Database.StartBatch();
            foreach (var entity in entities)
            {
                entity.Id = nextAvailableId++;

                var docReference = _collectionRef.Document(entity.Id.ToString());
                batch.Set(docReference, entity);
            }

            await batch.CommitAsync();

            return entitiesList;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            await _collectionRef.Document(entity.Id.ToString()).SetAsync(entity, SetOptions.MergeAll);

            return entity;
        }
        public async Task DeleteAsync(int id)
        {
            await _collectionRef.Document(id.ToString()).DeleteAsync();
        }
        public async Task<bool> ExistsAsync(int id)
        {
            var snapshot = await _collectionRef.Document(id.ToString()).GetSnapshotAsync();

            return snapshot.Exists;
        }
    }
}
