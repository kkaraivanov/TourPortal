namespace TourPortal.Client.Data
{
    using System.Collections.Generic;

    public class HotelInfoModel
    {
        public string Id { get; set; }

        public string HotelName { get; set; }

        public string Sity { get; set; }

        public string Address { get; set; }

        public List<string> Contacts { get; set; } = new List<string>();

        public string HotelImageUrl { get; set; }
    }
}