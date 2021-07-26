namespace TourPortal.Server.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Shared.Enums;
    using Infrastructure.Shared.Models.Hotel;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public interface IHotelService
    {
        Owner GetOwner(string userId);

        string GetHotelId(string userId);

        Task<Hotel> GetHotelById(string hotelId);

        Task<Hotel> GetHotelByOwnerId(string ownerId);

        Task<HotelInfoResponse> GetHotelInfo(string hotelId);

        Task<ApplicationResponse<HotelInfoResponse>> GetHotelInfoResponse(string hotelId);

        Task<Hotel> AddNewHotel(AddHotelModel hotelModel, Owner owner);

        Task AddNewHotlContacts(List<Contact> contacts);

        Task<IQueryable<Room>> GetRooms(string hotelId);

        Task<ICollection<string>> GetRoomTypes();

        Task<bool> ChangeHotel(Hotel hotelModel);
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

        public async Task<ApplicationResponse<HotelInfoResponse>> GetHotelInfoResponse(string hotelId)
        {
            var result = await GetHotelInfo(hotelId);

            return result.ToResponse();
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

        public async Task<bool> ChangeHotel(Hotel hotelModel)
        {
            if (hotelModel is null)
            {
                return false;
            }

            var hotel = await GetHotelById(hotelModel.Id);

            _context.Hotels.Update(hotelModel);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task AddNewHotlContacts(List<Contact> contacts)
        {
            await _context.AddRangeAsync(contacts);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<Room>> GetRooms(string hotelId) =>
            _context.Rooms.Where(x => x.HotelId == hotelId);

        public async Task<ICollection<string>> GetRoomTypes() =>
            await _context.RoomTypes.Select(x => x.Type).ToListAsync();

        private string GetOwnerId(string userId) =>
            GetOwner(userId)?.Id;
    }
}