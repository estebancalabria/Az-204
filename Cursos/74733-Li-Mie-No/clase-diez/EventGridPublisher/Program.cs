// See https://aka.ms/new-console-template for more information
using Azure;
using Azure.Messaging.EventGrid;

Console.WriteLine("Ingrese el topic endpoint:");
string topicEndpoint = Console.ReadLine()!;

Console.WriteLine("Ingrese el topic key:");
string topicKey = Console.ReadLine()!;

Console.WriteLine("Ingrese un mesaje para enviar al topic:");
string message = Console.ReadLine()!;

EventGridPublisherClient client = new EventGridPublisherClient(
    new Uri(topicEndpoint),
    new AzureKeyCredential(topicKey)
);

EventGridEvent evento = new EventGridEvent(
    subject: "Evento de Consola",
    eventType: "EventGridPublisherDemo.ConsoleApp",
    dataVersion: "1.0",
    data: message
);

client.SendEvent(evento);
Console.WriteLine("Evento enviado correctamente.");

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();
