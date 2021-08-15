namespace TourPortal.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    using Infrastructure.Global.Types;
    using Infrastructure.Services;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Services;
    using Storage;

    [Authorize(Roles = Security.Role.Employe)]
    public class EmployeController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHotelService _hotelService;
        private readonly IAccountService _accountService;

        public EmployeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, 
            IHotelService hotelService, 
            IAccountService accountService)
        {
            _userManager = userManager;
            _hotelService = hotelService;
            _accountService = accountService;
            _context = context;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<EmployeInfoResponse>> GetInfo()
        {
            var userId = _userManager.GetUserId(User);
            var employe = _context.Employes
                .FirstOrDefault(x => x.Profile.UserId == userId);

            var serialize = JsonConvert.SerializeObject(employe);
            var response = JsonConvert.DeserializeObject<EmployeInfoResponse>(serialize);

            return response.ToResponse();
        }

        
    }
}