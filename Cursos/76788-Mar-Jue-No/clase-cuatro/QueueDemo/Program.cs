using Azure.Storage.Queues;


String connectionString = "...";
String queueName = "queue4trainner";

QueueClient queueClient = new QueueClient(connectionString, queueName);
Console.WriteLine("Enter a message to send to the queue:");
String message = Console.ReadLine()!;
queueClient.SendMessage(message);
