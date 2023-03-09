using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Nancy.Json.Simple;
using Newtonsoft.Json.Linq;
using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WetterInfo.DataAccess.Repository.IRepository;
using WetterInfo.Models;

namespace WetterInfo_App.Controllers
{
    public class WeatherMapController : Controller
    {
        private readonly IResponseRepository _responseRepository;
        public WeatherMapController(IResponseRepository responseRepository)
        {
            _responseRepository = responseRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult WeatherForecast()
        {
            
            return View();
        }

        

        [HttpPost]
        public async Task<JsonResult> GetForecast(string city) 
        {
            string apiKey = "89f4d8c68709ae67c4ff7feabe73dd79";
            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric", city, apiKey);

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;

                var contentStream = await response.Content.ReadAsStreamAsync();
                var weatherResponse = await JsonSerializer.DeserializeAsync<Root>(contentStream);

                ResultView result = new ResultView();

                result.City = weatherResponse.name;
                result.CurrentTemp = weatherResponse.main.temp;
                result.TempMin = weatherResponse.main.temp_min;
                result.TempMax = weatherResponse.main.temp_max;
                result.AirPressure = weatherResponse.main.pressure;
                result.Humidity = weatherResponse.main.humidity;
                result.WindSpeed = weatherResponse.wind.speed;
                result.WindDirection = weatherResponse.wind.deg;
                result.CloudCondition = weatherResponse.clouds.all;



                _responseRepository.Add(result);
                return Json(result);


            }
        }
    }
}
