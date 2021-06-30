namespace TourPortal.Storage
{
    using System;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Infrastructure.Storage;

    public static class StorageServicesRegister
    {
        public static IServiceCollection AddStorageServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(builder => GetDbContextOptions<ApplicationDbContext>(builder, configuration));
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

            return services;
        }

        public static IIdentityServerBuilder AddIdentityServerStores(this IIdentityServerBuilder builder, IConfiguration configuration)
            => builder.AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = x => GetDbContextOptions<ApplicationDbContext>(x, configuration);
            })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = x => GetDbContextOptions<ApplicationDbContext>(x, configuration);
                    options.EnableTokenCleanup = true;
                // TODO: Change time value for token life state. Time is in seconds
                options.TokenCleanupInterval = 3600;
                });

        private static void GetDbContextOptions<T>(DbContextOptionsBuilder builder, IConfiguration configuration)
            where T : DbContext
        {
            var migrationsAssembly = typeof(T).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                if (string.IsNullOrEmpty(connectionString))
                    throw new ArgumentNullException("The DefaultConnection was not found.");

                if (!connectionString.ToLower().Contains("multipleactiveresultsets=true"))
                    throw new ArgumentException("When Sql Server is in use the DefaultConnection must contain: MultipleActiveResultSets=true");

                builder.UseSqlServer(connectionString, options =>
                {
                    options.CommandTimeout(60);
                    options.MigrationsAssembly(migrationsAssembly);
                });
            }
        }
    }
}