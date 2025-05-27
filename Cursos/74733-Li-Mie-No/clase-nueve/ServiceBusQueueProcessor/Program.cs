// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;

Console.WriteLine("Ingrese el connection string del service bus:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la cola:");
string queueName = Console.ReadLine()!;


ServiceBusClient client = new ServiceBusClient(connectionString);

ServiceBusProcessor processor = client.CreateProcessor(queueName);

processor.ProcessMessageAsync += async args =>
{
    Console.WriteLine($"Mensaje recibido: {args.Message.Body.ToString()}");
    await args.CompleteMessageAsync(args.Message);
};

processor.ProcessErrorAsync += args =>
{
    Console.WriteLine($"Error procesando el mensaje: {args.Exception.Message}");
    return Task.CompletedTask;
};

await processor.StartProcessingAsync();
Console.WriteLine("Presione cualquier tecla para detener el procesamiento...");

Console.ReadKey();
await processor.StopProcessingAsync();

Console.WriteLine("Procesamiento detenido.");
await client.DisposeAsync();

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();
