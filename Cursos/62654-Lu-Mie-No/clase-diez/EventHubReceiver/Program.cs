
using System.Text;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Microsoft.Extensions.Azure;

String connectionString = "xxx";


/*var events =consumerClient.ReadEventsAsync();

var lista = events.ToBlockingEnumerable().ToList();

foreach (var item in lista)
{
    if (item.Data is not null)
    {
        var body = Encoding.UTF8.GetString(item.Data.EventBody);
        Console.WriteLine($"Received message: {body}");
    }
}*/


//String consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
// Crear un cliente de consumidor de Event Hubs
await using (EventHubConsumerClient consumerClient = new EventHubConsumerClient(connectionString, "mysuperawesomehub"))
{
    // Leer eventos desde el principio de la partición
    await foreach (PartitionEvent partitionEvent in consumerClient.ReadEventsAsync())
    {
        Console.WriteLine($"Evento recibido: {partitionEvent.Data.EventBody}");
    }
}