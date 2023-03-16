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

            string firestoreId;

            if (Environment.GetEnvironmentVariable(Constant.GACEnvironmentB64Name) != null)
            {
                // Workaround for docker support
                var tempFile = Path.GetTempFileName();

                tempFile = Path.ChangeExtension(tempFile, "json");

                var b64GAC = Environment.GetEnvironmentVariable(Constant.GACEnvironmentB64Name) ?? string.Empty;

                File.WriteAllBytes(tempFile, 
                    Convert.FromBase64String(b64GAC));

                Environment.SetEnvironmentVariable(Constant.GACEnvironmentName, tempFile);

                firestoreId = Environment.GetEnvironmentVariable(Constant.FirestoreIdEnvironmentName) ?? string.Empty;
            }
            else
            {
                Environment.SetEnvironmentVariable(
                    Constant.GACEnvironmentName,
                    _appSettings.GoogleApplicationCredentialsPath);

                firestoreId = _appSettings.FirestoreProjectId;
            }
            
            _firestoreDb = FirestoreDb.Create(firestoreId);
        }
        public CollectionReference GetCollection(string collection)
        {
            return _firestoreDb.Collection(collection);
        }
    }
}
