using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class ConnectionAPI
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly double _latitude = -12.0464; // Coordenadas de Lima
        private readonly double _longitude = -77.0428; // Coordenadas de Lima

        public ConnectionAPI()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");
            _apiKey = ""; // API Key
        }

        public async Task<string> GetWeatherDataAsync()
        {
            string url = $"weather?lat={_latitude}&lon={_longitude}&appid={_apiKey}&units=metric";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                string errorDetails = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"No se puede obtener los datos del clima. Detalles: {errorDetails}");
            }
        }
    }
}
