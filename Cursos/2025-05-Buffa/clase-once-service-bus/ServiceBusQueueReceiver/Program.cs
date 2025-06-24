using Azure.Messaging.ServiceBus;

Console.WriteLine("Ingrese el connection String de la cola del service bs:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la cola:");
string queueName = Console.ReadLine()!;

ServiceBusClient client = new ServiceBusClient(connectionString);
ServiceBusReceiver receiver = client.CreateReceiver(queueName);

Console.WriteLine("Esperando mensajes...");
ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync();
if (message != null)
{
    Console.WriteLine($"Mensaje recibido: {message.Body.ToString()}");
    Console.WriteLine($"Id del mensaje: {message.MessageId}");
}
else
{
    Console.WriteLine("No se recibieron mensajes.");
}

//Preguntarle si desea eliminar el mensaje
Console.WriteLine("¿Desea eliminar el mensaje? (s/n)");
string respuesta = Console.ReadLine()!.ToLower();
if (respuesta == "s")
{
    await receiver.CompleteMessageAsync(message);
    Console.WriteLine("Mensaje eliminado.");
}
else
{
    Console.WriteLine("Mensaje no eliminado.");
}

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();