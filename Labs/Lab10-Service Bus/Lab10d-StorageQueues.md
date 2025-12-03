# Lab 10b - Storage Queues

## Setup del Proyecto

* Crear un resource Group

```az
az group create --name rg-az204-lab-10 --location westus
```

* Crear un Storage Account

```az
az storage account create --name cs4queue --resource-group rg-Az204-lab-10 --location westus --sku Standard_LRS               
```

* Crear una Queue

```az
az storage queue create --name queue4trainner --account-name cs4queue  
```

## Aplicacion que genera mensajes en una Queue

* Crear la aplicacion

```cmd
dotnet new console --name QueueSender
```

* Pararse en el directorio de la Aplicacion

```cmd
cd QueueSender
```

* Agregar los paquetes necesarios

```cmd
dotnet add package Azure.Storage.Queues
```

* Crear el programa que manda mensajes a la Queue

```cmd
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

Console.WriteLine("=== Azure Storage Queue Sender ===\n");

// Solicitar datos al usuario
Console.Write("Enter Storage Account connection string: ");
string? connectionString = Console.ReadLine();

Console.Write("Enter Queue name: ");
string? queueName = Console.ReadLine();

Console.Write("Enter message to send: ");
string? message = Console.ReadLine();


try
{
    // Crear cliente de Queue
    QueueClient queueClient = new QueueClient(connectionString, queueName);
    
    // Crear la cola si no existe
    await queueClient.CreateIfNotExistsAsync();
    
    // Enviar mensaje
    SendReceipt receipt = await queueClient.SendMessageAsync(message);
    
    Console.WriteLine($"\n✓ Message sent successfully!");
    Console.WriteLine($"  Message ID: {receipt.MessageId}");
    Console.WriteLine($"  Pop Receipt: {receipt.PopReceipt}");
    Console.WriteLine($"  Insertion Time: {receipt.InsertionTime}");
}
catch (Exception ex)
{
    Console.WriteLine($"\n✗ Error: {ex.Message}");
}

```

* Probar el programa

```cmd
dotnet run
```

## Aplicacion que consume mensajes de la queue 
