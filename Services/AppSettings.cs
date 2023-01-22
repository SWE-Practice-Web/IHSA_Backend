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
    }
}