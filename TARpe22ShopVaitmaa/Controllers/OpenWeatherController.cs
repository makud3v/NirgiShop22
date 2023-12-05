using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TARpe22ShopVaitmaa.Models.OpenWeather;

namespace TARpe22ShopVaitmaa.Controllers
{
    public class OpenWeatherController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();

            string result = await (await httpClient
                .GetAsync("https://api.openweathermap.org/data/2.5/weather?lat=59.4370&lon=24.7536&appid=6dad9e545a6fa8d31fd60b63304cd69c&units=metric"))
                    .Content.ReadAsStringAsync();

            dynamic json = JsonConvert.DeserializeObject(result);
            var weatherMain = json?.main;
            var weatherWind = json?.wind;
            var weatherInfo = json?.weather[0];

            OpenWeatherViewModel vm = new OpenWeatherViewModel
            {
                Title = weatherInfo.main ?? "N/A",
                Description = weatherInfo.description ?? "N/A",
                Temp = weatherMain.temp ?? "N/A",
                FeltTemp = weatherMain.feels_like ?? 0,
                TempMin = weatherMain.temp_min ?? 0,
                TempMax = weatherMain.temp_max ?? 0,
                Pressure = weatherMain.pressure ?? 0,
                Humidity = weatherMain.humidity ?? 0,
                WindSpeed = weatherWind.speed ?? 0
            };

            return View(vm);
        }
    }


}
