using Azure.Messaging.ServiceBus;

Console.WriteLine("Enter connection string del Service Bus: ");
string connectionString = Console.ReadLine();
Console.WriteLine("");
Console.WriteLine("Enter queue name: ");
string queueName = Console.ReadLine();
Console.WriteLine("");

ServiceBusClient client = new ServiceBusClient(connectionString);
ServiceBusReceiver receiver = client.CreateReceiver(queueName);

ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync();
if (message != null)
{
    Console.WriteLine($"Received: {message.Body}");
    await receiver.CompleteMessageAsync(message); //Esto lo elimina
} else {
    Console.WriteLine("No messages available.");
}
