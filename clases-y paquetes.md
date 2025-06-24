# Blobs

## Paquetes

* Azure.Storage.Blobs

## Clases

* StorageSharedKeyCredential : Para conectarse 

* BlobServiceClient : Para conectarme al blob
* BlobContainerClient
* BlobClient 

* AccountInfo
* BlobContainerItem
* BlobItem

---
# Queues

## Paquetes

* Azure.Storage.Queues

## Clases

* QueueClient
* QueueMessage
---
# Tables

## Paquetes

* Azure.Data.Tables

## Clases

* TableClient
* TableEntity

---
# CosmoDB

## Paquetes

* Microsoft.Azure.Cosmos

## Clases

* CosmosClient
* Database
* Container
  
---

# Service Bus

## Paquetes

* Azure.Messaging.ServiceBus

## Clases

* ServiceBusClient
* ServiceBusSender
* ServiceBusReceiver
* ServiceBusProcessor
* ServiceBusMessage
* ServiceBusReceivedMessage

---

# EventGrid

## Paquetes 

* Azure.Messaging.EventGrid

## Clases

* EventGridPublisherClient
* AzureKeyCredential
* EventGridEvent 
* ServiceBusProcessor

# Event Hub

## Paquetes

* Azure.Messaging.EventHubs

## Clases

* EventHubProducerClient
* EventDataBatch
* EventData

---
 # Logging

 ## Paquetes
 
* Microsoft.Extensions.Logging
* Microsoft.Extensions.Logging.AzureAppServices
* Microsoft.Extensions.Logging.Console
* Microsoft.Extensions.Logging.Debug

## Configuracion para Log Stream en el Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Logging.AddAzureWebAppDiagnostics();
builder.Logging.SetMinimumLevel(LogLevel.Information);
```
---
# ApplicationInsights

## Paquetes 
* Microsoft.Extensions.Logging.ApplicationInsights
* Microsoft.ApplicationInsights.AspNetCore
  
## Configuracion Program.cs
```csharp
builder.Services.AddApplicationInsightsTelemetry();
builder.Logging.AddApplicationInsights();
```

## Configuracion appsettings.json
```json
{
  "ApplicationInsights": {
    "ConnectionString": "InstrumentationKey=your-instrumentation-key-here"
  },
```
---
# KeyVault

## Paquetes

* Azure.Identity
* Azure.Security.KeyVault.Secrets

## Clases

* DefaultAzureCredential
* SecretClient
* KeyVaultSecret

## Configuracion App Service

* @Microsoft.KeyVault(SecretUri=<SecretUri>)
