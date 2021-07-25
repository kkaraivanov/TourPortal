namespace TourPortal.Server.Controllers
{
    using Infrastructure.Global.Types;
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Services;
    using Storage;

    [Authorize(Policy = Security.Policiy.IsUser)]
    public class ApplicationUserControler : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IHotelService _hotelService;

        public ApplicationUserControler(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IHotelService hotelService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _hotelService = hotelService;
        }
    }
}