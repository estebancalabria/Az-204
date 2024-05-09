

using System.Reflection.Metadata;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

var storageAccountName = "";
//var blobEndpoint = @"https://mediastor4trainner.blob.core.windows.net/";
var blobEndpoint = $@"https://{storageAccountName}.blob.core.windows.net/";
var storageAccountKey = "";

StorageSharedKeyCredential cred = new StorageSharedKeyCredential(storageAccountName, storageAccountKey);
BlobServiceClient blob = new BlobServiceClient(new Uri(blobEndpoint), cred);
Console.WriteLine(blob.AccountName);
Console.WriteLine("Account Kind: " + blob.GetAccountInfo().Value.AccountKind);
Console.WriteLine("Account Sku: " + blob.GetAccountInfo().Value.SkuName);

Console.WriteLine("");
Console.WriteLine("Listando contenedores");
foreach (BlobContainerItem container in blob.GetBlobContainers())
{
    Console.WriteLine(container.Name);
    //listar los blobs dentro del container
    BlobContainerClient containerClient = blob.GetBlobContainerClient(container.Name);
    foreach (BlobItem blobItem in containerClient.GetBlobs())
    {
        Console.WriteLine($" Blob : {blobItem.Name}");
    }
}

Console.WriteLine("");
Console.WriteLine("Creando contenedor demo si no existe");
if (!blob.GetBlobContainers().Any(c => c.Name == "demo")){
    Console.WriteLine("No existe el contenedor demo, creando...");
    BlobContainerClient nuevoContainer = blob.CreateBlobContainer("demo");
    nuevoContainer.CreateIfNotExists();
    BlobClient nuevoBlob = nuevoContainer.GetBlobClient("Program.cs");
    nuevoBlob.Upload(@"C:\Cursos\Az-204\Cursos\62654-Lu-Mie-No\clase-tres\StorageAccountDemo\Program.cs");
}else{
    Console.WriteLine("El contenedor demo ya existe");
}

Console.WriteLine("");
Console.WriteLine("Descargando una imagen");
BlobContainerClient contenedor = blob.GetBlobContainerClient("raster-graphics");
BlobClient archivo = contenedor.GetBlobClient("graph.jpg");
archivo.DownloadTo(@"C:\Cursos\Az-204\Cursos\62654-Lu-Mie-No\clase-tres\StorageAccountDemo\downloads\descarga.jpg");




// See https://aka.ms/new-console-template for more information
Console.WriteLine("Fin del programa");
