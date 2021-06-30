namespace TourPortal.Storage
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using IdentityModel;
    using Infrastructure.GlobalTypes;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using Infrastructure.Storage.Models;
    using Infrastructure.Storage;

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<DatabaseInitializer> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            await Migrate();

            await SeedApplicationUser();
        }

        private async Task Migrate()
        {
            await _context.Database.MigrateAsync();
        }

        private async Task SeedApplicationUser()
        {
            var roles = await _roleManager.FindByNameAsync(Security.Role.Administrator);

            if (roles == null)
            {
                _logger.LogError("The following role types are invalid: ");
                throw new Exception("The following role types are invalid: ");
            }

            try
            {
                if (await _userManager.FindByNameAsync(ApplicationConstants.DefaultAdministratorName) == null)
                {
                    await CreateUser(
                        ApplicationConstants.DefaultAdministratorName,
                        ApplicationConstants.DefaultAdministratorPassword,
                        "Application",
                        "",
                        "Administrator",
                        "admin@test.com",
                        "555 555 555",
                        new string[]{roles.ToString()});
                }
                else
                {
                    _logger.LogInformation("User {0} exist in db. Seed to database return.", ApplicationConstants.DefaultAdministratorName);
                    return;
                }

                var user = await _userManager.FindByNameAsync(ApplicationConstants.DefaultAdministratorName);
                var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == user.Id);

                if (userProfile == null)
                {
                    _context.UserProfiles.Add(new UserProfile
                    {
                        UserId = user.Id,
                        ApplicationUser = user,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                    });
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Importing administrator in db error: {0}", e.GetBaseException().Message);
            }
        }

        private async Task<ApplicationUser> CreateUser(
            string userName,
            string password, 
            string firstName, 
            string midleName, 
            string lastName, 
            string email, 
            string phoneNumber, 
            string[] roles)
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
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.PhoneNumber, phoneNumber)
                    }).Result;

                foreach (var role in roles.Distinct())
                {
                    await _userManager.AddClaimAsync(applicationUser, new Claim($"Is{role}", "true"));
                }

                ApplicationUser user = await _userManager.FindByNameAsync(applicationUser.UserName);
                
                try
                {
                    result = await _userManager.AddToRolesAsync(user, roles.Distinct());
                }
                catch
                {
                    await _userManager.DeleteAsync(user);
                    _logger.LogError("Importing user in db error: {0} User {1} has deleted.", Environment.NewLine, user.UserName);
                    throw;
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);
                var index = 1;

                _logger.LogInformation("Add user {0} to roles: {1}{2}",
                    user.UserName, Environment.NewLine, string.Join(
                        Environment.NewLine,
                        userRoles.Select(r =>
                            $"\t ({index++}) {r}")));

                index = 1;

                _logger.LogInformation("Added Claims for user: {0}{1}{2}",
                    user.UserName, Environment.NewLine, string.Join(
                        Environment.NewLine,
                        claims.Select(c =>
                            $"\t ({index++}) {c.Type} - {c.Value}")));

                if (!result.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                }
            }

            _logger.LogInformation("Importing {0} in db has completed", applicationUser.UserName);
            return applicationUser;
        }
    }
}