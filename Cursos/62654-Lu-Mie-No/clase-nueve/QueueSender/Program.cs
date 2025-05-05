using Azure.Storage.Queues;

String connectionString = "xxxx";
QueueClient queueClient = new QueueClient(connectionString, "app-exchange");
queueClient.SendMessage("Hello from .NET!");

//QueueServiceClient queueServiceClient = new QueueServiceClient(connectionString);