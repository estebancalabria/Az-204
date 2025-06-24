// See https://aka.ms/new-console-template for more information

using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

Console.WriteLine("Bienvenido al cliente de Azure Storage Queue");

Console.WriteLine("Ingrese el connection string de Azure Storage:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la cola:");
string queueName = Console.ReadLine()!;

QueueClient client = new QueueClient(connectionString, queueName);

QueueMessage mensaje = await client.ReceiveMessageAsync();

if (mensaje != null)
{
    Console.WriteLine($"Mensaje recibido: {mensaje.MessageText}");

    Console.WriteLine("¿Desea eliminar el mensaje? (s/n)");
    string respuesta = Console.ReadLine()!.ToLower();
    if (respuesta == "s")
    {
        await client.DeleteMessageAsync(mensaje.MessageId, mensaje.PopReceipt);
        Console.WriteLine("Mensaje eliminado.");
    }
}
else
{
    Console.WriteLine("No hay mensajes en la cola.");
}

//Preguntar al usuario si desea eliminar el mensaje


Console.WriteLine("Presione cualquier tecla para continuar...");
Console.ReadKey();
