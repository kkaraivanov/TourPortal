namespace TourPortal.Infrastructure.Models.Authentication
{
    public class LoginResponseModel
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }

        public string[] roles { get; set; }
    }
}