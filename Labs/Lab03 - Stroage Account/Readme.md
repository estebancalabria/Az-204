
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
