using CrossCutting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IWeatherSQL
    {
        Task Add(WeatherDTO weather);
        Task Update(WeatherDTO weather);
        Task Delete(int id);
        Task<WeatherDTO> GetById(int id);
        Task<IEnumerable<WeatherDTO>> GetAll();
        Task<int> GetNextWeatherIdAsync();
        Task<Dictionary<int, string>> GetWeatherIdsAndCodesAsync();
    }
}
