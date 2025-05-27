using System.Diagnostics;
using System.Text.Json;
using CosmoDbDemo;
using Microsoft.Azure.Cosmos;

Console.WriteLine("Ingrese el Endpoint de Cosmos DB:");
string EndpointUrl = Console.ReadLine()!;

Console.WriteLine("Ingrese la clave de autorización de Cosmos DB:");
string AuthorizationKey = Console.ReadLine()!;


const string DatabaseName = "Retail";
const string ContainerName = "Online";
const string PartitionKey = "/Category";
const string JsonFilePath = "models.json";

int amountToInsert;
List<Model> models;

 try
{
    // <CreateClient>
    CosmosClient cosmosClient = new CosmosClient(EndpointUrl, AuthorizationKey, new CosmosClientOptions() { AllowBulkExecution = true });
    // </CreateClient>

    // <Initialize>
    Console.WriteLine($"Creating a database if not already exists...");
    Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);

    // Configure indexing policy to exclude all attributes to maximize RU/s usage
    Console.WriteLine($"Creating a container if not already exists...");
    await database.DefineContainer(ContainerName, PartitionKey)
            .WithIndexingPolicy()
                .WithIndexingMode(IndexingMode.Consistent)
                .WithIncludedPaths()
                    .Attach()
                .WithExcludedPaths()
                    .Path("/*")
                    .Attach()
            .Attach()
        .CreateAsync();
    // </Initialize>

    using (StreamReader reader = new StreamReader(File.OpenRead(JsonFilePath)))
    {
        string json = await reader.ReadToEndAsync();
        models = JsonSerializer.Deserialize<List<Model>>(json)!;
        amountToInsert = models.Count;
    }

    // Prepare items for insertion
    Console.WriteLine($"Preparing {amountToInsert} items to insert...");

    // Create the list of Tasks
    Console.WriteLine($"Starting...");
    Stopwatch stopwatch = Stopwatch.StartNew();
    // <ConcurrentTasks>
    Container container = database.GetContainer(ContainerName);

    List<Task> tasks = new List<Task>(amountToInsert);
    foreach (Model model in models)
    {
        tasks.Add(container.CreateItemAsync(model, new PartitionKey(model.Category))
            .ContinueWith(itemResponse =>
            {
                if (!itemResponse.IsCompletedSuccessfully)
                {
                    AggregateException innerExceptions = itemResponse.Exception!.Flatten();
                    if (innerExceptions.InnerExceptions.FirstOrDefault(innerEx => innerEx is CosmosException) is CosmosException cosmosException)
                    {
                        Console.WriteLine($"Received {cosmosException.StatusCode} ({cosmosException.Message}).");
                    }
                    else
                    {
                        Console.WriteLine($"Exception {innerExceptions.InnerExceptions.FirstOrDefault()}.");
                    }
                }
            }));
    }

    // Wait until all are done
    await Task.WhenAll(tasks);
    // </ConcurrentTasks>
    stopwatch.Stop();

    Console.WriteLine($"Finished writing {amountToInsert} items in {stopwatch.Elapsed}.");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}