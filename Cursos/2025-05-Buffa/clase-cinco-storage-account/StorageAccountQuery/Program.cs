using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models; 

Console.WriteLine("Ingrese el nombre del Storage Account:");
string storageAccountName = Console.ReadLine()!;

//Console.WriteLine("Ingrese el endopoint del Storage Account:");
//string endpoint = Console.ReadLine()!;
string endpoint = $"https://{storageAccountName}.blob.core.windows.net/";

Console.WriteLine("Ingrese la key del Storage Account:");
string key = Console.ReadLine()!;

Console.WriteLine("Conectando al Blob Storage...");
BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(endpoint), new StorageSharedKeyCredential(storageAccountName, key));
Console.WriteLine("Conexion exitosa al Blob Storage.");

Console.WriteLine("Mostrando Informacion del Blob Storage:");
AccountInfo accountInfo = blobServiceClient.GetAccountInfo();
Console.WriteLine($"Tipo de cuenta: {accountInfo.AccountKind}");
Console.WriteLine($"SKU: {accountInfo.SkuName}");

Console.WriteLine("Listando contenedores y blobs...");
foreach (BlobContainerItem container in blobServiceClient.GetBlobContainers())
{
    Console.WriteLine($"    Container: - {container.Name}");
    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(container.Name);
    foreach (BlobItem blob in containerClient.GetBlobs())
    {
        Console.WriteLine($"        Blob: - {blob.Name}");
    }
}

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();


