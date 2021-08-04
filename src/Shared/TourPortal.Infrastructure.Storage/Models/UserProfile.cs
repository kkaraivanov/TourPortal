namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Templates;

    public class UserProfile : IAuditable
    {
        public UserProfile()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key] 
        public string Id { get; set; }
        
        public string Sity { get; set; }

        public string Address { get; set; }

        public byte[] ProfilaImage { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual Employe Employe { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}