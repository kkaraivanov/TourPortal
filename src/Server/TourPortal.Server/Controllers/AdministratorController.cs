namespace TourPortal.Server.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Global.Types;
    using Infrastructure.Services;
    using Infrastructure.Shared.Models;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Storage;

    [Authorize(Roles = Security.Role.Administrator)]
    public class AdministratorController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;

        public AdministratorController(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            IAccountService accountService)
        {
            _context = context;
            _userManager = userManager;
            _accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ApplicationResponse<bool>> DeleteUser([FromBody] DeletableUserModel model)
        {
            if (model is null || !ModelState.IsValid)
            {
                return ModelStateErrors<bool>();
            }

            var userId = _userManager.GetUserId(User);


            var result = await _accountService.DeleteUserData(userId);
            return result.ToResponse();
        }
    }
}