
using Azure.Storage.Queues;

Console.WriteLine("Ingese nombre queue");
string queueName = Console.ReadLine()!;

Console.WriteLine("Ingrese su connection string de la queue");
string connectionString = Console.ReadLine()!;

QueueClient client = new QueueClient(connectionString, queueName);
if (await client.ExistsAsync())
{
    Console.WriteLine($"La queue {queueName} ya existe.");
    client.SendMessage("Mensaje de prueba");
    Console.WriteLine("Mensaje enviado a la queue.");
}

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();
