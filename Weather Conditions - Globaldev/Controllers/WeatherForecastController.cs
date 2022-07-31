using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather_Conditions___Globaldev.Data;

namespace Weather_Conditions___Globaldev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly DataContext _context;

        public WeatherForecastController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<WeatherForecast>>> Get()
        {
            var weather = await _context.WeatherForecasts.ToListAsync();

           return Ok(weather);
        }

        [HttpGet("GetWeatherForecastByCity/{city}")]
        public async Task<ActionResult<List<WeatherForecast>>> GetWeatherForecastByCity(string city)
        {
            var cityWeather = await _context.WeatherForecasts.Where(t => t.City == city).ToListAsync();

            if (cityWeather == null)
                return BadRequest("City not found");

            return Ok(cityWeather);
        }

        [HttpGet("GetWeatherForecastByCityAndSI/{city}")]
        public async Task<ActionResult<WeatherForecastSI>> GetWeatherForecastByCityAndSI(string city)
        {
            var cityWeather = await _context.WeatherForecasts.Where(t => t.City == city).ToListAsync();

            if (cityWeather == null)
                return BadRequest("City not found");

            var sum = (decimal) cityWeather.Sum(t => t.TemperatureC) / cityWeather.Count();

            var newCityWeather = new WeatherForecastSI()
            {
                AverageT = Math.Round(sum, 1),
                MaxT = cityWeather.Max(t => t.TemperatureC),
                MinT = cityWeather.Min(t => t.TemperatureC),
                City = cityWeather.FirstOrDefault().City,
                Id = cityWeather.FirstOrDefault().Id,
                TemperatureC = cityWeather.OrderByDescending(t => t.Date).FirstOrDefault().TemperatureC,
                Date = cityWeather.OrderByDescending(t => t.Date).FirstOrDefault().Date
            };

            return Ok(newCityWeather);
        }

        [HttpPost]
        public async Task<ActionResult<List<WeatherForecast>>> AddWeatherForecast(WeatherForecast weatherForecast)
        {
            _context.WeatherForecasts.Add(weatherForecast);
            await _context.SaveChangesAsync();

            return Ok(await _context.WeatherForecasts.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<WeatherForecast>>> UpdateWeatherForecast(WeatherForecast weatherForecast)
        {

            var cityWeather = await _context.WeatherForecasts.FirstOrDefaultAsync(t => t.Id == weatherForecast.Id && t.City == weatherForecast.City && t.Date == weatherForecast.Date);

            if (cityWeather == null)
                return BadRequest("It was not possible to update de weather condition. Or this city doesn't exist or their is no record on this day for this city");

            cityWeather.TemperatureC = weatherForecast.TemperatureC;

            await _context.SaveChangesAsync();

            return Ok(await _context.WeatherForecasts.ToListAsync());
        }
    }
}
