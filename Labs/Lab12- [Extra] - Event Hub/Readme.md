# Laboratorio 12 - Event Hub

## Setup del Laboratorio

* Crear un Resource Group
* Crear un Event Hub Namespace
  * Crear un Event Hub
    * Crear un Shared Access Policy
    * Copiarse El Connection String
* Crear un Storage Account para el Checkpoint
  * Crear un Container dentro del Storage Account

## Crear una aplicacion que produce eventos

* Crear la aplicacion

```cmd
dotnet new console --name EventHubProducer
```

* Ir a la carpeta de la aplicacion

```cmd
cd EventHubProducer
```

* Agregar la librerias necesarias

```cmd
dotnet add package Azure.Messaging.EventHubs

```

* Completamos el codigo del Program.cs

```c#
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

Console.WriteLine("Event Hubs - Send Events Sample");

Console.Write("Enter the Event Hubs namespace connection string: ");
String connectionString = Console.ReadLine()!;

Console.Write("Enter the Event Hub name: ");
String eventHubName = Console.ReadLine()!;

// Create a producer client that you can use to send events to an event hub
await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
{
    // Create a batch of events 
    using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

    for (int i = 1; i <= 10; i++)
    {
        // Try adding a new event to the batch
        if (!eventBatch.TryAdd(new EventData($"Event {i}")))
        {
            // If it is too large for the batch, throw an exception
            throw new Exception($"Event {i} is too large for the batch and cannot be sent.");
        }
    }

    // Use the producer client to send the batch of events to the event hub
    await producerClient.SendAsync(eventBatch);
    Console.WriteLine("A batch of 10 events has been published.");
}

```

* Ejecutar el proyecto

```cmd
  dotnet run
```

* Chequear el portal
  * Podemos ver los eventos en el Data Explorer

## Crear una aplicacion que consume eventos

* Crear la aplicacion

```cmd
dotnet new console --name EventHubCosumer
```

* Ir a la carpeta de la aplicacion

```cmd
cd EventHubConsumer
```

* Agregar Paquetes necesarios

```
dotnet add package Azure.Messaging.EventHubs
dotnet add package Azure.Messaging.EventHubs.Processor
dotnet add package Azure.Storage.Blobs
```

* Copiar este codigo al Program.cs

```c#

using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs.Primitives;
using Azure.Storage.Blobs;
using System.Text;

Console.WriteLine("Event Hub Consumer Example with Checkpointing");


Console.Write("Enter the Event Hubs namespace connection string: ");
String connectionString = Console.ReadLine()!;

Console.Write("Enter the Event Hub name: ");
String eventHubName = Console.ReadLine()!;

Console.Write("Enter the Blob Storage connection string: ");
String blobStorageConnectionString = Console.ReadLine()!;

Console.Write("Enter the Blob Container name: ");
String blobContainerName = Console.ReadLine()!;

// Create a BlobContainerClient that the EventProcessorClient will use
BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

// Create an EventProcessorClient to process events

EventProcessorClient processor = new EventProcessorClient(storageClient,
                "$Default",
                connectionString,
                eventHubName);


processor.ProcessEventAsync += async (ProcessEventArgs eventArgs) =>
{
    // Write the body of the event to the console window
    Console.WriteLine($"Received event: { Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()) }");

    // Update checkpoint in the blob storage so that the app receives only new events the next time it's run
    await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
};

processor.ProcessErrorAsync += async (ProcessErrorEventArgs eventArgs) =>
{
    // Write details about the error to the console window
    Console.WriteLine($"\nError on Partition: { eventArgs.PartitionId }, Error: { eventArgs.Exception.Message }\n");
    await Task.CompletedTask;
};

// Start the processing
Console.WriteLine("Starting the processor...");
await processor.StartProcessingAsync();

Console.WriteLine("Press [Enter] to stop the processor.");
Console.ReadLine();

await processor.StopProcessingAsync();
``` 

* Ejecutar el proyecto

```cmd
  dotnet run
```

* Probar la app que recibe

> Podemos recibir primerlo los eventos que habiamos enviado antes y luego podemos mandar otros 10 para ver como los recibimos en forma sincronica
