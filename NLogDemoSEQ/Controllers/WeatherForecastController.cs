using Microsoft.AspNetCore.Mvc;
using NLog;

namespace NLogDemoSEQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        Logger logger = LogManager.GetCurrentClassLogger();


        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation($"NLogDemoSEQ1 - New Request Received");
            logger.Info($"NLogDemoSEQ - New Request Received");
            logger.Error($"NLogDemoSEQ Error - New Request Received");
            var data = Enumerable.Range(1, 100).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            logger.Info($"NLogDemoSEQ - Request is returning {data.ToList().Count} records");
            return data;
        }
    }
}