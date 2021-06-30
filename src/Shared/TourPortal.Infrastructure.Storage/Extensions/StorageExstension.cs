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
                    Name = Sequrity.Role.Administrator, 
                    NormalizedName = Sequrity.Role.Administrator.ToUpper()
                });

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = Sequrity.Role.User,
                    NormalizedName = Sequrity.Role.User.ToUpper()
                });

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = Sequrity.Role.Owner,
                    NormalizedName = Sequrity.Role.Owner.ToUpper()
                });

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = Sequrity.Role.Employe,
                    NormalizedName = Sequrity.Role.Employe.ToUpper()
                });

            return builder;
        }
    }
}