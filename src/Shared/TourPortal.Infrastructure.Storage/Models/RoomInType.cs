namespace TourPortal.Infrastructure.Storage.Models
{
    public class RoomInType
    {
        public string RoomId { get; set; }

        public Room Room { get; set; }

        public string RoomTypeId { get; set; }

        public RoomType RoomType { get; set; }
    }
}