namespace TourPortal.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Global.Types;
    using Infrastructure.Services;
    using Infrastructure.Shared.Models.Authentication;
    using Infrastructure.Shared.Models.Hotel;
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

        [HttpPost]
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

        [HttpPost]
        [Route("[action]")]
        public async Task<ApplicationResponse<HotelInfoResponse>> AddNewHotel([FromBody] AddHotelModel hotelModel)
        {
            ModelStateErrors<AddHotelModel>();

            if (hotelModel == null)
            {
                return new ApplicationResponse<HotelInfoResponse>(new ApplicationError("", "Hotel model can't be null."));
            }

            var userId = _userManager.GetUserId(User);
            var owner = _context.Owners
                .FirstOrDefault(x => x.Profile.UserId == userId);
            var hotel = _context.Hotels
                .FirstOrDefault(x => x.OwnerId == owner.Id);

            if (hotel != null)
            {
                return new ApplicationResponse<HotelInfoResponse>(new ApplicationError("", $"Hotel {hotel.HotelName} already exists."));
            }

            hotel = new Hotel
            {
                OwnerId = owner.Id,
                Owner = owner,
                HotelName = hotelModel.HotelName,
                City = hotelModel.City,
                Address = hotelModel.Address,
                HotelImageUrl = hotelModel.HotelImageUrl
            };

            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();

            var hotelName = hotel.HotelName;
            hotel = _context.Hotels
                .FirstOrDefault(x => x.OwnerId == owner.Id);

            if (hotel == null)
            {
                return new ApplicationResponse<HotelInfoResponse>(new ApplicationError("", $"Hotel {hotelName} was not created."));
            }

            foreach (var (contact, contactType) in hotelModel.Contacts)
            {
                var newContact = new Contact
                {
                    ContactString = contact,
                    ContactType = contactType
                };

                hotel.Contacts.Add(newContact);
            }

            owner.Hotel = hotel;
            _context.Owners.Update(owner);
            await _context.SaveChangesAsync();


            var respone = new HotelInfoResponse
            {
                Id = hotel.Id,
                HotelName = hotel.HotelName,
                Address = hotel.Address,
                City = hotel.City,
                Contacts = hotelModel.Contacts,
                HotelImageUrl = hotel.HotelImageUrl
            };

            return respone.ToResponse();
        }
    }
}