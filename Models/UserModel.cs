using Google.Cloud.Firestore;
using IHSA_Backend.Enums;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class UserModel : IBaseModel
    {
        [FirestoreProperty] public int Id { get; set; }
        [FirestoreProperty] public string? FirstName { get; set; }
        [FirestoreProperty] public string? LastName { get; set; }
        [FirestoreProperty] public string? UserName { get; set; }
        [FirestoreProperty] public string? Password { get; set; }
        [FirestoreProperty] public Role Role { get; set; }
    }
    public class BaseUserRequestModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
    public class BaseUserResponseModel : BaseUserRequestModel
    {
        public int Id { get; set; }
    }
}