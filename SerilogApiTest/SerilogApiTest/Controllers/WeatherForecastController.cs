using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using SerilogApiTest.Extensions;
using SerilogApiTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerilogApiTest.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> GetAll()
        {
            LogContext.PushProperty("UserId", "70915");//改成驗證時候增加
            //已經用ActionName 代替 {SourceContext}.{Method}
            //LogContext.PushProperty("Method", System.Reflection.MethodBase.GetCurrentMethod().Name);

            _logger.LogInformation($"jojo-the world~");
            _logger.LogAppError("dio");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("error")]
        public IActionResult GetError()
        {
            LogContext.PushProperty("UserId", "70915");//改成驗證時候增加

            try
            {
                throw new Exception("this is error!");
            }
            catch (Exception ex)
            {
                _logger.LogAppError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
