namespace TourPortal.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Shared.Models;
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

        Task<ApplicationResponse<HotelInfoResponse>> GetHotelCardInfo(string hotelId);

        Task<ApplicationResponse<RegisterResponseModel>> RegisterEmploye(RegisterModel registerModel);

        Task<ApplicationResponse<HotelInfoResponse>> AddNewHotel(AddHotelModel hotelModel);

        Task<ApplicationResponse<bool>> ChangeHotel(ChangeHotelModel hotelModel);

        Task<ApplicationResponse<bool>> AddNewRoom(RoomModel roomModel);

        Task<ApplicationResponse<IEnumerable<RoomResponse>>> GetRooms(string hotelId, int skip, int take);

        Task<ApplicationResponse<GetRoomByIdResponse>> GetRoom(string roomId);

        Task<ApplicationResponse<int>> GetRoomsCount(string hotelId);

        Task<ApplicationResponse<ICollection<string>>> GetRoomTypes();

        Task<ApplicationResponse<EmployeInfoResponse>> GetEmployeInfo();

        Task<ApplicationResponse<IEnumerable<GetEmployeResponse>>> GetEmployes();

        Task<ApplicationResponse<FullUserDataModel>> GetEmploye(string employeId);

        Task<ApplicationResponse<FullUserDataModel>> GetUserData();

        Task<ApplicationResponse<bool>> ChangeUserData(UserSettingModel model);
    }
}