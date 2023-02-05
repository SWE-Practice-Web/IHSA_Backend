using Google.Cloud.Firestore;

namespace IHSA_Backend.Services
{
    public class Firestore : IFirestore
    {
        private readonly IAppSettings _appSettings;
        private FirestoreDb _firestoreDb;
        public Firestore(IAppSettings appSettings)
        {
            _appSettings = appSettings;

            Environment.SetEnvironmentVariable(
                "GOOGLE_APPLICATION_CREDENTIALS",
                _appSettings.GoogleApplicationCredentialsPath);

            _firestoreDb = FirestoreDb.Create(_appSettings.FirestoreProjectId);
        }
        public CollectionReference GetCollection(string collection)
        {
            return _firestoreDb.Collection(collection);
        }
    }
}
