namespace TourPortal.Infrastructure.Shared.Models.Hotel
{
    using System.Collections.Generic;

    public class AddNewRoomModel
    {
        public int RoomNumber { get; set; }

        public int CountOfBeds { get; set; }

        public int CountOfPersons { get; set; }

        public decimal Price { get; set; }

        public string RoomType { get; set; }

        public List<string> RoomImages { get; set; } = new List<string>();
    }
}