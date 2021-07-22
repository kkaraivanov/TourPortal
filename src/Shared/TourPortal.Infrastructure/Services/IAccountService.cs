namespace TourPortal.Infrastructure.Services
{
    using System.Threading.Tasks;

    public interface IAccountService
    {
        Task AddNewUser(
            string userName,
            string email,
            string password,
            string phoneNumber,
            string firstName,
            string midleName,
            string lastName,
            string roleName);

        Task AddUserProfile(string userId, string userName);

        Task AddUserToOwner(string userIde);

        Task AddUserToEmploye(string userIde, string ownerId);
    }
}