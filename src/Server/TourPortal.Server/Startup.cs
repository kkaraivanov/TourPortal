namespace TourPortal.Server
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;

    using Infrastructure.Security.Authentication;
    using Infrastructure.Security.Authorization;
    using Infrastructure.Services;
    using Infrastructure.Storage;
    using Infrastructure.Storage.Models;
    using Services;
    using Storage;

    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        public Startup(
            IWebHostEnvironment environment,
            IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurity:JwtSecurityKey"]));
            var expireTime = int.Parse(Configuration["JwtSecurity:JwtExpiryInDays"]);

            if (Environment.IsDevelopment())
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 44340;
                });
            }
            else
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }

            services.AddCors();

            services.Configure<TokenProviderOptions>(opts =>
            {
                opts.Audience = Configuration["JwtSecurity:JwtAudience"];
                opts.Issuer = Configuration["JwtSecurity:JwtIssuer"];
                opts.Path = "/api/account/login";
                opts.Expiration = TimeSpan.FromDays(expireTime);
                opts.SigningCredentials = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddStorageServices(Configuration);
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
                AdditionalUserClaimsPrincipalFactory>();

            services.SetCookiePolicy();
            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtSecurity:JwtIssuer"],
                        ValidAudience = Configuration["JwtSecurity:JwtAudience"],
                        IssuerSigningKey = jwtKey
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services
                .AddTransient<IAccountService, AccountService>()
                .AddTransient<IHotelService, HotelService>()
                .AddTransient<IUserService, UserService>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            var getCorsAddres = Configuration.GetSection("corsAddress");
            var corsAddreses = getCorsAddres
                .AsEnumerable()
                .Where(x => x.Value != null)
                .Select(x => x.Value)
                .ToArray();
            
            app.UseCors(cors => cors
                .WithOrigins(corsAddreses)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
            );

            // migrate database if have new migration
            using var dbServicesScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var databaseInitializer = dbServicesScope.ServiceProvider.GetService<IDatabaseInitializer>();
            databaseInitializer.InitializeAsync().Wait();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseJwtBearerTokens();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("api", "api/{controller}/{action}/{id?}");
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
