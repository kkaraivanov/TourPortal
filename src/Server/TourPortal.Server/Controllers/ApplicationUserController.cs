namespace TourPortal.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Global.Types;
    using Infrastructure.Shared.Models.Hotel;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
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
        public async Task<ApplicationResponse<List<RoomResponse>>> GetRooms(string hotelId)
        {
            var rooms = await _hotelService.GetRooms(hotelId);
            var response = new List<RoomResponse>();
            foreach (var room in rooms)
            {
                var serialize = JsonConvert.SerializeObject(room);
                var roomForResponse = JsonConvert.DeserializeObject<RoomResponse>(serialize);
                var images = await _hotelService.GetRoomImages(roomForResponse.Id);
                roomForResponse.RoomImages.AddRange(images.Select(x => x.ImageUrl));
                response.Add(roomForResponse);
            }

            return response.ToResponse();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<HotelInfoResponse>> GetHotelCardInfo(string hotelId) =>
            await _hotelService.GetHotelInfoResponse(hotelId);
    }
}