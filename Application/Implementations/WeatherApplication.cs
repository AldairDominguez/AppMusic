using Application.Interfaces;
using CrossCutting.DTO;
using Infraestructure.Interfaces;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations
{
    public class WeatherApplication : IWeatherApplication
    {
        private readonly IWeatherSQL _weatherSQL;
        private readonly IWeatherService _weatherService;

        public WeatherApplication(IWeatherSQL weatherSQL, IWeatherService weatherService)
        {
            _weatherSQL = weatherSQL;
            _weatherService = weatherService;
        }

        public async Task AddWeather(WeatherDTO weather)
        {
            weather.Id = await GetNextWeatherIdAsync();
            await _weatherSQL.Add(weather);
        }

        public async Task UpdateWeather(WeatherDTO weather)
        {
            await _weatherSQL.Update(weather);
        }

        public async Task DeleteWeather(int id)
        {
            await _weatherSQL.Delete(id);
        }

        public async Task<WeatherDTO> GetWeatherById(int id)
        {
            return await _weatherSQL.GetById(id);
            
        }

        public async Task<IEnumerable<WeatherDTO>> GetAllWeathers()
        {
            return await _weatherSQL.GetAll();
        }

        public async Task<int> GetNextWeatherIdAsync() 
        {
            return await _weatherSQL.GetNextWeatherIdAsync();
        }

        public async Task<string> GetWeatherDescription()
        {
            return await _weatherService.GetCurrentWeatherAsync();
        }
        public async Task<string> GetWeatherDescriptionAsync()
        {
            return await _weatherService.GetWeatherDescriptionAsync();
        }
        public async Task<Dictionary<int, string>> GetWeatherIdsAndCodesAsync()
        {
            return await _weatherSQL.GetWeatherIdsAndCodesAsync();
        }
        

    }
}
