using Azure.Messaging.EventHubs.Consumer;

Console.WriteLine("Ingrese el connection string del event hub:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre del event hub:");
string eventHubName = Console.ReadLine()!;

EventHubConsumerClient consumer = new EventHubConsumerClient(
    EventHubConsumerClient.DefaultConsumerGroupName,
    connectionString,
    eventHubName
);

await foreach (PartitionEvent evento in consumer.ReadEventsAsync())
{
    string mensaje = evento.Data.EventBody.ToString();
    Console.WriteLine($"Mensaje recibido: {mensaje}");
}

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();
