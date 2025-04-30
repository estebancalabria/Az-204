using Azure.Storage.Queues;

Console.WriteLine("Ingrese el Connection String para el Storage Account de Azure");
String connectionString = Console.ReadLine()!;

Console.WriteLine();
Console.WriteLine("Ingrese el nombre de la cola");
String queueName = Console.ReadLine()!;

QueueClient client = new QueueClient(connectionString, queueName);
client.SendMessage("Mensaeje a la cola...");
Console.WriteLine("Mensaje enviado a la cola");