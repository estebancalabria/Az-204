# Laboratorio 10 - Procesando mensajes Asincronicos con Azure Service Bus

## Setup del laboratorio

* Crear Resource Group del Laboratorio

```bash
  az group create --name rg-az204-lab-10 --location westus   
```

* Crear un Service Bus Queue
    * Elegir un nombre unico
    * Pricing Tier : Basic

* Crear una queue

## Aplicacion que Envia Mensajes

* Crear un proyecto para Enviar mensajes a la cola

```cmd
dotnet new console --name ServiceBusSender
cd ServiceBusSender
```

* Agregar la libreria para trabajar con Service Bus

```cmd
dotnet add package Azure.Messaging.ServiceBus
```

* Codigo del Program.cs quedaria

```c#
Console.Write("Ingrese el connection string del Service Bus");
string connectionString = Console.ReadLine()!;

Console.Write("Ingrese el nombre de la queue");
string queueName = Console.ReadLine()!;

Console.WriteLine("Ingrese el contenido del mensaje");
string messageBody = Console.ReadLine()!;

var client = new Azure.Messaging.ServiceBus.ServiceBusClient(connectionString);
var sender = client.CreateSender(queueName);

var message = new Azure.Messaging.ServiceBus.ServiceBusMessage(messageBody);

await sender.SendMessageAsync(message);

Console.WriteLine("Mensaje enviado exitosamente.");

await sender.DisposeAsync();
await client.DisposeAsync();
```

* Ir a la queue y crear una policy para obtener el connection String
  * Elegir la Queue
    * Settings
      * Shared Access Policy
        * + Add

* Ejecutar el proyecto

```cmd
dotnet run
```

* Validar Envio en el portal
  * Mirar en el overview de la queue las estadisticas de la queue en el portal
  * Mirar los mensajes enviados en la opcion de Service Bus Explorer en el portal

## Aplicacion que Recibe Mensajes

* Crear la aplicacion para recibir mensajes

```cmd
dotnet new console --name ServiceBusReceiver
```

* Agregar la libreria

```cmd
 dotnet add package Azure.Messaging.ServiceBus
```

* Programar la recepcion del mensaje en c#

```c#
Console.WriteLine("Ingrese el Connection String de la queue");
string connectionString = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la queue");
string queueName = Console.ReadLine()!;

var client = new Azure.Messaging.ServiceBus.ServiceBusClient(connectionString);
var receiver = client.CreateReceiver(queueName);

var message = await receiver.ReceiveMessageAsync();
if (message != null)
{
    Console.WriteLine($"Mensaje recibido: {message.Body}");
    await receiver.CompleteMessageAsync(message);
}
else
{
    Console.WriteLine("No hay mensajes en la queue.");
}

await receiver.CloseAsync();
await client.DisposeAsync();
```

* Crear la policy

* Ejecutar el programa

* Verificar que la cola este vacia en e portal
