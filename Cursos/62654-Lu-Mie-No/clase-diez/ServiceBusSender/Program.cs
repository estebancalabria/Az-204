
using Azure.Messaging.ServiceBus;

String connectionString = "xxxx";

string name = "mysuperawesomequeue";

ServiceBusClient client = new ServiceBusClient(connectionString);
await using (ServiceBusSender sender = client.CreateSender(name)){
    for (int i = 0; i < 10; i++){
        Console.WriteLine($"Sending message {i} to {name}");
        await sender.SendMessageAsync(new ServiceBusMessage($"Hello, world {i*11}!"));

    }
}


