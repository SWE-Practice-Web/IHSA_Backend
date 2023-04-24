using Google.Cloud.Firestore;
using IHSA_Backend.Constants;
using System.Text.Json.Serialization;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class UserBaseModel : IBaseModel
    {
        [FirestoreProperty] public int Id { get; set; }
        [FirestoreProperty] public string? FirstName { get; set; }
        [FirestoreProperty] public string? LastName { get; set; }
    }
    public class UserModel : UserBaseModel
    {
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
    public class UserRequestModel : BaseUserRequestModel
    {
        [JsonIgnore]
        public string? Password { get; set; }
        public Role Role { get; set; }
    }
    public class UserResponseModel : UserRequestModel
    {
        public int Id { get; set; }
    }
}