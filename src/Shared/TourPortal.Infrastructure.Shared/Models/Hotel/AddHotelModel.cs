namespace TourPortal.Infrastructure.Shared.Models.Hotel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class AddHotelModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string HotelName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Address { get; set; }

        public Dictionary<string, ContactType> Contacts { get; set; } = new Dictionary<string, ContactType>();

        public string HotelImageUrl { get; set; }
    }
}