using Azure.Storage.Queues;

// See https://aka.ms/new-console-template for more information
String connectionString = "xxxxxxx";
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

