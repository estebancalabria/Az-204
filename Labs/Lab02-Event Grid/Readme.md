# Laboratorio 9. Event Grid

* Crear el Resource Group del Laboratorio

> rg-az204-lab-09

* Crear el Event Grid Topic

> topic-lab09

* Desplegar la aplicacion visor de Eventos
    * Nombre : eventviewer-lab09
    * Publish : Container 
    * Operating System : Linux
    * Plan : B1 
    * Imagen : microsoftlearning/azure-event-grid-viewer:latest

* Probar el despliegue

* Crear una subscripcion en el topico para la aplicacion
   * Nombre : subscription-4-eventviewer-lab09
   * Endpoint : https://eventviewer-lab09.azurewebsites.net/api/update

* Verficar en la aplicacion web que efectivamente se haya recibido el evento de subscripcion

* Crear una aplicacion para publicar Eventos en el topico

```cmd
   dotnet new console --name EventGridPublisher
```

* Editar esta aplicacion con VSCode

```cmd
code .
```

* Instalar el paquete Azure.Messaging.EventGrid

``` cmd
  dotnet add package Azure.Messaging.EventGrid
```

* Codificar el envio de un evento

```c#
using Azure;
using Azure.Messaging.EventGrid;


Console.WriteLine("Ingrese el endpoint del topic");
string topicEndpoint = Console.ReadLine()!;

Console.WriteLine("Ingrese la key del topic");
string topickey = Console.ReadLine()!;

Console.WriteLine("Ingrese informacion para mandar en el evento");
string data = Console.ReadLine()!;

AzureKeyCredential credential = new AzureKeyCredential(topickey);
EventGridPublisherClient client = new EventGridPublisherClient(new Uri(topicEndpoint), credential);

EventGridEvent eventGridEvent = new EventGridEvent(
    subject: "Test.Subject",
    eventType: "Test.EventType",
    dataVersion: "1.0",
    data: new { Info = data }
);

await client.SendEventAsync(eventGridEvent);

Console.WriteLine("Evento enviado correctamente.");
```

* Ejecutar la aplicacion

```cmd
dotnet run
```

> Nececitamos ingresar el topicurl y la key

* Verificar en la aplicacion que se recibio el evento.
