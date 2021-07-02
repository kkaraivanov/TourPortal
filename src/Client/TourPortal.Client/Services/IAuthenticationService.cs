namespace TourPortal.Client.Services
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Authentication;
    using Infrastructure.Models.Response;

    public interface IAuthenticationService
    {
        Task<ApplicationResponse<LoginResponseModel>> Login(LoginModel loginModel);

        Task Logout();

        Task<ApplicationResponse<RegisterResponseModel>> Register(RegisterModel registerModel);
    }
}