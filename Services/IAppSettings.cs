﻿namespace IHSA_Backend.Services
{
    public interface IAppSettings
    {
        public string JWTIssuer { get; }
        public string JWTAudience { get; }
        public string JWTSecret { get; }
        public string FirestoreProjectId { get; }
        public string GACEnvironmentB64 { get; }
        public string UserCollection { get; }
        public string RiderCollection { get; }
        public string SchoolCollection { get; }
        public string EventCollection { get; }
        public string AdminCollection { get; }
        public string CoachCollection { get; }
        public string EventAdminCollection { get; }
    }
}