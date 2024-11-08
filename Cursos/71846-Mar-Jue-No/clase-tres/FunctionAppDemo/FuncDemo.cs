using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;

namespace FunctionAppDemo
{
    public class FuncDemo
    {
        private readonly ILogger _logger;

        public FuncDemo(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FuncDemo>();
        }

        [Function("FuncDemo")]
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            [Blob(@"nuevocontenedor/6fb852ca-bae8-4b61-8152-e1a60bd4d083.txt", FileAccess.Read, Connection ="StorageAccount")] String contenidoArchivo)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
