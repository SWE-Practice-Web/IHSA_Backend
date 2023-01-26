namespace IHSA_Backend.Services
{
    public interface IAppSettings
    {
        public String JWTIssuer { get; }
        public String JWTAudience { get; }
        public String JWTSecret { get; }
    }
}