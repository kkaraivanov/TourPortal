namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Shared.Enums;

    public class Contacts
    {
        public Contacts()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string ContactString { get; set; }

        public ContactType ContactType { get; set; }
    }
}