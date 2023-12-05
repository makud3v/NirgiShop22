namespace TARpe22ShopVaitmaa.Models.OpenWeather
{
    public class OpenWeatherViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public float Temp {  get; set; }
        public float FeltTemp { get; set; }
        public float TempMin { get; set; }
        public float TempMax { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
        public float WindSpeed { get; set; }
    }
}
