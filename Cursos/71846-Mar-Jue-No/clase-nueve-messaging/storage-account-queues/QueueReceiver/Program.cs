using Azure.Storage.Queues;

Console.WriteLine("Ingrese el Connection String para el Storage Account de Azure");
String connectionString = Console.ReadLine()!;

Console.WriteLine();
Console.WriteLine("Ingrese el nombre de la cola");
String queueName = Console.ReadLine()!;

QueueClient client = new QueueClient(connectionString, queueName);  
var response = client.ReceiveMessage();

if (response.Value != null)
{
    Console.WriteLine($"Mensaje recibido: {response.Value.MessageText}");
    client.DeleteMessage(response.Value.MessageId, response.Value.PopReceipt);
}
else
{
    Console.WriteLine("No hay mensajes en la cola");
}

