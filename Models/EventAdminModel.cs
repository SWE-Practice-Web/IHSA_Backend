using Google.Cloud.Firestore;
using IHSA_Backend.Enums;
using IHSA_Backend.Models;
using System.Collections.Generic;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class EventAdminModel : UserModel
    {
        [FirestoreProperty] public string PhoneNumber { get; set; }
        [FirestoreProperty] public string Email { get; set; }
        [FirestoreProperty] public IEnumerable<int>? EventsManaged { get; set; }
    }
    public class EventAdminRequestModel : UserRequestModel
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<int>? EventsManaged { get; set; }
    }
    public class EventAdminResponseModel : EventAdminRequestModel
    {
        public int Id { get; set; }
    }
}
