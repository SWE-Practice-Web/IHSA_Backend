using Google.Cloud.Firestore;
using System.Runtime.InteropServices;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class SchoolModel : IBaseModel
    {
        [FirestoreProperty] public int Id { get; set; }
        [FirestoreProperty] public string? SchoolName { get; set; }
        [FirestoreProperty] public string? StateCode { get; set; }
        [FirestoreProperty] public double Latitude { get; set; }
        [FirestoreProperty] public double Longitude { get; set; }
        [FirestoreProperty] public int Region { get; set; }
        [FirestoreProperty] public int Zone { get; set; }
        [FirestoreProperty] public int NumRiders { get; set; }
        [FirestoreProperty] public bool IsAnchorSchool { get; set; }
    }
    public class SchoolRequestModel
    {
        public string? SchoolName { get; set; }
        public string? StateCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Region { get; set; }
        public int Zone { get; set; }
        public int NumRiders { get; set; }
        public bool IsAnchorSchool { get; set; }
    }
    public class SchoolResponseModel : SchoolRequestModel
    {
        public int Id { get; set; }
    }
}