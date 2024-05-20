using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using demo_mvc.Models;

namespace demo_mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private string _titulo = "Titulo Sin Definir";

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _titulo = configuration["Titulo"]!.ToString();
    }

    public IActionResult Index()
    {
        this.ViewBag.Titulo = this._titulo;
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
}
