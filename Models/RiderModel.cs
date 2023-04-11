using Google.Cloud.Firestore;
using IHSA_Backend.Enums;
using IHSA_Backend.Models;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class RiderModel : UserModel
    {
        [FirestoreProperty] public int RiderId { get; set; }
        [FirestoreProperty] public bool IsHeightRider { get; set; } = false;
        [FirestoreProperty] public bool IsWeightRider { get; set; } = false;
        [FirestoreProperty] public double? Height { get; set; }
        [FirestoreProperty] public double? Weight { get; set; }
        [FirestoreProperty] public IEnumerable<int>? ManagedBy { get; set; }
        [FirestoreProperty] public string? PlaysFor { get; set; }
    }
    public class RiderRequestModel : UserRequestModel
    {
        public int? RiderId { get; set; }
        public bool IsHeightRider { get; set; }
        public bool IsWeightRider { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public IEnumerable<int>? ManagedBy { get; set; }
        public string? PlaysFor { get; set; }
    }
    public class RiderResponseModel : RiderRequestModel
    {
        public int Id { get; set; }
    }
}
