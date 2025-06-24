
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

Console.WriteLine("Ingrese el connection string del event hub:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre del event hub:");
string eventHubName = Console.ReadLine()!;

EventHubProducerClient producerClient = new EventHubProducerClient(connectionString, eventHubName);

EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

for (int i = 0; i < 1000; i++)
{
    string message = $"Mensaje {i + 1}";
    eventBatch.TryAdd(new EventData(message));
}

await producerClient.SendAsync(eventBatch);
Console.WriteLine("Mensajes enviados correctamente.");

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();