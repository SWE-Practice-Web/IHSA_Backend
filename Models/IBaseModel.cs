using Google.Cloud.Firestore;

namespace IHSA_Backend.Models
{
    public interface IBaseModel
    {
        public string FirebaseId { get; set; }
        [FirestoreProperty]
        public int Id { get; set; }
    }
}
