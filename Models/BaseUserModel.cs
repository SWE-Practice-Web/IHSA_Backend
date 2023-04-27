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
        [FirestoreProperty] public string? Email { get; set; }
    }
    public class AuthUserBaseModel : UserBaseModel
    {
        [FirestoreProperty] public Role Role { get; set; } = Role.Default;
        [FirestoreProperty] public string? Username { get; set; }
        [FirestoreProperty] public string? Password { get; set; }
    }
    public class BaseUserRequestModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
    public class BaseUserResponseModel : BaseUserRequestModel
    {
        public int Id { get; set; }
    }
    public class AuthUserRequestBaseModel : BaseUserRequestModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; } = Role.Default;
    }
    public class RegisterAuthUserRequestBaseModel : BaseUserRequestModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class AuthUserResponseBaseModel : BaseUserRequestModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public Role Role { get; set; } = Role.Default;
    }
}
