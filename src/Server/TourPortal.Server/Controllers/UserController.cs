namespace TourPortal.Server.Controllers
{
    using Infrastructure.Global.Types;
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Services;
    using Storage;

    [Authorize(Roles = Security.Role.User)]
    public class UserController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHotelService _hotelService;

        public UserController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, IHotelService hotelService)
        {
            _context = context;
            _userManager = userManager;
            _hotelService = hotelService;
        }

        
    }
}