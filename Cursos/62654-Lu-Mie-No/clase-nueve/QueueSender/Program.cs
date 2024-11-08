using Azure.Storage.Queues;

String connectionString = "DefaultEndpointsProtocol=https;AccountName=csservicebus4trainner;AccountKey=xi8IjbvU2hDLh3nWzRRjbe5TLGuPBBHU9WYJHoBF2WGr1LxWui1k+uAB9M4QXD17ahMx2A/yMRlQ+AStRNtmWA==;EndpointSuffix=core.windows.net";
QueueClient queueClient = new QueueClient(connectionString, "app-exchange");
queueClient.SendMessage("Hello from .NET!");

//QueueServiceClient queueServiceClient = new QueueServiceClient(connectionString);