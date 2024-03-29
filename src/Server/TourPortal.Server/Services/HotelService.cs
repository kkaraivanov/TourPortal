﻿namespace TourPortal.Server.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Infrastructure.Shared.Enums;
    using Infrastructure.Shared.Models.Hotel;
    using Infrastructure.Shared.Models.Response;
    using Infrastructure.Storage.Models;
    using Storage;

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

        public async Task<bool> ChangeHotel(ChangeHotelModel hotelModel)
        {
            var hotel = await GetHotelById(hotelModel.Id);
            Thread.Sleep(200);
            hotel.HotelName = hotelModel.HotelName;
            hotel.Address = hotelModel.Address;
            hotel.City = hotelModel.City;
            hotel.HotelImageUrl = hotelModel.HotelImageUrl;
            hotel.ModifiedOn = DateTime.Now;

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

            _context.Hotels.Update(hotel);
            await UpdateHotlContacts(contacts, hotel.Id);

            _context.SaveChanges();

            return true;
        }

        public async Task AddNewHotlContacts(List<Contact> contacts)
        {
            await _context.Contacts.AddRangeAsync(contacts);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHotlContacts(List<Contact> contacts, string hotelId)
        {
            var databaseContacts = _context.Contacts
                .Where(x => x.HotelId == hotelId)
                .ToList();
            var temp = new Stack<Contact>(contacts);

            while (databaseContacts.Any() && temp.Any())
            {
                var tempContact = temp.Pop();
                var contact = databaseContacts.FirstOrDefault(x => x.Id == tempContact.Id);

                if (contact is null)
                {
                    _context.Contacts.Add(tempContact);
                }
                else
                {
                    _context.Contacts.Update(tempContact);
                    databaseContacts.Remove(contact);
                }
            }

            if (!databaseContacts.Any())
            {
                foreach (var contact in contacts)
                {
                    _context.Contacts.Add(contact);
                }
            }
            else
            {
                for (int i = 0; i < databaseContacts.Count; i++)
                {
                    _context.Contacts.Remove(databaseContacts[i]);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddNewRoom(RoomModel roomModel, Hotel hotel)
        {
            if (roomModel is null)
            {
                return false;
            }

            var room = GetNewRoom(roomModel);
            room.Hotel = hotel;
            room.HotelId = hotel.Id;

            foreach (var roomImage in roomModel.RoomImages)
            {
                room.RoomImages.Add(new RoomImages {ImageUrl = roomImage});
            }

            var roomType = _context.RoomTypes.FirstOrDefault(x => x.Type == roomModel.RoomType);
            var roomInType = new RoomInType
            {
                RoomType = roomType,
                RoomTypeId = roomType.Id,
                Room = room,
                RoomId = room.Id
            };

            room.RoomInTypes.Add(roomInType);
            _context.Rooms.Add(room);
            _context.SaveChanges();

            return true;
        }

        public async Task<bool> ChangeRoom(RoomModel roomModel, Hotel hotel)
        {
            if (roomModel is null)
            {
                return false;
            }

            //var room = _context.Rooms
            //    .FirstOrDefault(x => x.RoomNumber == roomModel.RoomNumber && x.HotelId == hotel.Id);
            var getRooms = await GetRooms(hotel.Id);
            var room = getRooms.FirstOrDefault(x => x.RoomNumber == roomModel.RoomNumber);

            if (room is null)
            {
                return false;
            }

            room.CountOfBeds = roomModel.CountOfBeds;
            room.CountOfPersons = roomModel.CountOfPersons;
            room.Price = roomModel.Price;

            var roomImages  = _context.RoomImageses
                .Where(x => x.RoomId == room.Id && x.IsDeleted == false)
                .ToList();
            
            if (!roomImages.Any())
            {
                foreach (var roomImage in roomModel.RoomImages)
                {
                    room.RoomImages.Add(new RoomImages { ImageUrl = roomImage });
                }
            }
            else
            {
                foreach (var roomImage in roomImages.OrderBy(x => x.ImageUrl))
                {
                    if (roomModel.RoomImages.Contains(roomImage.ImageUrl))
                    {
                        var image = roomModel.RoomImages.First(x => x == roomImage.ImageUrl);
                        roomModel.RoomImages.Remove(image);
                        continue;
                    }
                    else
                    {
                        roomImage.IsDeleted = true;
                        roomImage.DeletedOn = DateTime.Now;
                        _context.RoomImageses.Update(roomImage);
                        _context.SaveChanges();
                    }
                }
            }

            if (roomModel.RoomImages.Any())
            {
                foreach (var roomImage in roomModel.RoomImages.Distinct())
                {
                    room.RoomImages.Add(new RoomImages { ImageUrl = roomImage });
                }
            }

            var roomType = _context.RoomTypes.FirstOrDefault(x => x.Type == roomModel.RoomType);
            var roomInType = _context.RoomsInType.FirstOrDefault(x => x.RoomId == room.Id);
            roomInType.RoomType = roomType;
            roomInType.RoomTypeId = roomType.Id;

            _context.RoomsInType.Update(roomInType);
            _context.Rooms.Update(room);

            return _context.SaveChanges() > 0;
        }

        public async Task<Room> GetRoom(string roomId) =>
            _context.Rooms
                .FirstOrDefault(x => x.Id == roomId);

        public async Task<ICollection<Room>> GetRooms(string hotelId) =>
            DistinctRoomNumber(hotelId);

        public async Task<ICollection<RoomImages>> GetRoomImages(string roomId) =>
            _context.RoomImageses
                .Where(x => x.RoomId == roomId && x.IsDeleted == false)
                .OrderBy(x => x.Id)
                .ToList();

        public async Task<ICollection<string>> GetRoomTypes() =>
            await _context.RoomTypes
                .OrderBy(x => x.Type)
                .Select(x => x.Type)
                .ToListAsync();

        public async Task<string> GetRoomType(string roomId) =>
            _context.RoomsInType
                .Where(x => x.RoomId == roomId)
                .Select(x => x.RoomType.Type)
                .FirstOrDefault();

        public async Task<bool> CheckRoomIsFree(string roomId) =>
            _context.Reservations
                .Any(x => x.RoomId == roomId && x.IsCompleted == false);

        private Room GetNewRoom(RoomModel roomModel) =>
            new Room
            {
                RoomNumber = roomModel.RoomNumber,
                CountOfBeds = roomModel.CountOfBeds,
                CountOfPersons = roomModel.CountOfPersons,
                Price = roomModel.Price
            };

        private string GetOwnerId(string userId) =>
            GetOwner(userId)?.Id;

        // this method is return distinct records in rooms table and check errors from AddNewRooms() method
        private ICollection<Room> DistinctRoomNumber(string hotelId)
        { 
            var rooms = _context.Rooms
                .Where(x => x.HotelId == hotelId)
                .OrderBy(x => x.RoomNumber)
                .ToList();
            var temp = new List<Room>(rooms);

            for(int i = 0; i < temp.Count; i++)
            {
                var current = temp[i];
                var removed = rooms
                    .Where(x => x.RoomNumber == current.RoomNumber)
                    .FirstOrDefault(x => x.Id != current.Id);

                if (removed != null)
                {
                    rooms.Remove(removed);
                    temp.Remove(removed);
                }
            }

            return rooms;
        }
    }
}