namespace TourPortal.Client.Services
{
    using Infrastructure.Shared.Models.Hotel;

    public interface IRoomCache
    {
        public string RoomId { get; set; }

        public RoomModel RoomModel { get; set; }
    }
}