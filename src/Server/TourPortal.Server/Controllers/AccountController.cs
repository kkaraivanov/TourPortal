namespace TourPortal.Server.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using IdentityModel;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Storage;

    [Route("api/[controller]")]
    public class AccountController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Administrator")]
        //[Authorize(Roles = "Employe")]
        //[Authorize(Policy = "IsUser")]
        //[Authorize(Policy = "IsAdministrator")]
        public async Task<IActionResult> TestToken()
        {
            return Ok("Hallo from account controller");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ApplicationResponse<RegisterResponseModel>> Register([FromBody] RegisterModel model)
        {
            var respons = new RegisterResponseModel();

            if (model is null)
            {
                return new ApplicationResponse<RegisterResponseModel>(new ApplicationError("", "Model can't by null"));
            }

            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                return new ApplicationResponse<RegisterResponseModel>(new ApplicationError("", "The following role types are invalid."));
            }

            try
            {
                var applicationUser = _userManager.FindByNameAsync(model.UserName).Result;

                if (applicationUser != null)
                {
                    return new ApplicationResponse<RegisterResponseModel>(new ApplicationError("", "The following username already exist."));
                }

                if (await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    return new ApplicationResponse<RegisterResponseModel>(new ApplicationError("", "The following email already exist."));
                }

                applicationUser = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    MidleName = model.MidleName,
                    LastName = model.LastName,
                    EmailConfirmed = true
                };

                var result = _userManager.CreateAsync(applicationUser, model.Password).Result;
                if (!result.Succeeded)
                    return new ApplicationResponse<RegisterResponseModel>(new ApplicationError("", result.Errors.First().Description));

                result = _userManager.AddClaimsAsync(applicationUser, new Claim[]{
                    new Claim(JwtClaimTypes.Name, model.UserName),
                    new Claim(JwtClaimTypes.Email, model.Email),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean)
                }).Result;

                await _userManager.AddClaimAsync(applicationUser, new Claim($"Is{role.Name}", "true"));
                ApplicationUser user = await _userManager.FindByNameAsync(applicationUser.UserName);
                respons.UserId = user.Id;

                try
                {
                    result = await _userManager.AddToRolesAsync(user, new string[] { role.Name});
                }
                catch
                {
                    await _userManager.DeleteAsync(user);
                    return new ApplicationResponse<RegisterResponseModel>(new ApplicationError("", $"Importing user in db error: User {user.UserName} has deleted."));
                }

                if (!result.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    return new ApplicationResponse<RegisterResponseModel>(new ApplicationError("", result.Errors.First().Description));
                }

                var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == user.Id);

                if (userProfile != null)
                {
                    await _userManager.DeleteAsync(user);
                    return new ApplicationResponse<RegisterResponseModel>(new ApplicationError("", "User profile has exist"));
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

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new ApplicationResponse<RegisterResponseModel>(new ApplicationError($"Importing {model.UserName} in db error:", e.Message));
            }

            return respons.ToResponse();
        }

        private async Task<IdentityResult> CreateApplicationUser(
            string userName, 
            string email, 
            string password,
            string phoneNumber, 
            string firstName, 
            string midleName, 
            string lastName)
        {
            var applicationUser = _userManager.FindByNameAsync(userName).Result;

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

            return _userManager.CreateAsync(applicationUser, password).Result;
        }

        [HttpPost]
        [Authorize(Roles = "Owner")]
        [Route("[action]")]
        public async Task<ApplicationResponse<RegisterResponseModel>> RegisterEmploye([FromBody] RegisterModel model) =>
            await Register(model);

        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ApplicationResponse<UserRolesRespons>> GetUserRoles()
        {
            var response = new UserRolesRespons();
            response.UserRoles = _roleManager
                .Roles
                .Select(r => new UserRoleModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                })
                .ToList();

            return new ApplicationResponse<UserRolesRespons>(response);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<UserInfoResponse>> GetUserInfo(string userEmail)
        {
            if (userEmail is null)
            {
                return new ApplicationResponse<UserInfoResponse>(new ApplicationError("", "Parameter can't by null"));
            }

            var user = await _userManager
                .FindByEmailAsync(userEmail);

            if (user is null)
            {
                return new ApplicationResponse<UserInfoResponse>(new ApplicationError("", "User can't by null"));
            }

            var roles = _context.UserRoles
                .Where(x => x.UserId == user.Id);

            if (!roles.Any())
            {
                return new ApplicationResponse<UserInfoResponse>(new ApplicationError("", "Roles can't by empty"));
            }

            var role = await _roleManager
                .FindByIdAsync(roles.FirstOrDefault()?.RoleId);
            var response = new UserInfoResponse
            {
                Id = user.Id,
                ProfileImage = "",
                ProfileName = $"{user.FirstName} {user.LastName}",
                UserRole = role.Name,
            };

            return new ApplicationResponse<UserInfoResponse>(response);
        }
    }
}