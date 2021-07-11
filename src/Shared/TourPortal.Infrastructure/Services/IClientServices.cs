namespace TourPortal.Infrastructure.Services
{
    using System.Threading.Tasks;
    using Shared.Models.Authentication;
    using Shared.Models.Response;

    public interface IClientServices
    {
        Task<ApplicationResponse<UserRolesRespons>> GetUserRoles();
    }
}