namespace TourPortal.Client.Services
{
    using System.Threading.Tasks;

    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Response;

    public interface IAuthenticationService
    {
        Task<ApplicationResponse<LoginResponseModel>> Login(LoginModel loginModel);

        Task Logout();

        Task<ApplicationResponse<RegisterResponseModel>> Register(RegisterModel registerModel);
    }
}