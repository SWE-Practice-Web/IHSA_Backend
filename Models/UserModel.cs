using Google.Cloud.Firestore;
using IHSA_Backend.Constants;
using System.Text.Json.Serialization;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class UserModel : UserBaseModel
    {
        [FirestoreProperty] public string? Username { get; set; }
        [FirestoreProperty] public string? Password { get; set; }
        [FirestoreProperty] public Role Role { get; } = Role.Default;

    }
    public class UserRequestModel : BaseUserRequestModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class UserResponseModel : BaseUserRequestModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public Role Role { get; set; } = Role.Default;
    }
}