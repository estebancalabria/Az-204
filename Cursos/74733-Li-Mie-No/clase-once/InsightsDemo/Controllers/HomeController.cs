using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InsightsDemo.Models;

namespace InsightsDemo.Controllers;

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

    public String LogDemo()
    {
        _logger.LogInformation("Log: This is an information log message.");
        _logger.LogWarning("Log: This is a warning log message.");
        _logger.LogError("Log: This is an error log message.");
        //_logger.LogCritical("Log: This is a critical log message.");

        Console.WriteLine("Console: Logging demo completed. Check the logs for messages.");

        return "Logging demo completed. Check the logs for messages.";
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
