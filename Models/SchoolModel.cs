using Google.Cloud.Firestore;
using IHSA_Backend.Enums;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class SchoolModel : IBaseModel
    {
        public string FirebaseId { get; set; }
        [FirestoreProperty]
        public int Id { get; set; }
        [FirestoreProperty]
        public string SchoolName{ get; set; }
        [FirestoreProperty]
        public string Location { get; set; }
        [FirestoreProperty]
        public double Latitude { get; set; }
        [FirestoreProperty]
        public double Longitude { get; set; }
        [FirestoreProperty]
        public int Region { get; set; }
        [FirestoreProperty]
        public int Zone { get; set; }
        [FirestoreProperty]
        public int NumRiders { get; set; }
        [FirestoreProperty]
        public bool AnchorSchool { get; set; }
    }
    public class SchoolRequestModel
    {
        public string SchoolName { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Region { get; set; }
        public int Zone { get; set; }
        public int NumRiders { get; set; }
        public bool AnchorSchool { get; set; }
    }
}