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

## Crear una aplicacion que consume eventos
