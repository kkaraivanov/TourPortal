namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Templates;

    public class Hotel : IAuditable
    {
        public Hotel()
        {
            Id = Guid.NewGuid().ToString();
            Employes = new HashSet<Employe>();
            Rooms = new HashSet<Room>();
            Reservations = new HashSet<Reservation>();
            Contactses = new HashSet<Contacts>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string HotelName { get; set; }

        public string HotelImageUrl { get; set; }

        [Required]
        [MaxLength(20)]
        public string Sity { get; set; }

        [Required]
        [MaxLength(64)]
        public string Address { get; set; }

        public int RoomsCount => Rooms.Count;

        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual ICollection<Employe> Employes { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<Contacts> Contactses { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}