using Azure.Storage.Queues;


String connectionString = "DefaultEndpointsProtocol=https;AccountName=cs4trainner;AccountKey=2CWb4hJ56JI8oPZfLj6d+2P32g3Yak5Zn+fLuWl4dcQk+/4Ut0Tf2bANdopByTKCJNVfQ6oUdbEA+ASt4FGzlQ==;EndpointSuffix=core.windows.net";
String queueName = "queue4trainner";

QueueClient queueClient = new QueueClient(connectionString, queueName);
Console.WriteLine("Enter a message to send to the queue:");
String message = Console.ReadLine()!;
queueClient.SendMessage(message);
