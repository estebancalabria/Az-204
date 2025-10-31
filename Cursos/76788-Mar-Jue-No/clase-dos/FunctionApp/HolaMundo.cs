using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FunctionApp
{
    public class HolaMundo
    {
        private readonly ILogger<HolaMundo> _logger;

        public HolaMundo(ILogger<HolaMundo> logger)
        {
            _logger = logger;
        }

        [Function("HolaMundo")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Hola mundo esta es mi primer funcion de Function APP!");
        }
    }
}
