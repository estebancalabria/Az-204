using Azure.Storage.Queues;

// See https://aka.ms/new-console-template for more information
String connectionString = "DefaultEndpointsProtocol=https;AccountName=csservicebus4trainner;AccountKey=xi8IjbvU2hDLh3nWzRRjbe5TLGuPBBHU9WYJHoBF2WGr1LxWui1k+uAB9M4QXD17ahMx2A/yMRlQ+AStRNtmWA==;EndpointSuffix=core.windows.net";
QueueClient queueClient = new QueueClient(connectionString, "app-exchange");
/*var response = queueClient.PeekMessage();
if (response != null){
    Console.WriteLine(response.Value.Body.ToString());
}*/
var response = queueClient.ReceiveMessage();
if (response.Value != null){
    Console.WriteLine(response.Value.Body.ToString());
}else{
    Console.WriteLine("no message");
}

