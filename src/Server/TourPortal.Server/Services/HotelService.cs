namespace TourPortal.Server.Services
{
    using System.Collections.Generic;
    using System.Formats.Asn1;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Shared.Enums;
    using Infrastructure.Shared.Models.Hotel;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Identity;
    using Storage;

    public interface IHotelService
    {
        Owner GetOwner(string userId);

        string GetHotelId(string userId);

        Task<Hotel> GetHotelById(string hotelId);

        Task<Hotel> GetHotelByOwnerId(string ownerId);

        Task<HotelInfoResponse> GetHotelInfo(string hotelId);

        Task<Hotel> AddNewHotel(AddHotelModel hotelModel, Owner owner);

        Task AddNewHotlContacts(List<Contact> contacts);
    }

    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext _context;

        public HotelService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
        }

        public Owner GetOwner(string userId) =>
            _context.Owners.FirstOrDefault(x => x.Profile.UserId == userId);

        public string GetHotelId(string userId) =>
            _context.Hotels.FirstOrDefault(x => x.OwnerId == GetOwnerId(userId))?.Id;

        public async Task<Hotel> GetHotelById(string hotelId) =>
            _context.Hotels.FirstOrDefault(x => x.Id == hotelId);

        public async Task<Hotel> GetHotelByOwnerId(string ownerId) =>
            _context.Hotels.FirstOrDefault(x => x.Id == GetHotelId(ownerId));

        public async Task<HotelInfoResponse> GetHotelInfo(string hotelId)
        {
            var hotel = await GetHotelById(hotelId);
            var contacts = new Dictionary<string, ContactType>();
            if (hotel != null)
            {
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
            }

            return new HotelInfoResponse
            {
                Id = hotel.Id,
                HotelName = hotel.HotelName,
                City = hotel.City,
                Address = hotel.Address,
                Contacts = contacts,
                HotelImageUrl = hotel.HotelImageUrl
            };
        }

        public async Task<Hotel> AddNewHotel(AddHotelModel hotelModel, Owner owner)
        {
            var hotel = new Hotel
            {
                OwnerId = owner.Id,
                Owner = owner,
                HotelName = hotelModel.HotelName,
                City = hotelModel.City,
                Address = hotelModel.Address,
                HotelImageUrl = hotelModel.HotelImageUrl
            };
            
            _context.Hotels.Add(hotel);
             _context.SaveChanges();
            var result = _context.Hotels.FirstOrDefault(x => x.OwnerId == owner.Id);

            owner.Hotel = result;
            _context.Owners.Update(owner);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task AddNewHotlContacts(List<Contact> contacts)
        {
            await _context.AddRangeAsync(contacts);
            await _context.SaveChangesAsync();
        }

        private string GetOwnerId(string userId) =>
            GetOwner(userId)?.Id;
    }
}