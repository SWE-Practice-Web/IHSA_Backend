using Google.Cloud.Firestore;
using IHSA_Backend.Models;
using IHSA_Backend.Services;
using IHSA_Backend.Constants;
using static Grpc.Core.Metadata;
using System.Collections.Generic;

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
            entity.Id = nextAvailableId++;

            var documentReference = await _collectionRef.AddAsync(entity);

            entity.FirebaseId = documentReference.Id;

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
        public async Task<T?> GetByIdAsync<T>(int id) where T : IBaseModel
        {
            var snapshot = await _collectionRef.GetSnapshotAsync();

            foreach (var documentSnapshot in snapshot.Documents)
            {
                if (!documentSnapshot.Exists)
                    continue;

                var entity = documentSnapshot.ConvertTo<T>();
                if (entity.Id == id)
                {
                    return entity;
                }
            }

            return default;
        }
    }
}
