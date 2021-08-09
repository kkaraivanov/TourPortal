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

        Task AddUserToEmploye(string userId, string ownerId);

        Task<bool> ChangeUserData(
            string userId,
            string firstName,
            string midleName,
            string lastName,
            string phoneNumber,
            string sity,
            string address,
            byte[] profilaImage);

        Task<bool> ChangeUserPassword(string userId, string oldPassword, string newPassword);

        Task<bool> DeleteUserData(string userId);
    }
}