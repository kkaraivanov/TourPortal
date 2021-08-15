namespace TourPortal.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Infrastructure.Shared.Models.Hotel;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;

    public interface IHotelService
    {
        Owner GetOwner(string userId);

        string GetHotelId(string userId);

        Task<Hotel> GetHotelById(string hotelId);

        Task<Hotel> GetHotelByOwnerId(string ownerId);

        Task<HotelInfoResponse> GetHotelInfo(string hotelId);

        Task<ApplicationResponse<HotelInfoResponse>> GetHotelInfoResponse(string hotelId);

        Task<Hotel> AddNewHotel(AddHotelModel hotelModel, Owner owner);

        Task AddNewHotlContacts(List<Contact> contacts);

        Task UpdateHotlContacts(List<Contact> contacts, string hotelId);

        Task<bool> AddNewRoom(RoomModel roomModel, Hotel hotel);

        Task<bool> ChangeRoom(RoomModel roomModel, Hotel hotel);

        Task<ICollection<Room>> GetRooms(string hotelId);

        Task<ICollection<RoomImages>> GetRoomImages(string roomId);

        Task<ICollection<string>> GetRoomTypes();

        Task<string> GetRoomType(string roomId);

        Task<bool> ChangeHotel(ChangeHotelModel hotelModel);

        Task<Room> GetRoom(string roomId);

        Task<bool> CheckRoomIsFree(string roomId);
    }
}