using CrossCutting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWeatherApplication
    {
        Task AddWeather(WeatherDTO weather);
        Task UpdateWeather(WeatherDTO weather);
        Task DeleteWeather(int id);
        Task<WeatherDTO> GetWeatherById(int id);
        Task<IEnumerable<WeatherDTO>> GetAllWeathers();
        
        Task<int> GetNextWeatherIdAsync();
        Task<Dictionary<int, string>> GetWeatherIdsAndCodesAsync();

    }
}
