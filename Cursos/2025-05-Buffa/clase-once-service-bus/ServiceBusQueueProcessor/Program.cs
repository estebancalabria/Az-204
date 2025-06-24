using Azure.Messaging.ServiceBus;

Console.WriteLine("Ingrese el connection String de la cola del service bs:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la cola:");
string queueName = Console.ReadLine()!;

ServiceBusClient client = new ServiceBusClient(connectionString);
ServiceBusProcessor processor = client.CreateProcessor(queueName);

//Recibo los mensajes
processor.ProcessMessageAsync += async args =>
{
    Console.WriteLine($"Mensaje recibido: {args.Message.Body.ToString()}");
    await args.CompleteMessageAsync(args.Message);
};

//Manejo de errores
processor.ProcessErrorAsync += args =>
{
    Console.WriteLine($"Error: {args.Exception.Message}");
    return Task.CompletedTask;
};

//Iniciar el procesador
await processor.StartProcessingAsync();

Console.WriteLine("Presione cualquier tecla para detener el procesador...");
Console.ReadKey();

//Detener el procesador
await processor.StopProcessingAsync();