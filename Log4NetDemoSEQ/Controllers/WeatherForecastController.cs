using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Log4NetDemoSEQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        //private readonly ILogger<WeatherForecastController> _logger;
        public ILoggerManager _log;

        public WeatherForecastController(ILoggerManager log)    //ILogger<WeatherForecastController> logger, )
        {
            //_logger = logger;
            _log = log;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //_logger.LogInformation($"Log4NetDemoSEQ - New Request Received");
            _log.Information($"Log4NetDemoSEQ - New Request Received");
            var data = Enumerable.Range(1, 100).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            _log.Information($"Log4NetDemoSEQ - Request is returning {data.ToList().Count} records");
            return data;
        }
    }
}