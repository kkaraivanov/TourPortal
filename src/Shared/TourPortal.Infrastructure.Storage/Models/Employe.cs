namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Employe
    {
        public Employe()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string ProfileId { get; set; }

        public virtual UserProfile Profile { get; set; }

        public string HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}