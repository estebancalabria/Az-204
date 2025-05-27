using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clase_tres_functionapp
{
    public class Saludo
    {
        private readonly ILogger<Saludo> _logger;

        public Saludo(ILogger<Saludo> logger)
        {
            _logger = logger;
        }

        [Function("Saludo")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Bienvenido a la clase 3 de Azure Functions");
        }
    }
}
