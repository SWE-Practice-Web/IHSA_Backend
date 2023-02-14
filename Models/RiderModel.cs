﻿using Google.Cloud.Firestore;
using IHSA_Backend.Enums;
using IHSA_Backend.Models;

namespace IHSA_Backend.Models
{
    [FirestoreData]
    public class RiderModel : UserModel
    {
        [FirestoreProperty]
        public bool isHeightWeightRider { get; set; }
        [FirestoreProperty]
        public double? Height { get; set; }
        [FirestoreProperty]
        public double? Weight { get; set; }
        [FirestoreProperty]
        public IEnumerable<string> ManagedBy { get; set; }
        [FirestoreProperty]
        public string PlaysFor { get; set; }
    }
    public class RiderRequestModel : UserRequestModel
    {
        public bool isHeightWeightRider { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public IEnumerable<string> ManagedBy { get; set; }
        public string PlaysFor { get; set; }
    }
}
