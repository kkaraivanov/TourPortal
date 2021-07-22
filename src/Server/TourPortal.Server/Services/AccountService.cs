namespace TourPortal.Server.Services
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using IdentityModel;
    using Infrastructure.Services;
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddNewUser(
            string userName, 
            string email, 
            string password, 
            string phoneNumber, 
            string firstName, 
            string midleName,
            string lastName, 
            string roleName)
        {
            try
            {
                var applicationUser = _userManager.FindByNameAsync(userName).Result;

                if (applicationUser == null)
                {
                    applicationUser = new ApplicationUser
                    {
                        UserName = userName,
                        Email = email,
                        PhoneNumber = phoneNumber,
                        FirstName = firstName,
                        MidleName = midleName,
                        LastName = lastName,
                        EmailConfirmed = true
                    };

                    var result = _userManager.CreateAsync(applicationUser, password).Result;
                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);

                    result = _userManager.AddClaimsAsync(applicationUser, new Claim[]{
                        new Claim(JwtClaimTypes.Name, userName),
                        new Claim(JwtClaimTypes.GivenName, firstName),
                        new Claim(JwtClaimTypes.FamilyName, lastName),
                        new Claim(JwtClaimTypes.Email, email),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                    }).Result;

                    await _userManager.AddClaimAsync(applicationUser, new Claim($"Is{roleName}", "true"));
                    ApplicationUser user = await _userManager.FindByNameAsync(applicationUser.UserName);

                    try
                    {
                        result = await _userManager.AddToRolesAsync(user, new string[] { roleName });
                    }
                    catch
                    {
                        await _userManager.DeleteAsync(user);
                        throw;
                    }

                    if (!result.Succeeded)
                    {
                        await _userManager.DeleteAsync(user);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task AddUserProfile(string userId, string userName)
        {
            try
            {
                ApplicationUser user = await _userManager
                    .FindByNameAsync(userName);
                var userProfile = await _context
                    .UserProfiles
                    .FirstOrDefaultAsync(x => x.UserId == userId);

                if (userProfile != null)
                {
                    await _userManager.DeleteAsync(user);
                    throw new Exception("User has by deleted.");
                }
                else
                {
                    _context.UserProfiles.Add(new UserProfile
                    {
                        UserId = user.Id,
                        ApplicationUser = user,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                    });
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task AddUserToOwner(string userIde)
        {
            try
            {
                var userProfile = await _context
                    .UserProfiles
                    .FirstOrDefaultAsync(x => x.UserId == userIde);

                if (userProfile != null)
                {
                    _context.Owners.Add(new Owner
                    {
                        ProfileId = userProfile.Id,
                        Profile = userProfile
                    });

                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("User can't add to profile Owner.");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task AddUserToEmploye(string userIde, string ownerId)
        {
            try
            {
                var userProfile = await _context
                    .UserProfiles
                    .FirstOrDefaultAsync(x => x.UserId == userIde);
                var hotel = _context
                    .Hotels
                    .FirstOrDefault(x => x.OwnerId == ownerId);

                if (userProfile != null)
                {
                    _context.Employes.Add(new Employe
                    {
                        ProfileId = userProfile.Id,
                        Profile = userProfile,
                        HotelId = hotel.Id,
                        Hotel = hotel
                    });
                }
                else
                {
                    throw new Exception("User can't add to profile Employe.");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}