
using Azure.Messaging.ServiceBus;

String connectionString = "Endpoint=sb://bus4trainner.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=eeUFBzuX0hVHFX2ule0GALVLtU02RCGli+ASbKW0Pvo=";

string name = "mysuperawesomequeue";

ServiceBusClient client = new ServiceBusClient(connectionString);

//Forma de Recibirlo Manuel
/*await using (ServiceBusReceiver receiver = client.CreateReceiver(name)){
    var msg = receiver.ReceiveMessageAsync();
    Console.WriteLine($"Received message: {msg.Result.Body}");
    //Para borrararlo
    await receiver.CompleteMessageAsync(msg.Result);
}*/

//Recibirlo automaticamente
ServiceBusProcessor processor = client.CreateProcessor(name);

processor.ProcessMessageAsync += async (ProcessMessageEventArgs args) => {
    Console.WriteLine($"Received message: {args.Message.Body.ToString()}");
    await args.CompleteMessageAsync(args.Message);
};

processor.ProcessErrorAsync += async (ProcessErrorEventArgs args) => {
    Console.WriteLine($"Received error: {args.Exception.Message}");
    //await args.c;
};

Console.WriteLine("Leyendo los mensajes del Service Bus");
await processor.StartProcessingAsync();
Console.WriteLine("Presiona una tecla para salir...");
Console.ReadLine();
await processor.StopProcessingAsync();

