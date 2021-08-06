namespace TourPortal.Infrastructure.Shared.Models.Hotel
{
    public class GetRoomByIdResponse : RoomModel
    {
        public string HotelId { get; set; }

        public string HotelName { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public bool IsFree { get; set; }
    }
}