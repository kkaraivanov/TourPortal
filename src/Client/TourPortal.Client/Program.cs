namespace TourPortal.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Blazored.Modal;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using Infrastructure.Global.Types;
    using Infrastructure.Services;
    using Radzen;
    using Services;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
                .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services
                .AddBlazoredLocalStorage()
                .AddAuthorizationCore(config =>
                {
                    config.AddPolicy(Security.Policiy.IsAdministrator, Policies.IsAdministratorPolicy());
                    config.AddPolicy(Security.Policiy.IsUser, Policies.IsUserPolicy());
                    config.AddPolicy(Security.Policiy.IsOwner, Policies.IsOwnerPolicy());
                });

            builder.Services
                .AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IClientServices, ClientServices>();

            builder.Services
                .AddHttpClient<IAuthenticationService, AuthenticationService>(x =>
                    x.BaseAddress = new Uri(builder.Configuration["apiUrl"]));
            builder.Services
                .AddHttpClient<IClientServices, ClientServices>(x =>
                    x.BaseAddress = new Uri(builder.Configuration["apiUrl"]));

            builder.Services
                .AddScoped<DialogService>()
                .AddScoped<NotificationService>()
                .AddScoped<TooltipService>()
                .AddScoped<ContextMenuService>()
                .AddBlazoredModal();

            await builder.Build().RunAsync();
        }
    }
}
