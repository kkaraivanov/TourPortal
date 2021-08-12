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
        public async Task<ApplicationResponse<List<RoomResponse>>> GetRooms(string hotelId, int skip, int take)
        {
            var rooms = await _hotelService.GetRooms(hotelId);
            rooms = rooms.Skip(skip).Take(take).ToList();
            var roomsToResponse = await GetRoomsToRespone(rooms);

            return roomsToResponse.ToResponse();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<List<RoomResponse>>> GetRoomByRoomNumber(string hotelId, string roomNumber)
        {
            var rooms = await _hotelService.GetRooms(hotelId);
            var result = new List<Room>();
            foreach (var room in rooms)
            {
                var current = string.Concat(room.RoomNumber);
                if (current.StartsWith(roomNumber))
                {
                    result.Add(room);
                }
            }

            var roomsToResponse = await GetRoomsToRespone(result);

            return roomsToResponse.ToResponse();
        }

        private async Task<List<RoomResponse>> GetRoomsToRespone(ICollection<Room> rooms)
        {
            var roomsToResponse = new List<RoomResponse>();
            foreach (var room in rooms)
            {
                var serialize = JsonConvert.SerializeObject(room);
                var roomForResponse = JsonConvert.DeserializeObject<RoomResponse>(serialize);
                var images = await _hotelService.GetRoomImages(roomForResponse.Id);
                roomForResponse.RoomImages.AddRange(images.Select(x => x.ImageUrl));
                var roomType = await _hotelService.GetRoomType(room.Id);
                roomForResponse.RoomType = roomType;
                roomsToResponse.Add(roomForResponse);
            }

            return roomsToResponse;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<GetRoomByIdResponse>> GetRoom(string roomId)
        {
            var room = await _hotelService.GetRoom(roomId);
            if (room is null)
            {
                return new ApplicationResponse<GetRoomByIdResponse>(new ApplicationError("", "Room is not exist."));
            }

            var hotel = await _hotelService.GetHotelById(room.HotelId);
            var roomType = await _hotelService.GetRoomType(room.Id);
            var roomImages = await _hotelService.GetRoomImages(room.Id);
            var roomIsFree = await _hotelService.CheckRoomIsFree(room.Id);

            var response = new GetRoomByIdResponse
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                CountOfBeds = room.CountOfBeds,
                CountOfPersons = room.CountOfPersons,
                Price = room.Price,
                RoomType = roomType,
                RoomImages = roomImages.Select(x => x.ImageUrl).ToList(),
                HotelId = hotel.Id,
                HotelName = hotel.HotelName,
                City = hotel.City,
                Address = hotel.Address,
                IsFree = roomIsFree
            };

            return response.ToResponse();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<int>> GetRoomsCout(string hotelId)
        {
            var rooms = await _hotelService.GetRooms(hotelId);
            var count = rooms.Count;

            return count.ToResponse();
        }
            

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<HotelInfoResponse>> GetHotelCardInfo(string hotelId) =>
            await _hotelService.GetHotelInfoResponse(hotelId);
    }
}