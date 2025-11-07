
#  Lab03 - Storage Account - Containers 

* Crear el Resource Group

* Crear un Storage Account

* Crear Container

* Subir archivo a Container

* Crear proyecto de .NET

``` bash
  dotnet new console --name StorageAccountDemo
```
* Agregar el paquete del stocage account

``` bash
  dotnet add package Azure.Storage.Blobs
```

* Codigo para listar Containers y Blobs dentro de Containers

```c#
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

String key = "xxxxxxx";
String endpoint = "https://xxxxxx.blob.core.windows.net/";
String storageAccountName = "xxxxxxx";

StorageSharedKeyCredential credential = new StorageSharedKeyCredential(storageAccountName, key);

//Listar el contenido
BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(endpoint), credential);

AccountInfo accountInfo = blobServiceClient.GetAccountInfo();
Console.WriteLine($"Account Kind: {accountInfo.AccountKind}");
Console.WriteLine($"SKU Name: {accountInfo.SkuName}");

await foreach (BlobContainerItem containerItem in blobServiceClient.GetBlobContainersAsync())
{
    Console.WriteLine($"Container Name: {containerItem.Name}");

    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerItem.Name);

    await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
    {
        Console.WriteLine($"\tBlob Name: {blobItem.Name}");
    }
}

```

* Codigo para crear un contenedor nuevo

```c#
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

String key = "xxxxxxxxxxx";
String endpoint = "https://xxxxxx.blob.core.windows.net/";
String storageAccountName = "xxxxxx";

StorageSharedKeyCredential credential = new StorageSharedKeyCredential(storageAccountName, key);

/*Crear un blob nuevo*/
BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(endpoint), credential);
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("created-from-home");
containerClient.CreateIfNotExists();
Console.WriteLine("Contenedor creado");
```

* Subir un blob

```c#

using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

String key = "xxxxxxxxxxx";
String endpoint = "https://xxxxxx.blob.core.windows.net/";
String storageAccountName = "xxxxxx";

StorageSharedKeyCredential credential = new StorageSharedKeyCredential(storageAccountName, key);

//Subir un blob
BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(endpoint), credential);
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("created-from-home");
BlobClient blobClient = containerClient.GetBlobClient("demo.csproj");
blobClient.Upload(@"C:\Cursos\Az-204\Cursos\76788-Mar-Jue-No\clase-cuatro\StorageAccountDemo\StorageAccountDemo.csproj");
Console.WriteLine("Blob subido");
```

* Bajar unblob a mi computadora

```c#
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

String key = "xxxxxxxxxxx";
String endpoint = "https://xxxxxx.blob.core.windows.net/";
String storageAccountName = "xxxxxx";

StorageSharedKeyCredential credential = new StorageSharedKeyCredential(storageAccountName, key);

BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(endpoint), credential);
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("demo");
BlobClient blobClient = containerClient.GetBlobClient("grok-video-5d79d6ed-ec2d-4b5c-9f9f-9900fb21e5a4.mp4");
blobClient.DownloadTo(@"C:\Cursos\Az-204\Cursos\76788-Mar-Jue-No\clase-cuatro\StorageAccountDemo\grok-video-5d79d6ed-ec2d-4b5c-9f9f-9900fb21e5a4.mp4");
Console.WriteLine("Blob descargado");
```

#  Lab03 - Storage Account - Queues

* Crear el Resource Group

* Crear un Storage Account

* Crear Queue

* Crear proyecto de .NET

``` bash
  dotnet new console --name QueueDemo
```

* Agregar el paquete

```bash
  dotnet add package Azure.Storage.Queues
```

* Codigo para agrear mensaje a la Queue

```c#
using Azure.Storage.Queues;


String connectionString = "xxxxxxxxxx";
String queueName = "queue4trainner";

QueueClient queueClient = new QueueClient(connectionString, queueName);
Console.WriteLine("Enter a message to send to the queue:");
String message = Console.ReadLine()!;
queueClient.SendMessage(message);

```
