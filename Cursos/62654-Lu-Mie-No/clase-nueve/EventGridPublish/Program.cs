using Azure;
using Azure.Messaging.EventGrid;

String key = "XqbSVaDcuDZ1mdDvo0SHndGEgbPItHggOAZEGFPXmpg=";
String endpoint = "https://topic4trainner.eastus-1.eventgrid.azure.net/api/events";

EventGridPublisherClient client = new EventGridPublisherClient(new Uri(endpoint), 
            new AzureKeyCredential(key));

EventGridEvent evento = new EventGridEvent(
     subject: "Mi Evento Magico",
     eventType: typeof(EventGridEvent).ToString(),
     dataVersion: "1.0",
     data: new {
        mensaje = "Hola Mundo, soy un Event Grid",
        autor = "Juan Carlos"
     }
);

client.SendEvent(evento);

