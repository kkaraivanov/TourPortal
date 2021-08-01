namespace TourPortal.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Shared.Models.Authentication;
    using Shared.Models.Hotel;
    using Shared.Models.Response;

    public interface IApiService
    {
        Task<ApplicationResponse<LoginResponseModel>> Login(LoginModel loginModel);

        Task Logout();

        Task<ApplicationResponse<RegisterResponseModel>> Register(RegisterModel registerModel);

        Task<ApplicationResponse<UserRolesRespons>> GetUserRoles();

        Task<ApplicationResponse<UserInfoResponse>> GetUserInfo(string userEmail);

        Task<ApplicationResponse<HotelInfoResponse>> GetHotelInfo();

        Task<ApplicationResponse<HotelInfoResponse>> AddNewHotel(AddHotelModel hotelModel);

        Task<ApplicationResponse<bool>> ChangeHotel(ChangeHotelModel hotelModel);

        Task<ApplicationResponse<bool>> AddNewRoom(AddNewRoomModel roomModel);

        Task<ApplicationResponse<IEnumerable<RoomResponse>>> GetRooms(string hotelId, int skip, int take);

        Task<ApplicationResponse<int>> GetRoomsCount(string hotelId);

        Task<ApplicationResponse<ICollection<string>>> GetRoomTypes();
    }
}