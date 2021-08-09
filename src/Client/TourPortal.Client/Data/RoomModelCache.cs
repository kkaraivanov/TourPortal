namespace TourPortal.Client.Data
{
    using Infrastructure.Shared.Models.Hotel;
    using Services;

    public class RoomModelCache : IRoomCache
    {
        public string RoomId { get; set; }

        public RoomModel RoomModel { get; set; }
    }
}