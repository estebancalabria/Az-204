# Lab 02- CosmoDB

## Resumen de pasos
1. Crear un recurso CosmoDB

2. Crear un Storage Account

3. Crear un container images dentro del Storage Account

4. Subir las imagenes del repo al container
https://github.com/MicrosoftLearning/AZ-204-DevelopingSolutionsforMicrosoftAzure/tree/master/Allfiles/Labs/04/Starter/Images

5. Vamos a trabajar con una solucion Armada con varios proyectos
https://github.com/MicrosoftLearning/AZ-204-DevelopingSolutionsforMicrosoftAzure/tree/master/Allfiles/Labs/04/Starter/AdventureWorks

6. Subir los datos a la base de datos con el proyecto AdventureWorks.Upload

    * Vamos a subir el archivo models.json

    * Editar el Program.cs del Proyecto AdventureWorks.Upload modificando las variables vacias

    * Agregar Paquete al proyecto Microsoft.Azure.Cosmos
    dotnet add package Microsoft.Azure.Cosmos -

    * Ejecutar Proyecto

7. Ejecutar Proyecto Web
    * Modificar el application.settins
    * Agregar Clase AdventureWorksCosmosContext.cs ap proyecto AdventureWorks.Context

```c#
using AdventureWorks.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorks.Context
{
    /* AdventureWorksCosmosContext class will implement the
    IAdventureWorksProductContext interface */
   public class AdventureWorksCosmosContext : IAdventureWorksProductContext
   {
        /* Create a new read-only Container variable named _container */
        private readonly Container _container;

      public AdventureWorksCosmosContext(string connectionString, string database = "Retail", string container = "Online")
      {
        /* Create a new instance of the CosmosClient class, and then obtain
          both a Database and Container instance from the client */
        _container = new CosmosClient(connectionString)
        .GetDatabase(database)
        .GetContainer(container);
      }

      public async Task<Model> FindModelAsync(Guid id)
      {
        /* Create a LINQ query, transform it into an iterator, iterate over the result set,
           and then return the single item in the result set */
        var iterator = _container.GetItemLinqQueryable<Model>()
        .Where(m => m.id == id).ToFeedIterator<Model>();
        List<Model> matches = new List<Model>();
        while (iterator.HasMoreResults)
        {
            var next = await iterator.ReadNextAsync();
            matches.AddRange(next);
        }

        return matches.SingleOrDefault();
      }

      public async Task<List<Model>> GetModelsAsync()
      {
        /* Run an SQL query, get the query result iterator, iterate over the result set,
            and then return the union of all results */
        string query = $@"SELECT * FROM items";
        var iterator = _container.GetItemQueryIterator<Model>(query);
        List<Model> matches = new List<Model>();
        while (iterator.HasMoreResults)
        {
            var next = await iterator.ReadNextAsync();
            matches.AddRange(next);
        }

        return matches;
      }

      public async Task<Product> FindProductAsync(Guid id)
      {
        /* Run an SQL query, get the query result iterator, iterate over the result set,
           and then return the single item in the result set */
        string query = $@"SELECT VALUE products
                    FROM models
                    JOIN products in models.Products
                    WHERE products.id = '{id}'";
        var iterator = _container.GetItemQueryIterator<Product>(query);
        List<Product> matches = new List<Product>();
        while (iterator.HasMoreResults)
        {
            var next = await iterator.ReadNextAsync();
            matches.AddRange(next);
        }

        return matches.SingleOrDefault();
      }

   }
}
```

* Compilar y ejecutar el proyecto

