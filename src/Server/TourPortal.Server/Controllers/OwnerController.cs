namespace TourPortal.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure.Global.Types;
    using Infrastructure.Services;
    using Infrastructure.Shared.Models;
    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Hotel;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Services;
    using Storage;

    [Authorize(Roles = Security.Role.Owner)]
    public class OwnerController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IAccountService _accountService;
        private readonly IHotelService _hotelService;
        private readonly IUserService _userService;

        public OwnerController(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager, 
            IAccountService accountService, IHotelService hotelService, 
            IUserService userService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
            _hotelService = hotelService;
            _userService = userService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ApplicationResponse<RegisterResponseModel>> RegisterEmploye([FromBody] RegisterModel model)
        {
            if (model is null || !ModelState.IsValid)
            {
                return ModelStateErrors<RegisterResponseModel>();
            }
            
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
            _context.SaveChanges();
            Thread.Sleep(500);
            await _accountService.AddUserToEmploye(user.Id, _userManager.GetUserId(User));
            _context.SaveChanges();

            return respons.ToResponse();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ApplicationResponse<HotelInfoResponse>> AddNewHotel([FromBody] AddHotelModel hotelModel)
        {
            if (hotelModel is null || !ModelState.IsValid)
            {
                return ModelStateErrors<HotelInfoResponse>();
            }

            var userId = _userManager.GetUserId(User);
            var owner = _hotelService.GetOwner(userId);
            var hotel = await _hotelService.GetHotelByOwnerId(owner.Id);

            if (hotel != null)
            {
                return new ApplicationResponse<HotelInfoResponse>(new ApplicationError("", $"Hotel {hotel.HotelName} already exists."));
            }

            var hotelName = hotelModel.HotelName;
            hotel = await _hotelService.AddNewHotel(hotelModel, owner);
            
            if (hotel == null)
            {
                return new ApplicationResponse<HotelInfoResponse>(new ApplicationError("", $"Hotel {hotelName} was not created."));
            }

            var contacts = new List<Contact>();
            foreach (var (contact, contactType) in hotelModel.Contacts)
            {
                var newContact = new Contact
                {
                    ContactString = contact,
                    ContactType = contactType,
                    HotelId = hotel.Id
                };

                contacts.Add(newContact);
            }

            await _hotelService.AddNewHotlContacts(contacts);
            var respone = await _hotelService.GetHotelInfo(hotel.Id);

            return respone.ToResponse();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ApplicationResponse<bool>> ChangeHotel([FromBody] ChangeHotelModel hotelModel)
        {
            if (hotelModel is null || !ModelState.IsValid)
            {
                return ModelStateErrors<bool>();
            }

            var respons = await _hotelService.ChangeHotel(hotelModel);

            return respons.ToResponse();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ApplicationResponse<bool>> AddNewRoom([FromBody] RoomModel roomModel)
        {
            if (roomModel is null || !ModelState.IsValid)
            {
                return ModelStateErrors<bool>();
            }
            
            var hotel = await _hotelService.GetHotelByOwnerId(UserId);
            var rooms = await _hotelService.GetRooms(hotel.Id);
            var roomExsist = rooms.Any(x => x.RoomNumber == roomModel.RoomNumber);

            if (roomExsist)
            {
                return new ApplicationResponse<bool>(new ApplicationError("", $"Room number {roomModel.RoomNumber} already exist."));
            }

            var response = await _hotelService.AddNewRooms(roomModel, hotel);

            return response.ToResponse();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<ICollection<string>>> GetRoomTypes() =>
            new ApplicationResponse<ICollection<string>>(await _hotelService.GetRoomTypes());

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<List<GetEmployeResponse>>> GetEmployes()
        {
            var hotelId = _hotelService.GetHotelId(UserId);
            var employes = await _userService.GetEmployes(hotelId);
            var serialize = JsonConvert.SerializeObject(employes);
            var response = JsonConvert.DeserializeObject<List<GetEmployeResponse>>(serialize);

            return response.ToResponse();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<FullUserDataModel>> GetEmploye(string employeId)
        {
            var employe = await _userService.GetEmployeData(employeId);

            if (employe is null)
            {
                return new ApplicationResponse<FullUserDataModel>(new ApplicationError("", "Employe data is null."));
            }
            return employe.ToResponse();
        }

        private string UserId =>
            _userManager.GetUserId(User);
    }
}