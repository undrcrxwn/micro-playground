using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Api;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWeatherService _weather;

    public HomeController(ILogger<HomeController> logger, IWeatherService weather)
    {
        _logger = logger;
        _weather = weather;
    }

    public async Task<IActionResult> Index()
    {
        var forecasts = await _weather.GetWeatherForecasts();
        return View(forecasts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}