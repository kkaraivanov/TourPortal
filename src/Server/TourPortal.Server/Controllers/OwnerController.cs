namespace TourPortal.Server.Controllers
{
    using System.Threading.Tasks;
    using Infrastructure.Global.Types;
    using Infrastructure.Services;
    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Storage;

    [Authorize(Roles = Security.Role.Owner)]
    public class OwnerController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IAccountService _accountService;

        public OwnerController(
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

        [Route("[action]")]
        public async Task<ApplicationResponse<RegisterResponseModel>> RegisterEmploye([FromBody] RegisterModel model)
        {
            ModelStateErrors<RegisterModel>();
            var respons = new RegisterResponseModel();
            var role = await _roleManager.FindByIdAsync(model.RoleId);
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
            await _accountService.AddUserToEmploye(user.Id, _userManager.GetUserId(User));
            _context.SaveChanges();

            return respons.ToResponse();
        }


    }
}