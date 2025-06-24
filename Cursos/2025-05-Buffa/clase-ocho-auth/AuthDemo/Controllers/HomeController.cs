using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthDemo.Models;

namespace AuthDemo.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    public IActionResult Saludar(String nombre)
    {
        if (string.IsNullOrEmpty(nombre))
        {
            return BadRequest("El nombre no puede estar vacío.");
        }

        int longitud = nombre.Length;
        if (longitud > 10)
        {
            return BadRequest("El nombre no puede tener más de 10 caracteres.");
        }

        var mensaje = $"Hola, {nombre}!";
        _logger.LogInformation(mensaje);
        return Content(mensaje);
    }



    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
