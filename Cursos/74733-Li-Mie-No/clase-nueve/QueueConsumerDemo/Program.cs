using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

Console.WriteLine("Ingese nombre queue");
string queueName = Console.ReadLine()!;

Console.WriteLine("Ingrese su connection string de la queue");
string connectionString = Console.ReadLine()!;

QueueClient client = new QueueClient(connectionString, queueName);

QueueMessage mensaje = client.ReceiveMessage();
if (mensaje == null)
{
    Console.WriteLine("No se encontraron mensajes en la cola.");
}
else
{
    Console.WriteLine("Mensaje recibido: " + mensaje.MessageText);
    Console.WriteLine("Desea eliminar el mensaje? (s/n)");
    string respuesta = Console.ReadLine()!.ToLower();
    if (respuesta == "s")
    {
        client.DeleteMessage(mensaje.MessageId, mensaje.PopReceipt);
        Console.WriteLine("Mensaje eliminado.");
    }
}


Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();