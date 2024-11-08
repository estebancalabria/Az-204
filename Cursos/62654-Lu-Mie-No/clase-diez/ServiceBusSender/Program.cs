
using Azure.Messaging.ServiceBus;

String connectionString = "Endpoint=sb://bus4trainner.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=eeUFBzuX0hVHFX2ule0GALVLtU02RCGli+ASbKW0Pvo=";

string name = "mysuperawesomequeue";

ServiceBusClient client = new ServiceBusClient(connectionString);
await using (ServiceBusSender sender = client.CreateSender(name)){
    for (int i = 0; i < 10; i++){
        Console.WriteLine($"Sending message {i} to {name}");
        await sender.SendMessageAsync(new ServiceBusMessage($"Hello, world {i*11}!"));

    }
}


