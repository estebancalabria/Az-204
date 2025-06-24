using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ApplicationInsightsDemo.Models;

namespace ApplicationInsightsDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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

    public String LogDemo()
    {
        this._logger.LogWarning("LOG: Este es un mensaje de advertencia de Prueba");
        this._logger.LogWarning("LOG: Este es otro mensaje de advertencia de Prueba");

        return "LogDemo ejecutado correctamente mire la terminal de logs";
    }
}
