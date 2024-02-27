using Microsoft.AspNetCore.Mvc;
using SmartHelper.Helpers.EntitiesRecognition;
using SmartHelper.Helpers.SpeechRecognition;

namespace SmartHelper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly ISpeechRecognition _recognition;
        private readonly IEntitiesRecognition _entities;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISpeechRecognition speechRecognition, IEntitiesRecognition entities)
        {
            _logger = logger;
            _recognition = speechRecognition;
            _entities = entities;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var result = await _recognition.RecognizedSpeechAsync(@"C:\Dima\NURE\5Course\2_semester\SmartCity\PZ\PZ1\Recording.wav");
            var t = await _entities.RecognizedEntitiesAsync(result);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}