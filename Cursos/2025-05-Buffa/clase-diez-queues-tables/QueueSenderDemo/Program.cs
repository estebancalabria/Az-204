// See https://aka.ms/new-console-template for more information

using Azure.Storage.Queues;

Console.WriteLine("Bienvenido al cliente de Azure Storage Queue");

Console.WriteLine("Ingrese el connection string de Azure Storage:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la cola:");
string queueName = Console.ReadLine()!;

Console.WriteLine("Ingrese el mensaje que desea enviar a la cola:");
string message = Console.ReadLine()!;

QueueClient client = new QueueClient(connectionString, queueName);

await client.SendMessageAsync(message);

Console.WriteLine($"Mensaje enviado a la cola '{queueName}': {message}");
Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();