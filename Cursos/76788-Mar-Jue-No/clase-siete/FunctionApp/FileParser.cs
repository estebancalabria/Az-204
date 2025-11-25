using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using System.Net;

namespace FunctionApp
{
    public class FileParser(ILogger<FileParser> logger)
    {
        private readonly ILogger<FileParser> _logger = logger;

        [Function("FileParser")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            
            var response = req.CreateResponse(HttpStatusCode.OK);

            string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString") ?? "No connection string found.";
            BlobClient blobClient = new BlobClient(connectionString, "drop", "datos.json");
            BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();

            await response.WriteStringAsync(downloadResult.Content.ToString());

            return response;
        }
    }
}
