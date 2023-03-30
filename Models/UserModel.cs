using Google.Cloud.Firestore;
using IHSA_Backend.Enums;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class UserModel : IBaseModel
    {
        [FirestoreProperty]
        public int Id { get; set; }
        [FirestoreProperty]
        public string? Username { get; set; }
        [FirestoreProperty]
        public string? Password { get; set; }
        [FirestoreProperty]
        public string? FirstName { get; set; }
        [FirestoreProperty]
        public string? LastName { get; set; }
        [FirestoreProperty]
        public string? Email { get; set; }
    }
    public class UserRequestModel
    {
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
    public class UserResponseModel : UserRequestModel
    {
        public int Id { get; set; }
    }
}