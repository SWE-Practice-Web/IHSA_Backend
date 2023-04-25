using Google.Cloud.Firestore;
using IHSA_Backend.Constants;
using IHSA_Backend.Models;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class AdminModel : AuthUserBaseModel
    {
        [FirestoreProperty] public new Role Role { get; } = Role.Admin;
    }
    
    public class AdminRequestModel : AuthUserRequestModel
    {
        public new Role Role { get; } = Role.Admin;
    }
    public class AdminResponseModel : AuthUserResponseModel
    {
        public new Role Role { get; } = Role.Admin;
    }
}
