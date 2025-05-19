
using Newtonsoft.Json;

namespace ViaPlan.Services.External.HotelApi
{
    public class HotelService
    {

         private readonly HttpClient _httpClient;

        public HotelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HotelDTO> GetHotelAsync(double lat, double lon)
        {
            Console.WriteLine($"Fetching hotel data for coordinates: {lat}, {lon}");
            var response = await _httpClient.GetAsync($"https://nominatim.openstreetmap.org/search.php?q=hotel&format=json&limit=1&lat={lat}&lon={lon}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var hotels = JsonConvert.DeserializeObject<List<HotelDTO>>(content);
                return hotels.FirstOrDefault();
            }
            else
            {
                throw new Exception("Failed to fetch hotel data");
            }
        }
    }
}