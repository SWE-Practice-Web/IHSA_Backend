using Google.Cloud.Firestore;
using IHSA_Backend.Constants;
using IHSA_Backend.Models;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class AdminModel : AuthUserBaseModel
    {
        [FirestoreProperty] public new Role Role { get; set; } = Role.Admin;
    }
    
    public class AdminRequestModel : AuthUserRequestBaseModel
    { }
    public class AdminResponseModel : AuthUserResponseBaseModel
    {
        public new Role Role { get; set; } = Role.Admin;
    }
}
