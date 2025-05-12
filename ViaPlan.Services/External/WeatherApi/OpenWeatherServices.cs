
using Newtonsoft.Json;

namespace ViaPlan.Services.External.WeatherApi
{
    public class OpenWeatherServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _openWeatherApiKey;

        public OpenWeatherServices(HttpClient httpClient, string openWeatherApiKey)
        {
            _httpClient = httpClient;
            _openWeatherApiKey = openWeatherApiKey;
        }
        public async Task<OpenWeatherGetLocationDTO> GetLocationAsync(string city)
        {
            var response = await _httpClient.GetAsync($"http://api.openweathermap.org/geo/1.0/direct?q={city}&appid={_openWeatherApiKey}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var locations = JsonConvert.DeserializeObject<List<OpenWeatherGetLocationDTO>>(content);
                return locations?.FirstOrDefault() ?? throw new Exception("Location not found");
            }
            else
            {
                throw new Exception("Failed to fetch location data");
            }
        }
        public async Task<OpenWeatherDTO> GetWeatherDataAsync(double lat, double lon)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&exclude=minutely,hourly&units=metric&lang=bg&appid={_openWeatherApiKey}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OpenWeatherDTO>(content);
            }
            else
            {
                throw new Exception("Failed to fetch weather data");
            }
        }
    }
}