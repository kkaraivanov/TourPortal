namespace TourPortal.Storage
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Configurations;
    using Infrastructure.Storage.Extensions;
    using Infrastructure.Storage.Models;
    using Infrastructure.Storage.Templates;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetSoftDeleteMethodInfo = typeof(ApplicationDbContext)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetSoftDelete");

        public DbSet<Message> Messages { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Employe> Employes { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<RoomInType> RoomsInType { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ConfigureUserIdentity(builder);
            SetQueryFilters(builder);
        }

        private static void ConfigureUserIdentity(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(b =>
            {
                b.HasOne(a => a.Profile)
                    .WithOne(b => b.ApplicationUser)
                    .HasForeignKey<UserProfile>(b => b.UserId);

                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.HasMany(e => e.Roles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            builder.Entity<UserProfile>(b =>
            {
                b.HasOne(up => up.Owner)
                    .WithOne(o => o.Profile)
                    .HasForeignKey<Owner>(o => o.ProfileId);

                b.HasOne(up => up.Employe)
                    .WithOne(e => e.Profile)
                    .HasForeignKey<Employe>(e => e.ProfileId);

                b.HasMany(up => up.Reservations)
                    .WithOne(r => r.Profile)
                    .HasForeignKey(r => r.ProfileId);
            });

            builder.Entity<Hotel>(b =>
            {
                b.HasOne(h => h.Owner)
                    .WithOne(o => o.Hotel)
                    .HasForeignKey<Hotel>(h => h.OwnerId);

                b.HasMany(h => h.Employes)
                    .WithOne(e => e.Hotel)
                    .HasForeignKey(eh => eh.HotelId);
                
                b.HasMany(h => h.Rooms)
                    .WithOne(r => r.Hotel)
                    .HasForeignKey(r => r.HotelId);
                
                b.HasMany(h => h.Reservations)
                    .WithOne(r => r.Hotel)
                    .HasForeignKey(r => r.HotelId);
            });

            builder.Entity<RoomInType>(b =>
            {
                b.HasKey(k => new {k.RoomId, k.RoomTypeId});

                b.HasOne(rit => rit.Room)
                    .WithMany(r => r.RoomInTypes)
                    .HasForeignKey(rit => rit.RoomId)
                    .IsRequired();

                b.HasOne(rit => rit.RoomType)
                    .WithMany(rt => rt.RoomInTypes)
                    .HasForeignKey(rit => rit.RoomTypeId)
                    .IsRequired();
            });

            builder.SeedRolesToDatabase();
            builder.SoftDeletableProperties();
            builder.Entity<Message>().ToTable("Messages");
            builder.ApplyConfiguration(new MessageConfiguration());
        }

        private void SetQueryFilters(ModelBuilder modelBuilder)
        {
            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType tp in modelBuilder.Model.GetEntityTypes())
            {
                Type t = tp.ClrType;

                if (typeof(ISoftDelete).IsAssignableFrom(t))
                {
                    MethodInfo method = SetSoftDeleteMethodInfo.MakeGenericMethod(t);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        public void SetSoftDelete<T>(ModelBuilder builder) where T : class, ISoftDelete
        {
            builder.Entity<T>().HasQueryFilter(item => !EF.Property<bool>(item, "IsDeleted"));
        }

        public override int SaveChanges()
        {
            ChangeTracker.SetTemplateProperties();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.SetTemplateProperties();
            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}