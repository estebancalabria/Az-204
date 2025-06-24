using Azure.Messaging.ServiceBus;

Console.WriteLine("Ingrese el connection String de la cola del service bs:");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la cola:");
string queueName = Console.ReadLine()!;

Console.WriteLine("Ingrese el mensaje a enviar:");
string messageContent = Console.ReadLine()!;

ServiceBusClient client = new ServiceBusClient(connectionString);
ServiceBusSender sender = client.CreateSender(queueName);
ServiceBusMessage message = new ServiceBusMessage(messageContent);
await sender.SendMessageAsync(message);

Console.WriteLine("Mensaje enviado a la cola " + queueName);
Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();
