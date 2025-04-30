
using Azure;
using Azure.Messaging.EventGrid;

Console.WriteLine("Ingrese el endpoint del Event Grid: ");
string endpoint = Console.ReadLine()!; 

Console.WriteLine("Ingrese la clave (key) de acceso del Event Grid: ");
string key = Console.ReadLine()!;

Console.WriteLine();

Uri endpointUri = new Uri(endpoint);
AzureKeyCredential credential = new AzureKeyCredential(key);

EventGridPublisherClient client = new EventGridPublisherClient(endpointUri, credential);

EventGridEvent evento = new EventGridEvent(
    subject: "MiEvento",
    eventType: "MiTipo",
    dataVersion: "1.0",
    data: new { mensaje = "Hola, Event Grid!" }
);

await client.SendEventAsync(evento);

Console.WriteLine("Evento enviado correctamente.");