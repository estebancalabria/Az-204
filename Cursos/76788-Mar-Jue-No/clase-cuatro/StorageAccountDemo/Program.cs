using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

String key = "2CWb4hJ56JI8oPZfLj6d+2P32g3Yak5Zn+fLuWl4dcQk+/4Ut0Tf2bANdopByTKCJNVfQ6oUdbEA+ASt4FGzlQ==";
String endpoint = "https://cs4trainner.blob.core.windows.net/";
String storageAccountName = "cs4trainner";

StorageSharedKeyCredential credential = new StorageSharedKeyCredential(storageAccountName, key);

//Listar el contenido
/*BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(endpoint), credential);

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
}*/

/*Crear un blob nuevo*/
/*BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(endpoint), credential);
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("created-from-home");
containerClient.CreateIfNotExists();
Console.WriteLine("Contenedor creado");*/

//Subir un blob
/*BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(endpoint), credential);
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("created-from-home");
BlobClient blobClient = containerClient.GetBlobClient("demo.csproj");
blobClient.Upload(@"C:\Cursos\Az-204\Cursos\76788-Mar-Jue-No\clase-cuatro\StorageAccountDemo\StorageAccountDemo.csproj");
Console.WriteLine("Blob subido");*/

BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(endpoint), credential);
BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("demo");
BlobClient blobClient = containerClient.GetBlobClient("grok-video-5d79d6ed-ec2d-4b5c-9f9f-9900fb21e5a4.mp4");
blobClient.DownloadTo(@"C:\Cursos\Az-204\Cursos\76788-Mar-Jue-No\clase-cuatro\StorageAccountDemo\grok-video-5d79d6ed-ec2d-4b5c-9f9f-9900fb21e5a4.mp4");
Console.WriteLine("Blob descargado");