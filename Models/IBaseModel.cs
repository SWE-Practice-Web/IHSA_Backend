using Google.Cloud.Firestore;

namespace IHSA_Backend.Models
{
    public interface IBaseModel
    {
        [FirestoreProperty]
        public int Id { get; set; }
    }
}
