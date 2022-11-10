using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Services.Api;

public class WeatherService : IWeatherService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WeatherService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
    {
        var httpClient = _httpClientFactory.CreateClient("WeatherApiClient");

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/weather");

        var response = await httpClient.SendAsync(
            request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var forecasts = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(content);
        return forecasts;
    }
}