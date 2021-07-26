namespace TourPortal.Server.Controllers
{
    using System.Threading.Tasks;
    using Infrastructure.Global.Types;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Storage;

    [Authorize(Policy = Security.Policiy.IsUser)]
    public class ApplicationUserController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IHotelService _hotelService;

        public ApplicationUserController(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager, 
            IHotelService hotelService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _hotelService = hotelService;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = Security.Role.Owner)]
        public async Task<ApplicationResponse<HotelInfoResponse>> GetHotelInfo()
        {
            var userId = _userManager.GetUserId(User);
            var hotelId = _hotelService.GetHotelId(userId);

            if (string.IsNullOrEmpty(hotelId))
            {
                return new ApplicationResponse<HotelInfoResponse>(new HotelInfoResponse());
            }

            return await _hotelService.GetHotelInfoResponse(hotelId);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<HotelInfoResponse>> GetHotelCardInfo(string hotelId) =>
            await _hotelService.GetHotelInfoResponse(hotelId);
    }
}