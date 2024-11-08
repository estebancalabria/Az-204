using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

string storageAccountUrl = "<URL STORAGE ACCOuNT>";
string storageAccountName = "<STORAGE ACCOuNT NAME>";
string strogageAccountKey = "<STORAGE ACCOUNT KEy>";

StorageSharedKeyCredential storageSharedKeyCredential = new StorageSharedKeyCredential(storageAccountName, strogageAccountKey);

//new BlobServiceClient()
BlobServiceClient client = new BlobServiceClient(new Uri(storageAccountUrl), storageSharedKeyCredential);

AccountInfo info = await client.GetAccountInfoAsync();

//Account Name
Console.WriteLine($"Account Kind: {info?.AccountKind}");
Console.WriteLine($"Sku Name: {info?.SkuName}");

// List Containers

await foreach (BlobContainerItem container in client.GetBlobContainersAsync())
{
    Console.WriteLine("Container:");
    Console.WriteLine($"\t{container.Name}");
    // List Blobs
    BlobContainerClient containerClient = client.GetBlobContainerClient(container.Name);
    Console.WriteLine("Blobs:");
    await foreach (BlobItem blob in containerClient.GetBlobsAsync())
    {
        Console.WriteLine($"\t{blob.Name}");
    }
}

//Creo un contenedor y subo un archivo
/*BlobContainerClient containerClient2 = client.GetBlobContainerClient("nuevocontenedor");
await containerClient2.CreateIfNotExistsAsync();
//Subo el archivo subir.txt
//creo un guid para el nombre del archivo .txt
BlobClient blobClient = containerClient2.GetBlobClient(Guid.NewGuid().ToString() + ".txt");
using FileStream fs = File.OpenRead(@"C:\Cursos\Az-204\Cursos\71846-Mar-Jue-No\clase-tres\BlobStorageDemo\Subir.txt");
await blobClient.UploadAsync(fs, true);*/

//Descargar un archiv del contenedor imagenes y guardarlo en la carpeta @"C:\Cursos\Az-204\Cursos\71846-Mar-Jue-No\clase-tres\BlobStorageDemo\
BlobContainerClient containerClient3 = client.GetBlobContainerClient("imagenes");
BlobClient blobClient2 = containerClient3.GetBlobClient("IG - Azure Key Concepts.jpg");
BlobDownloadInfo download = await blobClient2.DownloadAsync();
using FileStream fs = File.OpenWrite(@"C:\Cursos\Az-204\Cursos\71846-Mar-Jue-No\clase-tres\BlobStorageDemo\imagen.jpg");
await download.Content.CopyToAsync(fs);

//Muevo una imagen desde el contenedor imagenes al contenedor imagenes2
//BlobContainerClient containerClient4 = client.GetBlobContainerClient("imagenes2");
//BlobClient blobClient3 = containerClient3.GetBlobClient("IG - Azure Key Concepts.jpg");
//BlobClient blobClient4 = containerClient4.GetBlobClient("IG - Azure Key Concepts.jpg");
//await blobClient4.StartCopyFromUriAsync(blobClient3.Uri);
//await blobClient3.DeleteIfExistsAsync();






