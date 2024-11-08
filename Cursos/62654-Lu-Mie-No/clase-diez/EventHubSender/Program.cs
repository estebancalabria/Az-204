
using System.Text;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

String connectionString = "Endpoint=sb://hub4trainner.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=udWOkbILoj+uRFMUkLe7ZnAMj79i9/DWf+AEhGXCvcs=";

await using (EventHubProducerClient producerClient = new EventHubProducerClient(connectionString, "mysuperawesomehub"))
{
    EventDataBatch batch = await producerClient.CreateBatchAsync();

    Console.WriteLine("Enviando eventos");
    int i = 0;
    EventData data = new EventData(Encoding.UTF8.GetBytes($"Hello, World! {i}"));
    while (batch.TryAdd(data))
    {
        data = new EventData(Encoding.UTF8.GetBytes($"Hello, World! {i}"));
        i++;
    }

    await producerClient.SendAsync(batch);
    Console.WriteLine($" {i} Eventos enviados");
}

