namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
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

        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}