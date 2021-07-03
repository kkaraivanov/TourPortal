namespace TourPortal.Infrastructure.Storage.Extensions
{
    using System;
    using Microsoft.EntityFrameworkCore;

    using Global.Types;
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
                    Name = Security.Role.Administrator, 
                    NormalizedName = Security.Role.Administrator.ToUpper()
                });

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = Security.Role.User,
                    NormalizedName = Security.Role.User.ToUpper()
                });

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = Security.Role.Owner,
                    NormalizedName = Security.Role.Owner.ToUpper()
                });

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = Security.Role.Employe,
                    NormalizedName = Security.Role.Employe.ToUpper()
                });

            return builder;
        }
    }
}