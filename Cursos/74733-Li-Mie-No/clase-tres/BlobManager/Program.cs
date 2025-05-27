

using System.Reflection.Metadata;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

Console.WriteLine("Input your Azure Storage Endpont:");
string blobEndpoint = Console.ReadLine()!.Trim();

Console.WriteLine("Input your Azure Storage Account Name:");
string storageAccountName= Console.ReadLine()!.Trim();

Console.WriteLine("Input your Azure Storage Account Key:");
string storageAccountKey = Console.ReadLine()!.Trim();

StorageSharedKeyCredential storageSharedKeyCredential = new StorageSharedKeyCredential(storageAccountName, storageAccountKey);
BlobServiceClient client = new BlobServiceClient(new Uri(blobEndpoint), storageSharedKeyCredential);
AccountInfo info = client.GetAccountInfo();

Console.WriteLine("Conectad to Azure Storage Account successfully!");

Console.WriteLine($"Tipo de Cuenta {info.AccountKind}");
Console.WriteLine($"SKU {info.SkuName}");

Console.WriteLine("Containers : ");
foreach (BlobContainerItem container in client.GetBlobContainers()){
    Console.WriteLine($"   Container: {container.Name}");

    BlobContainerClient containerClient = client.GetBlobContainerClient(container.Name);
    
    if (containerClient.GetBlobs().Count()==0){
        Console.WriteLine($"   No blobs found in this container.");
    } else {
        Console.WriteLine($"   Blobs: ");
    }


    foreach (BlobItem blob in containerClient.GetBlobs()){
        Console.WriteLine($"       Blob: {blob.Name}");
        //BlobClient blobClient = containerClient.GetBlobClient(blob.Name);
        //blobClient.DownloadTo($"./{blob.Name}");
    }
}

Console.WriteLine("Creando Container Nuevo...");
BlobContainerClient nuevoBlobContainerClient = client.GetBlobContainerClient("nuevo");
nuevoBlobContainerClient.CreateIfNotExists(PublicAccessType.BlobContainer);
BlobClient blobClientNuevo = nuevoBlobContainerClient.GetBlobClient("readme.txt");
blobClientNuevo.Upload("./Program.cs", true);

Console.WriteLine("Copiando archivo de un contenedor a otro...");
BlobClient destino = client.GetBlobContainerClient("raster-graphics").GetBlobClient("readme-copia.txt");
Console.WriteLine($"   Copiando {blobClientNuevo.Uri} a {client.GetBlobContainerClient("raster-graphics").GetBlobClient("readme-copia.txt").Uri}");
destino.StartCopyFromUri(blobClientNuevo.Uri);


Console.WriteLine("Enter any key to continue...");
Console.ReadKey();