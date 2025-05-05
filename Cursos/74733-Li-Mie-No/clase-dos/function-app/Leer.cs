using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace function_app
{
    public class Leer
    {
        private readonly ILogger _logger;

        public Leer(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Leer>();
        }

        [Function("Leer")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        [Blob("demo/menu.txt", FileAccess.Read)] string texto
        )
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
