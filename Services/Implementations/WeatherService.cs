using System.Threading.Tasks;
using Services.Interfaces;
using Newtonsoft.Json.Linq;
using CrossCutting;
using CrossCutting.WeatherDictionary;

namespace Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        private readonly ConnectionAPI _connectionAPI;

        public WeatherService(ConnectionAPI connectionAPI)
        {
            _connectionAPI = connectionAPI;
        }

        public async Task<string> GetCurrentWeatherAsync()
        {
            string weatherData = await _connectionAPI.GetWeatherDataAsync();
            JObject weatherJson = JObject.Parse(weatherData);
            string weatherDescription = weatherJson["weather"][0]["description"].ToString();

            if (WeatherTranslations.Translations.TryGetValue(weatherDescription, out string translatedDescription))
            {
                return translatedDescription;
            }
            else
            {
                return weatherDescription; // Devuelve la descripción en inglés si no se encuentra la traducción
            }
        }

        public async Task<string> GetWeatherDescriptionAsync()
        {
            string weatherData = await _connectionAPI.GetWeatherDataAsync();
            JObject weatherJson = JObject.Parse(weatherData);
            string weatherDescription = weatherJson["weather"][0]["description"].ToString();

            if (WeatherTranslations.Translations.TryGetValue(weatherDescription, out string translatedDescription))
            {
                return translatedDescription;
            }
            else
            {
                return weatherDescription; // Devuelve la descripción en inglés si no se encuentra la traducción.
            }
        }
    }
}
