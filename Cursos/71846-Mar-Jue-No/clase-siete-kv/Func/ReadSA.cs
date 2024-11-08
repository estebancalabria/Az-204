using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Func
{
    public class ReadSA
    {
        private readonly ILogger _logger;

        public ReadSA(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ReadSA>();
        }

        [Function("ReadSA")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
             var response = req.CreateResponse(HttpStatusCode.OK);

             string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString")!;

             /* Create a new instance of the BlobClient class by passing in your
                connectionString variable, a  "drop" string value, and a
                "records.json" string value to the constructor */
             BlobClient blob = new BlobClient(connectionString, "files", "chat.txt");

             // Download the content of the referenced blob 
             BlobDownloadResult downloadResult = blob.DownloadContent();

              // Retrieve the value of the downloaded blob and convert it to string
             response.WriteString(downloadResult.Content.ToString());
            
             //return the response
             return response;
        }
    }
}
