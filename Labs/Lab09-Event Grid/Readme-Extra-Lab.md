# Lab 9 - Extra - Event Grid - System Topics 

* Crear el Resource Group

```bash
az group create --name rg-az204-lab-09 --location westus
```

* Crear un Stroage Account

```bash
az storage account create --name cs4events --resource-group rg-Az204-lab-09 --location westus --sku Standard_LRS               
```

* Crear un container en el storage account

```bash
az storage container create --name upload --account-name cs4events
```

* Obtener el Resource ID del Storage Account

```bash
 $storageID = az storage account show --name cs4events --resource-group rg-az204-lab-09 --query id --output tsv
```

* Crear un System Topic para el Storage Account

```bash
az eventgrid system-topic create --name topic-4-storage --resource-group rg-az204-lab-09 --location westus --source $storageID --topic-type microsoft.storage.storageaccounts
```

* Crear la aplicacion para recibir eventos

```cmd
  dotnet new web --name EventGridReceiver
```

* Editar con VSCode

```cmd
code .
```

* Programar el endpoint para recibir los eventos

```c#
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/events", async (HttpContext context) =>
{
    var req = context.Request;
    using var reader = new StreamReader(req.Body);
    var body = await reader.ReadToEndAsync();
    using var jsonDoc = JsonDocument.Parse(body);
    var events = jsonDoc.RootElement;
    
    //sI es un evento de registracion lo valido
    if (req.Headers.TryGetValue("Aeg-Event-Type", out var eventType) 
         && eventType == "SubscriptionValidation")
    {
        Console.WriteLine(events.ToString());
        var validationCode = events[0].GetProperty("data").GetProperty("validationCode").GetString();

        Console.WriteLine($"Received subscription validation event. Validation code: {validationCode}");

        return Results.Ok(new
        {
            validationResponse = validationCode
        });
    }

    Console.WriteLine("Evento Recibido");
    Console.WriteLine(events.ToString());

    return Results.Ok();
});

app.Run();

```

* Ejecutar la aplicacion y crear una url publica con VSCode (devTunnel)

* Crear la subscripcion

```bash
az eventgrid system-topic event-subscription create --name subscription-4-cs4events --system-topic-name topic-4-storage --resource-group rg-az204-lab-09 --endpoint https://4sz0nj9n-5169.brs.devtunnels.ms/events --included-event-types Microsoft.Storage.BlobCreated Microsoft.Storage.BlobDeleted
```

* Crear un blob y chequear que se recibe el evento en la aplicacion

Se deberia recibir un evento de esta forma:
```
Evento Recibido
[{"topic":"/subscriptions/91dc3067-fd7a-4ed3-95eb-8c938f69cbfe/resourceGroups/rg-Az204-lab-09/providers/Microsoft.Storage/storageAccounts/cs4events","subject":"/blobServices/default/containers/upload/blobs/55a29d22d81646f8a27d22d98629469e (1).webp","eventType":"Microsoft.Storage.BlobCreated","id":"8c5af164-a01e-0068-756d-5e9738065c04","data":{"api":"PutBlob","requestId":"8c5af164-a01e-0068-756d-5e9738000000","eTag":"0x8DE2C84B394EF2A","contentType":"image/webp","contentLength":42464,"blobType":"BlockBlob","accessTier":"Default","url":"https://cs4events.blob.core.windows.net/upload/55a29d22d81646f8a27d22d98629469e (1).webp","sequencer":"00000000000000000000000000022D49000000000cd09d91","storageDiagnostics":{"batchId":"4067ab1f-0006-0061-006d-5ed2eb000000"}},"dataVersion":"","metadataVersion":"1","eventTime":"2025-11-26T00:42:39.3376554Z"}]    
```

