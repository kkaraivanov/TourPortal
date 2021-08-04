namespace TourPortal.Infrastructure.Shared.Models.Response
{
    public class UserInfoResponse
    {
        public string Id { get; set; }

        public byte[] ProfileImage { get; set; }

        public string ProfileName { get; set; }

        public string UserRole { get; set; }
    }
}