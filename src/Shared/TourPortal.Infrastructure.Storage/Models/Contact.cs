namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Shared.Enums;

    public class Contact
    {
        public Contact()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string HotelId { get; set; }

        public Hotel Hotel { get; set; }

        [Required]
        public string ContactString { get; set; }

        public ContactType ContactType { get; set; }
    }
}