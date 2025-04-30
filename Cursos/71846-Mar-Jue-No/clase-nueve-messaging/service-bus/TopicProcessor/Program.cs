using Azure.Messaging.ServiceBus;

Console.WriteLine("Enter connection string del Service Bus: ");
string connectionString = Console.ReadLine();
Console.WriteLine("");
Console.WriteLine("Enter topic name: ");
string topicName = Console.ReadLine();
Console.WriteLine("");
Console.WriteLine("Enter subscription name: ");
string subscription = Console.ReadLine();
Console.WriteLine("");

ServiceBusClient client = new ServiceBusClient(connectionString);
ServiceBusProcessor processor = client.CreateProcessor(topicName, subscription);

processor.ProcessMessageAsync += async args =>
{
    string body = args.Message.Body.ToString();
    Console.WriteLine($"Received: {body}");
    await args.CompleteMessageAsync(args.Message);
};

processor.ProcessErrorAsync += args =>
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
};

await processor.StartProcessingAsync();

Console.WriteLine("Press any key to exit");
Console.ReadKey();

await processor.StopProcessingAsync();