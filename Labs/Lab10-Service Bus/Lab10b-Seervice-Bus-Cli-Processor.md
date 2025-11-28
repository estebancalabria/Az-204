# Laboratorio 10b - Alternativa utilizndo el Cli y Procesando Mensajes Apenas llegan

## Setup Laboratorio

* Crear el Resource Group

```bash
  az group create --name rg-az204-lab-10 --location westus
```

* Crear el Service Bus

```bash
az servicebus namespace create --resource-group rg-az204-lab-10 --name bus4trainner --location westus --sku Standard          
```

* Crear una queue

```bash
az servicebus queue create --resource-group rg-az204-lab-10 --namespace-name bus4trainner --name queue4trainner                
```

*  Crear una policy de listen, send y Manage para la queue

```bash
az servicebus queue authorization-rule create --resource-group rg-az204-lab-10 --namespace-name bus4trainner --queue-name queue4trainner --name root --rights Listen Send Manage
```

## Aplicacion que envia

* Crear la aplicacion

```cmd
dotnet new console --name ServiceBusSenderWithKey
```

* Agregar la libreria para trabajar con Service Bus

```cmd
dotnet add package Azure.Messaging.ServiceBus
```

* Crear la aplicacion en C#

``` c#
using Azure;
using Azure.Messaging.ServiceBus;

Console.WriteLine("Ingrese el endpoint del service bus");
string endpoint = Console.ReadLine()!;

Console.WriteLine("Ingrese la clave de acceso del service bus");
string accessKey = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la cola");
string queueName = Console.ReadLine()!;

Console.WriteLine("Ingrese el mensaje enviado al service bus");
string message = Console.ReadLine()!;

var client = new ServiceBusClient(endpoint, new AzureNamedKeyCredential("root", accessKey));
ServiceBusSender sender = client.CreateSender(queueName);

ServiceBusMessage busMessage = new ServiceBusMessage(message);
await sender.SendMessageAsync(busMessage);
Console.WriteLine("Mensaje enviado correctamente");
```

## Aplicaion que recibe apenas se envian

* Crear la aplicacion

```cmd
dotnet new console --name ServiceBusProcessor
```

* Agregar la libreria para trabajar con Service Bus

```cmd
dotnet add package Azure.Messaging.ServiceBus
```

* Crear la aplicacion en C#

```c#
using Azure;
using Azure.Messaging.ServiceBus;

Console.WriteLine("Ingrese el endpoint del service bus");
string endpoint = Console.ReadLine()!;

Console.WriteLine("Ingrese la clave de acceso del service bus");
string accessKey = Console.ReadLine()!;

Console.WriteLine("Ingrese el nombre de la cola");
string queueName = Console.ReadLine()!;

var client = new ServiceBusClient(endpoint, new AzureNamedKeyCredential("root", accessKey));

var processor = client.CreateProcessor(queueName);

processor.ProcessMessageAsync += async args =>
{
    string body = args.Message.Body.ToString();
    Console.WriteLine($"Mensaje recibido: {body}");

    await args.CompleteMessageAsync(args.Message);
};

processor.ProcessErrorAsync += args =>
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
};

await processor.StartProcessingAsync();

Console.WriteLine("Presione cualquier tecla para detener el procesamiento de mensajes");
Console.ReadKey();

await processor.StopProcessingAsync();
```

## Ejecucion de Ambas y pruebas

* Leeer el endpoint con el comando az

```bash
az servicebus namespace show --resource-group rg-az204-lab-10 --name bus4trainner --query "serviceBusEndpoint" -o tsv          
```

* Leeer la key con el comando az

```bash
az servicebus queue authorization-rule keys list --resource-group rg-az204-lab-10 --namespace-name bus4trainner --queue-name queue4trainner --name root --query primaryKey -o tsv
```
 
* Darle Dot net run primero al que recibe


* Darle dot net run luego al que envia
