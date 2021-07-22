namespace TourPortal.Infrastructure.Shared.Models.HotelRequestResponse
{
    using System;

    public class SearchFreeHotelsRequest
    {
        public DateTime ChecIn { get; set; } = DateTime.Now;

        public DateTime ChecOut { get; set; } = DateTime.Now;

        public int Persons { get; set; }
    }
}