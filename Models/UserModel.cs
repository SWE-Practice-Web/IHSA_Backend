using Google.Cloud.Firestore;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class UserModel : IBaseModel
    {
        public string Id { get; set; }
        
        [FirestoreProperty]
        public string Username { get; set; }
        
        [FirestoreProperty]
        public string Email { get; set; }
    }
    public class UserRequestModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}