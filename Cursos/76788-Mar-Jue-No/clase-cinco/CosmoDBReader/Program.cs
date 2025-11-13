using Microsoft.Azure.Cosmos;

Console.WriteLine("Ingrese el endpoint");
string endpoint = Console.ReadLine()!;

Console.WriteLine("Ingrese la key");
string key = Console.ReadLine()!;

string databaseId = "CursoDB";

string containerId = "Clientes";

CosmosClient cosmosClient = new CosmosClient(endpoint, key);
Container container = cosmosClient.GetContainer(databaseId, containerId);

var sqlQueryText = "SELECT * FROM c";

var resultSet = container.GetItemQueryIterator<dynamic>(new QueryDefinition(sqlQueryText));

while (resultSet.HasMoreResults)
{
    var response = await resultSet.ReadNextAsync();
    foreach (var item in response)
    {
        Console.WriteLine(item.nombre);
    }
}

Console.WriteLine("Consulta completada");