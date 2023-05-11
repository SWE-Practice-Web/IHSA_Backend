using Google.Cloud.Firestore;
using IHSA_Backend.Constants;

namespace IHSA_Backend.Services
{
    public class Firestore : IFirestore
    {
        private readonly IAppSettings _appSettings;
        private FirestoreDb _firestoreDb;
        public Firestore(IAppSettings appSettings)
        {
            _appSettings = appSettings;

            var firestoreId = _appSettings.FirestoreProjectId;

            // Workaround since google services need a file path
            var tempFile = Path.ChangeExtension(
                Path.GetTempFileName(), "json");
            
            File.WriteAllBytes(tempFile, 
                Convert.FromBase64String(_appSettings.GACEnvironmentB64));

            Environment.SetEnvironmentVariable(Constant.GACEnvironmentName, tempFile);
            
            _firestoreDb = FirestoreDb.Create(firestoreId);
        }
        public CollectionReference GetCollection(string collection)
        {
            return _firestoreDb.Collection(collection);
        }
    }
}
