namespace TourPortal.Infrastructure.Shared.Models.Response
{
    using System.Collections.Generic;
    using Enums;

    public class HotelInfoResponse
    {
        public string Id { get; set; }

        public string HotelName { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public Dictionary<string, ContactType> Contacts { get; set; } = new Dictionary<string, ContactType>();

        public string HotelImageUrl { get; set; }
    }
}