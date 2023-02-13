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

        
        public async Task<IActionResult> WeatherForecast()
        {
            //using (HttpClient client = new HttpClient())
            //{
            //    try
            //    {
            //        client.BaseAddress = new Uri("https://api.openweathermap.org");
            //        HttpResponseMessage response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=9a330029906249853bb17f5b790a8151&units=metric");
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //}
            return View();
        }

        //[HttpPost]
        //public string WeatherDetail(string city)
        //{

        //    //Assign API KEY which received from OPENWEATHERMAP.ORG  
        //    string appId = "8113fcc5a7494b0518bd91ef3acc074f";

        //    //API path with CITY parameter and other parameters.  
        //    string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", city, appId);

        //    using (WebClient client = new WebClient())
        //    {
        //        string json = client.DownloadString(url);

        //        //********************//  
        //        //     JSON RECIVED   
        //        //********************//  
        //        //{"coord":{ "lon":72.85,"lat":19.01},  
        //        //"weather":[{"id":711,"main":"Smoke","description":"smoke","icon":"50d"}],  
        //        //"base":"stations",  
        //        //"main":{"temp":31.75,"feels_like":31.51,"temp_min":31,"temp_max":32.22,"pressure":1014,"humidity":43},  
        //        //"visibility":2500,  
        //        //"wind":{"speed":4.1,"deg":140},  
        //        //"clouds":{"all":0},  
        //        //"dt":1578730750,  
        //        //"sys":{"type":1,"id":9052,"country":"IN","sunrise":1578707041,"sunset":1578746875},  
        //        //"timezone":19800,  
        //        //"id":1275339,  
        //        //"name":"Mumbai",  
        //        //"cod":200}  

        //        //Converting to OBJECT from JSON string.  
        //        Root weatherInfo = (new JavaScriptSerializer()).Deserialize<Root>(json);

        //        //Special VIEWMODEL design to send only required fields not all fields which received from   
        //        //www.openweathermap.org api  
        //        ResultView rslt = new ResultView();

        //        rslt.Country = weatherInfo.sys.country;
        //        rslt.City = weatherInfo.name;
        //        rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
        //        rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
        //        rslt.Description = weatherInfo.weather[0].description;
        //        rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
        //        rslt.Temp = Convert.ToString(weatherInfo.main.temp);
        //        //rslt.TempFeelsLike = Convert.ToString(weatherInfo.Main.feels_like);
        //        rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
        //        rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
        //        //rslt.WeatherIcon = weatherInfo.Weather[0].icon;

        //        //Converting OBJECT to JSON String   
        //        var jsonstring = new JavaScriptSerializer().Serialize(rslt);

        //        //Return JSON string.  
        //        return jsonstring;
        //    }
        //}

        [HttpPost]
        public async Task<JsonResult> GetForecast(string city)
        {
            string apiKey = "8113fcc5a7494b0518bd91ef3acc074";
            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", city, apiKey);
            //string urls = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                //var jsonOpts = new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true };
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

                // var serializedObj = new JavaScriptSerializer().Serialize(result);

                _responseRepository.Add(result);
                return Json(result);

                
            }
        }
    }
}
