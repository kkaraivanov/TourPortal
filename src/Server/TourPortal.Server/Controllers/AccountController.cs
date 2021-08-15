namespace TourPortal.Server.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Newtonsoft.Json;

    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Infrastructure.Shared.Models;
    using IdentityModel;
    using Infrastructure.Global.Types;
    using Infrastructure.Services;
    using Services;
    using Storage;

    public class AccountController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, 
            IAccountService accountService, 
            IUserService userService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ApplicationResponse<RegisterResponseModel>> Register([FromBody] RegisterModel model)
        {
            if (model is null || !ModelState.IsValid)
            {
                return ModelStateErrors<RegisterResponseModel>();
            }

            var respons = new RegisterResponseModel();
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if (role is null)
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

                await _accountService.AddNewUser(
                    model.UserName,
                    model.Email,
                    model.Password,
                    model.PhoneNumber,
                    model.FirstName,
                    model.MidleName,
                    model.LastName,
                    role.Name);
                
                ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
                respons.UserId = user.Id;
                await _accountService.AddUserProfile(user.Id, model.UserName);

                _context.SaveChanges();

                if (role.Name.Equals(Security.Role.Owner))
                {
                    await _accountService.AddUserToOwner(user.Id);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return new ApplicationResponse<RegisterResponseModel>(new ApplicationError($"Importing {model.UserName} in db error:", e.Message));
            }

            return respons.ToResponse();
        }

        [HttpPost]
        [Authorize(Roles = Security.Role.Owner)]
        [Route("[action]")]
        public async Task<ApplicationResponse<RegisterResponseModel>> RegisterEmploye([FromBody] RegisterModel model) =>
            await Register(model);

        [HttpPost]
        [Route("[action]")]
        public async Task<ApplicationResponse<bool>> ChangeUserData([FromBody] UserSettingModel model)
        {
            if (model is null || !ModelState.IsValid)
            {
                return ModelStateErrors<bool>();
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user is null)
            {
                return new ApplicationResponse<bool>(new ApplicationError("", "Incorect user."));
            }

            var userId = user.Id;
            if (model.Id != userId)
            {
                return new ApplicationResponse<bool>(new ApplicationError("", "Incorect user."));
            }

            var serialize = JsonConvert.SerializeObject(model);
            user = JsonConvert.DeserializeObject<ApplicationUser>(serialize);
            var profile = JsonConvert.DeserializeObject<UserProfile>(serialize);
            var updateUser = await _accountService.ChangeUserData(
                userId,
                user.FirstName,
                user.MidleName,
                user.LastName,
                user.PhoneNumber,
                profile.Sity,
                profile.Address,
                profile.ProfilaImage);

            if (!updateUser)
            {
                return new ApplicationResponse<bool>(new ApplicationError("", "User can't by update."));
            }

            if (!string.IsNullOrEmpty(model.OldPassword) && !string.IsNullOrEmpty(model.NewPassword))
            {
                var paswordChanged = await _accountService.ChangeUserPassword(userId, model.OldPassword, model.NewPassword);
                if (!paswordChanged)
                {
                    return new ApplicationResponse<bool>(new ApplicationError("", "Incorect password."));
                }
            }

            return true.ToResponse();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ApplicationResponse<bool>> DeleteUserData([FromBody] UserSettingModel model)
        {
            if (model is null || !ModelState.IsValid)
            {
                return ModelStateErrors<bool>();
            }

            var userId = _userManager.GetUserId(User);
            if (userId != model.Id)
            {
                return new ApplicationResponse<bool>(new ApplicationError("", $"User {model.UserName} does not match this account."));
            }

            var result = await _accountService.DeleteUserData(userId);
            return result.ToResponse();
        }

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
            if (string.IsNullOrEmpty(userEmail))
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
            var profile = _context.UserProfiles.FirstOrDefault(x => x.UserId == user.Id);
            var response = new UserInfoResponse
            {
                Id = user.Id,
                ProfileImage = profile.ProfilaImage,
                ProfileName = $"{user.FirstName} {user.LastName}",
                UserRole = role.Name,
            };

            return new ApplicationResponse<UserInfoResponse>(response);
        }

        private async Task CreateApplicationUser(
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

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<FullUserDataModel>> GetUserData()
        {
            var userId = _userManager.GetUserId(User);
            var userData = await _userService.GetFullUserData(userId);

            if (userData is null)
            {
                return new ApplicationResponse<FullUserDataModel>(new ApplicationError("", "User data is null!"));
            }

            return userData.ToResponse();
        }
    }
}