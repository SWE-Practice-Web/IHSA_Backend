using Google.Cloud.Firestore;
using IHSA_Backend.Constants;
using System.Text.Json.Serialization;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class UserModel : AuthUserBaseModel
    { }
    public class UserRequestModel : AuthUserRequestBaseModel
    { }
    public class RegisterUserRequestModel : RegisterAuthUserRequestBaseModel
    { }
    public class UserResponseModel : AuthUserResponseBaseModel
    { }
}