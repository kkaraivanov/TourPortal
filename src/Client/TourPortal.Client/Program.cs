namespace TourPortal.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services
                .AddBlazoredLocalStorage()
                .AddAuthorizationCore()
                .AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>()
                .AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(x =>
            x.BaseAddress = new Uri(builder.Configuration["apiUrl"]));

            await builder.Build().RunAsync();
        }
    }
}
