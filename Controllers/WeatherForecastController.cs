using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static List<WeatherForecast> _previsoes = new List<WeatherForecast>
        {
            new WeatherForecast { Date = DateTime.Now, Summary="Quente!!!!", TemperatureC=35, Id=1},
            new WeatherForecast { Date = DateTime.Now.AddDays(1), Summary="Ainda quente", TemperatureC=30, Id=2},
            new WeatherForecast { Date = DateTime.Now.AddDays(2), Summary="Já começo a precisar de casaco", TemperatureC=24, Id=3},
            new WeatherForecast { Date = DateTime.Now.AddDays(3), Summary="Tou tranquilo", TemperatureC=20, Id=4},
            new WeatherForecast { Date = DateTime.Now.AddDays(4), Summary="Atchoooo", TemperatureC=10, Id=5},
            new WeatherForecast { Date = DateTime.Now.AddDays(5), Summary="Polo Norte", TemperatureC=0, Id=6},
        };


        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {

        }

        [HttpGet]
        public ActionResult Get()
        {
            List<WeatherSimple> lista = new List<WeatherSimple>();
            _previsoes.ForEach((p) =>
            {
                lista.Add(new WeatherSimple { Date = p.Date, Id = p.Id });
            });
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult GetWeatherForecastById(int id)
        {
            var weatherForecast = _previsoes.FirstOrDefault(wf => wf.Id == id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return Ok(weatherForecast);
        }

        [HttpPost("create")]
        public IActionResult CreateWeatherForecast(WeatherForecast weatherForecast)
        {
            weatherForecast.Id = _previsoes.Count + 1;
            _previsoes.Add(weatherForecast);

            return CreatedAtAction(nameof(GetWeatherForecastById), new { id = weatherForecast.Id }, weatherForecast);
        }

        [HttpPut("update")]
        public IActionResult UpdateWeatherForecast(WeatherForecast updatedWeatherForecast)
        {
            var existingWeatherForecast = _previsoes.FirstOrDefault(wf => wf.Id == updatedWeatherForecast.Id);

            if (existingWeatherForecast == null)
            {
                return NotFound();
            }

            existingWeatherForecast.Date = updatedWeatherForecast.Date;
            existingWeatherForecast.TemperatureC = updatedWeatherForecast.TemperatureC;
            existingWeatherForecast.Summary = updatedWeatherForecast.Summary;

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteWeatherForecast(int id)
        {
            var existingWeatherForecast = _previsoes.FirstOrDefault(wf => wf.Id == id);

            if (existingWeatherForecast == null)
            {
                return NotFound();
            }

            _previsoes.Remove(existingWeatherForecast);

            return NoContent();
        }
    }
}
