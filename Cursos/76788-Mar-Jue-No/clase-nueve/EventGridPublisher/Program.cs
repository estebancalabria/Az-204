using Azure;
using Azure.Messaging.EventGrid;


Console.WriteLine("Ingrese el endpoint del topic");
string topicEndpoint = Console.ReadLine()!;

Console.WriteLine("Ingrese la key del topic");
string topickey = Console.ReadLine()!;

Console.WriteLine("Ingrese informacion para mandar en el evento");
string data = Console.ReadLine()!;

AzureKeyCredential credential = new AzureKeyCredential(topickey);
EventGridPublisherClient client = new EventGridPublisherClient(new Uri(topicEndpoint), credential);

EventGridEvent eventGridEvent = new EventGridEvent(
    subject: "Test.Subject",
    eventType: "Test.EventType",
    dataVersion: "1.0",
    data: new { Info = data }
);

await client.SendEventAsync(eventGridEvent);

Console.WriteLine("Evento enviado correctamente.");