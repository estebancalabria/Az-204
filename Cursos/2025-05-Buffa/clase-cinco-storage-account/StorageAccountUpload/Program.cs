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

Console.WriteLine("Ingrese el nombre de contenedor (se creara si no existe):");
string containerName = Console.ReadLine()!;

Console.WriteLine("Ingrese el path del archivo a subir:");
string filePath = Console.ReadLine()!;


Console.WriteLine("Conectando al Blob Storage...");
BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri(endpoint), new StorageSharedKeyCredential(storageAccountName, key));
Console.WriteLine("Conexion exitosa al Blob Storage.");

BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName); //Anonimo
Console.WriteLine($"Creando contenedor '{containerName}' si no existe.");
if (!containerClient.Exists())
{
    containerClient.CreateIfNotExists();    
}

Console.WriteLine($"Subiendo archivo '{filePath}' al contenedor '{containerName}'...");
 //Lo crea localmente pero en Azure no existe hasta que se suba el archivo
BlobClient blobClient = containerClient.GetBlobClient(Path.GetFileName(filePath)); 

using (FileStream uploadFileStream = File.OpenRead(filePath))
{
    await blobClient.UploadAsync(uploadFileStream, true);
    uploadFileStream.Close();
}

Console.WriteLine($"Archivo '{filePath}' subido exitosamente al contenedor '{containerName}'.");
Console.WriteLine("Operación completada. Presione cualquier tecla para salir.");
Console.ReadKey();
// Fin del programa
