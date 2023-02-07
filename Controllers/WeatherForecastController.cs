using IHSA_Backend.Collections;
using IHSA_Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IHSA_Backend.Controllers
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
        private IUserCollection _userCollection;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IUserCollection userCollection)
        {
            _logger = logger;
            _userCollection = userCollection;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecastModel> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastModel
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost(Name = "TestDatabaseCreate")]
        public void makeNewUser(UserRequestModel request)
        {
            // create new usermodel
            var user = new UserModel();
            user.Username = request.Username;
            user.Email = request.Email;

            // add to database
            _userCollection.AddAsync(user);
        }
        [HttpGet("test", Name = "TestDatabaseGet")]
        public async Task<IEnumerable<UserModel>> TestDatabaseGet()
        {
            return await _userCollection.GetAllAsync();
        }
    }
}