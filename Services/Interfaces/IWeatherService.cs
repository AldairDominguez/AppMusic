using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetCurrentWeatherAsync();
        Task<string> GetWeatherDescriptionAsync();
    }
}

