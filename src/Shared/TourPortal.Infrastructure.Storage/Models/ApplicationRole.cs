namespace TourPortal.Infrastructure.Storage.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole() : base(null) { }

        protected ApplicationRole(string roleName) : base(roleName) { Id = Guid.NewGuid().ToString(); }

        public virtual ICollection<ApplicationUserRole> Roles { get; set; }
    }
}