namespace TourPortal.Infrastructure.Storage.Extensions
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using GlobalTypes;

    public static class StorageExstension
    {
        public static ModelBuilder SeedRolesToDatabase(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = ApplicationRoles.Administrator, 
                    NormalizedName = ApplicationRoles.Administrator.ToUpper()
                });

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = ApplicationRoles.User,
                    NormalizedName = ApplicationRoles.User.ToUpper()
                });

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = ApplicationRoles.Owner,
                    NormalizedName = ApplicationRoles.Owner.ToUpper()
                });

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = ApplicationRoles.Employe,
                    NormalizedName = ApplicationRoles.Employe.ToUpper()
                });

            return builder;
        }
    }
}