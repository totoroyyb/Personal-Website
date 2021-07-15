using System.Threading.Tasks;

namespace BlazorApp.Client.Services
{
    public interface IApiClientService
    {
        Task GetLocationAsync();
    }
}
