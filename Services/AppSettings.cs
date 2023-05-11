namespace IHSA_Backend.Services
{
    public class AppSettings : IAppSettings
    {
        IConfiguration _configuration;
        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string JWTIssuer
        {
            get => _configuration["JWTIssuer"];
        }
        public string JWTAudience
        {
            get => _configuration["JWTAudience"];
        }
        public string JWTSecret
        {
            get => _configuration["JWTSecret"];
        }
        public string FirestoreProjectId
        {
            get => _configuration["FirestoreProjectId"];
        }
        public string GoogleApplicationCredentialsPath
        {
            get => _configuration["GoogleApplicationCredentialsPath"];
        }
        public string UserCollection
        {
            get => _configuration["UserCollection"];
        }
        public string RiderCollection
        {
            get => _configuration["RiderCollection"];
        }
        public string SchoolCollection
        {
            get => _configuration["SchoolCollection"];
        }
        public string EventCollection
        {
            get => _configuration["EventCollection"];
        }
        public string AdminCollection
        {
            get => _configuration["AdminCollection"];
        }
        public string CoachCollection
        {
            get => _configuration["CoachCollection"];
        }
        public string EventAdminCollection
        {
            get => _configuration["EventAdminCollection"];
        }
    }
}