using Google.Cloud.Firestore;
using IHSA_Backend.Enums;
using IHSA_Backend.Models;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class EventModel : IBaseModel
    {
        [FirestoreProperty] public int Id { get; set; }
        [FirestoreProperty] public string? Location { get; set; }
        [FirestoreProperty] public DateTime EventTime { get; set; }
        [FirestoreProperty] public IList<EventElementOrderModel>? EventOrder { get; set; } = new List<EventElementOrderModel>();
        [FirestoreProperty] public int Zone { get; set; }
        [FirestoreProperty] public string? RidingPattern { get; set; }
        [FirestoreProperty] public string? Description { get; set; }


    }
    public class EventRequestModel
    {
        public string? Location { get; set; }
        public DateTime EventTime { get; set; }
        public IList<EventElementOrderRequestModel>? EventOrder { get; set; }
        public int Zone { get; set; }
        public string? RidingPattern { get; set; }
        public string? Description { get; set; }

    }
    public class EventResponseModel : EventRequestModel
    {
        public int Id { get; set; }
        public new IList<EventElementOrderResponseModel>? EventOrder { get; set; }
    }
}