namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Reservation
    {
        public Reservation()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }

        public string RoomId { get; set; }

        public string ProfileId { get; set; }

        public virtual UserProfile Profile { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int NumberOfNights { get; set; }

        public int NumberOfPersons { get; set; }

        public decimal Price { get; set; }

        public bool IsCompleted { get; set; }
    }
}