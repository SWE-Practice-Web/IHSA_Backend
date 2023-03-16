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

            var maxDoc = _collectionRef.GetSnapshotAsync()
                .Result
                .Documents
                .MaxBy(doc => doc.GetValue<int>(Constant.DatabaseId) + 1);

            if (maxDoc != null)
                nextAvailableId = maxDoc.GetValue<int>(Constant.DatabaseId) + 1;
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
                    list.Add(entity);
            }

            return list;
        }
        public async Task<T?> GetAsync<T>(T entity) where T : IBaseModel
        {
            var snapshot = await _collectionRef.Document(entity.Id.ToString()).GetSnapshotAsync();

            return snapshot.Exists ? snapshot.ConvertTo<T>() : default;
        }
        public async Task<T> AddAsync<T>(T entity) where T : IBaseModel
        {
            entity.Id = nextAvailableId++;

            var docReference = _collectionRef.Document(entity.Id.ToString());
            await docReference.CreateAsync(entity);

            return entity;
        }
        public async Task<T> UpdateAsync<T>(T entity) where T : IBaseModel
        {
            await _collectionRef.Document(entity.Id.ToString()).SetAsync(entity, SetOptions.MergeAll);

            return entity;
        }
        public async Task DeleteAsync<T>(T entity) where T : IBaseModel
        {
            await _collectionRef.Document(entity.Id.ToString()).DeleteAsync();
        }
        public async Task<T?> GetByIdAsync<T>(int id) where T : IBaseModel
        {
            var docReference = _collectionRef.Document(id.ToString());
            var snapshot = await docReference.GetSnapshotAsync();

            if (snapshot.Exists && snapshot.GetValue<int>(Constant.DatabaseId) == id)
                return snapshot.ConvertTo<T>();

            return default;
        }
        public async Task DeleteByIdAsync<T>(int id) where T : IBaseModel
        {
            var docReference = _collectionRef.Document(id.ToString());
            await docReference.DeleteAsync();
        }
    }
}
