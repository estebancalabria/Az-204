using Azure.Messaging.ServiceBus;

Console.WriteLine("Enter connection string del Service Bus: ");
string connectionString = Console.ReadLine();
Console.WriteLine("");
Console.WriteLine("Enter queue name: ");
string queueName = Console.ReadLine();
Console.WriteLine("");

ServiceBusClient client = new ServiceBusClient(connectionString);
ServiceBusSender sender = client.CreateSender(queueName);

try{
    ServiceBusMessage message = new ServiceBusMessage("Mesaje al service Bus");
    await sender.SendMessageAsync(message);
    Console.WriteLine("Mensaje enviado");
}finally{
    await sender.DisposeAsync();
    await client.DisposeAsync();
}