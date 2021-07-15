using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorApp.Client.Services;

namespace BlazorApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var baseAddress = builder.Configuration["BaseAddress"] ?? builder.HostEnvironment.BaseAddress;
            var httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
            builder.Services.AddScoped(_ => httpClient);

            builder.Services.AddScoped<IApiClientService, ApiClientService>();

            await builder.Build().RunAsync();

            // Console.WriteLine("Hello World");

            // await LogGeoLocation(builder, httpClient);
        }

        private static async Task LogGeoLocation(WebAssemblyHostBuilder builder, HttpClient client)
        {
            var apiService = new ApiClientService(client);
            try 
            {
                await apiService.GetLocationAsync();
                Console.WriteLine("Finished Geo logging.");
            } 
            catch
            {
                Console.WriteLine("Some exception happed during logging geo location.");
            }
            // if (builder.HostEnvironment.IsProduction())
            // {
            //     try 
            //     {
            //         await apiService.GetLocationAsync();
            //     } 
            //     catch
            //     {
            //         Console.WriteLine("Some exception happed during logging geo location.");
            //     }
            // }
            // else
            // {
            //     Console.WriteLine("Skipped geo loc logging due to not in production mode.");
            // }
        }
    }
}