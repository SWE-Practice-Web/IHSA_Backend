using Google.Cloud.Firestore;
using IHSA_Backend.Enums;
using IHSA_Backend.Models;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class EventElementOrderModel
    {
        [FirestoreProperty] public string? ShowClass { get; set; }
        [FirestoreProperty] public string? Class { get; set; }
        [FirestoreProperty] public string? Section { get; set; }
        [FirestoreProperty] public IList<EventPairModel>? Pairs { get; set; }
    }

    [FirestoreData]
    public class EventPairModel
    {
        [FirestoreProperty] public int RiderId { get; set; } = -1;
        [FirestoreProperty] public int RiderPlacing { get; set; }
        [FirestoreProperty] public int Order { get; set; }
        [FirestoreProperty] public string? HorseName { get; set; }
        [FirestoreProperty] public string? HorseProvider { get; set; }
    }

    public class EventElementOrderRequestModel
    {
        public string? ShowClass { get; set; }
        public string? Class { get; set; }
        public string? Section { get; set; }
        public IList<EventPairRequestModel>? Pairs { get; set; }
    }

    public class EventPairRequestModel
    {
        public int RiderId { get; set; } = -1;
        public int RiderPlacing { get; set; }
        public int Order { get; set; }
        public string? HorseName { get; set; }
        public string? HorseProvider { get; set; }
    }
    public class EventElementOrderResponseModel : EventElementOrderRequestModel { }
}
