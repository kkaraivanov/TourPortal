namespace TourPortal.Infrastructure.Services
{
    using System.Threading.Tasks;

    using Shared.Models.Authentication;
    using Shared.Models.Response;

    public interface IApiService
    {
        Task<ApplicationResponse<LoginResponseModel>> Login(LoginModel loginModel);

        Task Logout();

        Task<ApplicationResponse<RegisterResponseModel>> Register(RegisterModel registerModel);

        Task<ApplicationResponse<UserRolesRespons>> GetUserRoles();

        Task<ApplicationResponse<UserInfoResponse>> GetUserInfo(string userEmail);

        Task<ApplicationResponse<HotelInfoResponse>> GetHotelInfo();
    }
}