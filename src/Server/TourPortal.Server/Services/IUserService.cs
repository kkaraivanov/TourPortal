namespace TourPortal.Server.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Shared.Models;
    using Infrastructure.Storage.Models;
    using Storage;

    public interface IUserService
    {
        Task<FullUserDataModel> GetFullUserData(string userId);

        Task<FullUserDataModel> GetEmployeData(string employeId);

        Task<IEnumerable<UserDataModel>> GetEmployes(string hotelId);
    }

    class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FullUserDataModel> GetFullUserData(string userId) =>
            await Task.Run(() => _context.Users
                .Where(x => x.Id == userId)
                .Select(x => ToFullDataModel(_context, x))
                .FirstOrDefault());

        public async Task<FullUserDataModel> GetEmployeData(string employeId) =>
            await Task.Run(() => _context.Users
                .Where(x => x.Profile.Employe.Id == employeId)
                .Select(x => ToFullDataModel(_context, x))
                .FirstOrDefault());

        public async Task<IEnumerable<UserDataModel>> GetEmployes(string hotelId) =>
            await Task.Run(() => _context.Employes
                 .Where(e => e.HotelId == hotelId)
                 .SelectMany(e => _context.UserProfiles.Where(up => up.Id == e.ProfileId))
                 .SelectMany(up => _context.Users
                      .Where(au => au.Profile.Id == up.Id && au.IsDeleted == false)
                      .Select(x => new UserDataModel
                      {
                          Id = up.Employe.Id,
                          FirstName = x.FirstName,
                          MidleName = x.MidleName,
                          LastName = x.LastName,
                          PhoneNumber = x.PhoneNumber,
                          IsDeleted = x.IsDeleted,
                          Sity = up.Sity,
                          Address = up.Address,
                          CreatedOn = up.CreatedOn,
                          ModifiedOn = up.ModifiedOn
                      }))
                 .ToList());

        private readonly Func<ApplicationDbContext, ApplicationUser, FullUserDataModel> ToFullDataModel = (context, user) =>
                context.Users
                .Where(x => x.Id == user.Id)
                .Select(x => new FullUserDataModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    MidleName = x.MidleName,
                    LastName = x.LastName,
                    Sity = x.Profile.Sity,
                    Address = x.Profile.Address,
                    PhoneNumber = x.PhoneNumber,
                    ProfilaImage = x.Profile.ProfilaImage,
                    CreatedOn = x.Profile.CreatedOn
                })
                .FirstOrDefault();
    }
}