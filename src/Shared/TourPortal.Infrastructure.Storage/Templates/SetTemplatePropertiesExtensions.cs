namespace TourPortal.Infrastructure.Storage.Templates
{
    using System;
    using System.Linq;
    using System.Reflection;
    using IdentityServer4.EntityFramework.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public static class SetTemplatePropertiesExtensions
    {
        public static void SetTemplateProperties(this ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();
            DateTime timestamp = DateTime.UtcNow;

            foreach (EntityEntry entry in changeTracker.Entries())
            {
                // Auditable Entity Template
                if (entry.Entity is IAuditable)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("CreatedOn").CurrentValue = timestamp;
                    }

                    if (entry.State == EntityState.Deleted || entry.State == EntityState.Modified)
                    {
                        entry.Property("ModifiedOn").CurrentValue = timestamp;
                    }
                }

                if (entry.Entity is ISoftDelete)
                {
                    // Soft Delete Entity Template
                    //if (entry.State == EntityState.Modified)
                    //{
                    //    var state = Convert.ToBoolean(entry.Property("IsDeleted").CurrentValue ?? "false");
                    //    entry.Property("IsDeleted").CurrentValue = state;
                    //}
                    if (entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        entry.Property("IsDeleted").CurrentValue = true;
                    }
                }
            }
        }

        public static void SoftDeletableProperties(this ModelBuilder modelBuilder)
        {
            foreach (var tp in modelBuilder.Model.GetEntityTypes())
            {
                Type t = tp.ClrType;

                if (typeof(ISoftDelete).IsAssignableFrom(t))
                {
                    var method = SetIsDeletedShadowPropertyMethodInfo.MakeGenericMethod(t);
                    method.Invoke(modelBuilder, new object[] { modelBuilder });
                }
            }
        }

        private static readonly MethodInfo SetIsDeletedShadowPropertyMethodInfo = typeof(ModelBuilderExtensions).GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetIsSoftDeletableProperty");

        public static void SetIsSoftDeletableProperty<T>(ModelBuilder builder) where T : class, ISoftDelete
        {
            builder.Entity<T>().Property<bool>("IsDeleted");
        }
    }
}