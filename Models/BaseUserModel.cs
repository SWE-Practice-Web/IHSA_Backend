using Google.Cloud.Firestore;
using IHSA_Backend.Constants;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class UserBaseModel : IBaseModel
    {
        [FirestoreProperty] public int Id { get; set; }
        [FirestoreProperty] public string? FirstName { get; set; }
        [FirestoreProperty] public string? LastName { get; set; }
    }
    public class AuthUserBaseModel : UserBaseModel
    {
        [FirestoreProperty] public Role Role { get; } = Role.Default;
        [FirestoreProperty] public string? Username { get; set; }
        [FirestoreProperty] public string? Password { get; set; }
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
    public class AuthUserRequestModel : BaseUserRequestModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class AuthUserResponseModel : BaseUserRequestModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public Role Role { get; set; } = Role.Default;
    }
}
