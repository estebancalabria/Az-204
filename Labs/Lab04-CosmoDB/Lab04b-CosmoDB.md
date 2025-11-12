# Laboratorio de Cosmo DB

* Crear el Resource Group
* Crear Recurso Azure CosmoDB for NoSQL
* Crear la base de datos y el contenedor
* Crear la app consola para escribir en esa base de datos

```
dotnet new console --name CosmoDBWriter
```
* Agregar el paquete

```
dotnet add package Microsoft.Azure.Cosmos
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
```
* 
