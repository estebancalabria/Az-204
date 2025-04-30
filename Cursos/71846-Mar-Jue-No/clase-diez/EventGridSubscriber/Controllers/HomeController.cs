using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventGridSubscriber.Models;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace EventGridSubscriber.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Evento([FromBody] JObject eventGridEvent)
    {
        Console.WriteLine("Evento Recibido");
        if (eventGridEvent["eventType"]?.ToString() == "Microsoft.EventGrid.SubscriptionValidationEvent")
        {
            var validationCode = eventGridEvent["data"]!["validationCode"]!.ToString();
            return Ok(new { validationResponse = validationCode });
        }

        // Procesar el evento del blob creado
        if (eventGridEvent["eventType"]?.ToString() == "Microsoft.Storage.BlobCreated")
        {
            var blobUrl = eventGridEvent["data"]!["url"]!.ToString();
            // Aquí puedes añadir la lógica para procesar el archivo
            Console.WriteLine($"Nuevo blob creado: {blobUrl}");
        }

        return Ok();
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
}
