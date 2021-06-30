namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Microsoft.AspNetCore.Identity;
    using Templates;

    public class ApplicationUser : IdentityUser<string>, ISoftDelete
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
            UserRoles = new HashSet<ApplicationUserRole>();
            Messages = new HashSet<Message>();
        }

        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string MidleName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        [JsonIgnore]
        public override string PasswordHash { get; set; }

        [JsonIgnore]
        public override string SecurityStamp { get; set; }

        public UserProfile Profile { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}