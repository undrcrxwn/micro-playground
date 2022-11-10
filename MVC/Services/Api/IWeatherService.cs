using MVC.Models;

namespace MVC.Services.Api;

public interface IWeatherService
{
    public Task<IEnumerable<WeatherForecast>> GetWeatherForecasts();
}