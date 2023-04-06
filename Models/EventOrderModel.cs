using Google.Cloud.Firestore;
using IHSA_Backend.Enums;
using IHSA_Backend.Models;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class EventElementOrderModel
    {
        [FirestoreData] public string? ShowClass { get; set; }
        [FirestoreData] public string? Class { get; set; }
        [FirestoreData] public string? Section { get; set; }
        [FirestoreData] public IEnumerable<EventPairModel>? Pairs { get; set; }
    }

    [FirestoreData]
    public class EventPairModel
    {
        [FirestoreData] public int RiderId { get; set; } = -1;
        [FirestoreData] public int Placing { get; set; }
        [FirestoreData] public int Order { get; set; }
        [FirestoreData] public string? HorseName { get; set; }
        [FirestoreData] public string? HorseProvider { get; set; };
    }

    public class EventElementOrderRequestModel
    {
        public string? ShowClass { get; set; }
        public string? Class { get; set; }
        public string? Section { get; set; }
        public IEnumerable<EventPairRequestModel>? Pairs { get; set; }
    }

    public class EventPairRequestModel
    {
        public int RiderId { get; set; } = -1;
        public int Placing { get; set; }
        public int Order { get; set; }
        public string? HorseName { get;
        public string? HorseProvider { get; }
    }
}
