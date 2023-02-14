namespace IHSA_Backend.Services
{
    public interface IAppSettings
    {
        public string JWTIssuer { get; }
        public string JWTAudience { get; }
        public string JWTSecret { get; }
        public string FirestoreProjectId { get; }
        public string GoogleApplicationCredentialsPath { get; }
        public string UserCollection { get; }
        public string RiderCollection { get; }
    }
}