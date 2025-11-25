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

* 


