using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System;
using IpInfo;
 
namespace BlazorApp.Client.Services
{
    public class ApiClientService: IApiClientService
    {
        private readonly HttpClient http;

        public ApiClientService(HttpClient http)
        {
            this.http = http;
        }

        public async Task GetLocationAsync()
        {
            var token = await http.GetStringAsync("/api/GetGeoLoc");

            using var client = new HttpClient();
            var api = new IpInfoApi(token, client);

            var response = await api.GetCurrentInformationAsync();
            
            var json = JsonConvert.SerializeObject(response);
            
            var result = await http.PostAsync("/api/LogGeoLoc", new StringContent(json, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                Console.WriteLine("Successfully log the geo location.");
            }
            else
            {
                Console.WriteLine("Fail to log the geo location.");
            }
        }
    }
}