namespace TourPortal.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Global.Types;
    using Infrastructure.Shared.Enums;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Storage;

    public class UserController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = Security.Role.Owner)]
        public async Task<ApplicationResponse<HotelInfoResponse>> GetHotelInfo()
        {
            var userId = _userManager.GetUserId(User);
            var ownerId = _context.Owners
                .FirstOrDefault(x => x.Profile.UserId == userId)?.Id;
            var hotelId = _context.Hotels
                .FirstOrDefault(x => x.OwnerId == ownerId)
                ?.Id;

            return await GetHotelCardInfo(hotelId);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ApplicationResponse<HotelInfoResponse>> GetHotelCardInfo(string hotelId)
        {
            var hotel = _context.Hotels
                .FirstOrDefault(x => x.Id == hotelId);

            if (hotel != null)
            {
                var contacts = new Dictionary<string, ContactType>();
                var contactsQuery = _context.Contacts
                    .Where(x => x.HotelId == hotel.Id)
                    .Select(x => new Contact
                    {
                        ContactString = x.ContactString,
                        ContactType = x.ContactType
                    });

                foreach (var contact in contactsQuery)
                {
                    contacts[contact.ContactString] = contact.ContactType;
                }

                var response = new HotelInfoResponse
                {
                    Id = hotel.Id,
                    HotelName = hotel.HotelName,
                    City = hotel.City,
                    Address = hotel.Address,
                    Contacts = contacts,
                    HotelImageUrl = hotel.HotelImageUrl
                };

                return response.ToResponse();
            }

            return new ApplicationResponse<HotelInfoResponse>();
        }
    }
}