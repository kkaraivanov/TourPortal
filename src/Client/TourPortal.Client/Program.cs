namespace TourPortal.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services
                .AddScoped(sp =>
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
                .AddSingleton(async sp =>
                {
                    var httpClient = sp.GetRequiredService<HttpClient>();
                    return await httpClient
                        .GetStringAsync("appsettings.json")
                        .ConfigureAwait(false);
                });

            await builder.Build().RunAsync();
        }
    }
}
