using Google.Cloud.Firestore;

namespace IHSA_Backend.Services
{
    public interface IFirestore
    {
        public CollectionReference GetCollection(string collection);
    }
}
