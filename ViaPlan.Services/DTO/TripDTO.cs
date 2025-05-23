namespace ViaPlan.Services.DTO
{
    public class TripDTO
    {
        public int Id { get; set; }
        public string? Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? WeatherSummary { get; set; }
        public string? HotelRecommendation { get; set; }
        public UserDTO? User { get; set; }
    }

    public class CreateTripDTO
    {
        public string? Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? WeatherSummary { get; set; }
        public string? HotelRecommendation { get; set; }
        public int UserId { get; set; }
    }
}