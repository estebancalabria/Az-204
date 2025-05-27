// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;

Console.WriteLine("Ingrese el connection string del service bus:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la cola:");
string queueName = Console.ReadLine()!;


ServiceBusClient client = new ServiceBusClient(connectionString);
ServiceBusSender sender = client.CreateSender(queueName);
ServiceBusMessage message = new ServiceBusMessage(new Random().Next() + " Hola, este es un mensaje de prueba desde C#");
await sender.SendMessageAsync(message);

Console.WriteLine("Mensaje enviado correctamente a la cola.");

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();