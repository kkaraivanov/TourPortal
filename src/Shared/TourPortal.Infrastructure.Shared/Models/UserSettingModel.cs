namespace TourPortal.Infrastructure.Shared.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserSettingModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string MidleName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string Sity { get; set; }

        public string Address { get; set; }

        public byte[] ProfilaImage { get; set; }
    }
}