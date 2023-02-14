namespace IHSA_Backend.Services
{
    public class AppSettings : IAppSettings
    {
        IConfiguration _configuration;
        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public String JWTIssuer
        {
            get => _configuration["JWTIssuer"];
        }
        public String JWTAudience
        {
            get => _configuration["JWTAudience"];
        }
        public String JWTSecret
        {
            get => _configuration["JWTSecret"];
        }
        public String FirestoreProjectId
        {
            get => _configuration["FirestoreProjectId"];
        }
        public String GoogleApplicationCredentialsPath
        {
            get => _configuration["GoogleApplicationCredentialsPath"];
        }
        public String UserCollection
        {
            get => _configuration["UserCollection"];
        }
        public String RiderCollection
        {
            get => _configuration["RiderCollection"];
        }
    }
}