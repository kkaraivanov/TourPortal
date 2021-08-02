namespace TourPortal.Server.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using IdentityModel;
    using Infrastructure.Global.Types;
    using Infrastructure.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Storage;

    public class AccountController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IAccountService _accountService;

        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, 
            IAccountService accountService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
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
    }
}