// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;

Console.WriteLine("Ingrese el connection string del service bus:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la cola:");
string queueName = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la subscripcion:");
string subscriptionName = Console.ReadLine()!;

ServiceBusClient client = new ServiceBusClient(connectionString);
ServiceBusReceiver receiver = client.CreateReceiver(queueName, subscriptionName);

Console.WriteLine("Esperando mensajes...");

ServiceBusReceivedMessage mensaje = await receiver.ReceiveMessageAsync();
if (mensaje != null)
{
    Console.WriteLine($"Mensaje recibido: {mensaje.Body.ToString()}");
    await receiver.CompleteMessageAsync(mensaje); //Lo elimina de la cola

}
else
{
    Console.WriteLine("No se recibieron mensajes.");
}

await receiver.DisposeAsync();
await client.DisposeAsync();

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();


