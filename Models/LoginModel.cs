namespace IHSA_Backend.Models
{
    public class LoginRequestModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class LoginResponseModel
    {
        public string? Token { get; set; }
        public AuthUserResponseBaseModel? User { get; set; }
    }
}
