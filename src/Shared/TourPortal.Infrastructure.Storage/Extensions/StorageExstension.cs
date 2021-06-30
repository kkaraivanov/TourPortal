namespace TourPortal.Infrastructure.Storage.Extensions
{
    using System;
    using GlobalTypes;
    using Microsoft.EntityFrameworkCore;

    using Models;

    public static class StorageExstension
    {
        public static ModelBuilder SeedRolesToDatabase(this ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = ApplicationRoles.Administrator, 
                    NormalizedName = ApplicationRoles.Administrator.ToUpper()
                });

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = ApplicationRoles.User,
                    NormalizedName = ApplicationRoles.User.ToUpper()
                });

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = ApplicationRoles.Owner,
                    NormalizedName = ApplicationRoles.Owner.ToUpper()
                });

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
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