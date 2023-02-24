using Google.Cloud.Firestore;
using IHSA_Backend.Enums;
using IHSA_Backend.Models;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class EventModel : IBaseModel
    {
        public string FirebaseId { get; set; }
        [FirestoreProperty]
        public int Id { get; set; }
        [FirestoreProperty]
        public string Location { get; set; }
        [FirestoreProperty]
        public DateTime EventTime { get; set; }
        [FirestoreProperty]
        public IEnumerable<RiderModel> Riders { get; set; }
        [FirestoreProperty]
        public int Zone { get; set; }
    }
    public class EventRequestModel
    {
        public string Location { get; set; }
        public DateTime EventTime { get; set; }
        public IEnumerable<RiderModel> Riders { get; set; }
        public int Zone { get; set; }
    }
}