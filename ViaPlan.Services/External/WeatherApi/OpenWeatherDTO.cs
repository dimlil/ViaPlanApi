namespace ViaPlan.Services.External.WeatherApi
{
    public class OpenWeatherGetLocationDTO
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
    public class OpenWeatherDTO
    {
        public double lon { get; set; }
        public double lat { get; set; }
        public Daily[] daily { get; set; }
        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
        }
        public class Daily
        {
            public int dt { get; set; }
            public Temp temp { get; set; }
            public Feels_Like feels_like { get; set; }
            public Weather[] weather { get; set; }
        }
        public class Temp
        {
            public double min { get; set; }
            public double max { get; set; }
        }
        public class Feels_Like
        {
            public double day { get; set; }
            public double night { get; set; }
        }
    }
}