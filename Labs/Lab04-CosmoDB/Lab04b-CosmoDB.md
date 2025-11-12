# Laboratorio de Cosmo DB

* Crear el Resource Group
* Crear Recurso Azure CosmoDB for NoSQL
* Crear la base de datos y el contenedor

- ## Aplicacion para Escribir datos en Cosmos DB
  
* Crear la app consola para escribir en esa base de datos

```
dotnet new console --name CosmoDBWriter
```
* Agregar los paquetes

```
dotnet add package Microsoft.Azure.Cosmos
dotnet add package Newtonsoft.Json
```

* Codigo para escribir en mi base de datos

```c#
using Microsoft.Azure.Cosmos;

Console.WriteLine("Ingrese el endpoint");
string endpoint = Console.ReadLine()!;

Console.WriteLine("Ingrese la key");
string key = Console.ReadLine()!;

string databaseId = "CursoDB";

string containerId = "Clientes";

CosmosClient cosmosClient = new CosmosClient(endpoint, key);
Container container = cosmosClient.GetContainer(databaseId, containerId);

var cliente = new
{
    id = Guid.NewGuid().ToString(),
    nombre = "Juan Perez",
    email = "juan.perez@example.com"
    ciudad = "Ciudad de México"
};

await container.CreateItemAsync(cliente, new PartitionKey(cliente.id));

Console.WriteLine("Cliente creado exitosamente");
```

* Correr el proyecto y verificar luego que este los datos en Azure

```cmd
dotnet run
```

- ## Aplicacion para leer datos en Cosmos DB

* Crear la app

```
dotnet new console --name CosmoDBReader
```

* Agregar los paquetes

```
dotnet add package Microsoft.Azure.Cosmos
dotnet add package Newtonsoft.Json
```

* Codigo para Leer de mi base de datos

```c#
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
```
