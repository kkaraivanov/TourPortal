namespace TourPortal.Infrastructure.Shared.Models
{
    public class FullUserDataModel : UserDataModel
    {
        public string UserName { get; set; }
        
        public string Email { get; set; }

        public byte[] ProfilaImage { get; set; }
    }
}