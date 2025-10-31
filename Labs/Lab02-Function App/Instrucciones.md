# Lab 02 - Crear una Function App con Azure Core Function

## Instrucciones

Vamos a programar una function App por linea de comand en un entorno local y luego desplegarla en Azure

## Requisitos

* Tener Instalazo Azure Function Core Tools
> Descargar de (https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Cisolated-process%2Cnode-v4%2Cpython-v2%2Chttp-trigger%2Ccontainer-apps&pivots=programming-language-csharp)

* Tener Instalado el comando AZ
> Descargar de https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli

## Resumen Pasos

* Crear un Resource Group
* Crear un Storage Account
* Crear una function App
* Crear un directorio maquina local
* Crear un proyecto de FunctionApp con Azure Function Core Tools

```
  func init --worker-runtime dotnet-isolated --target-framework net9.0 --force
```

* Compilar Proyecto 
* Agregar funtion con Azure Function Core Tools

```
  func new --name "HolaMundo"
```
**Elegir HTTP Trigger**  

* Programar la funcion

```c#
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FunctionApp
{
    public class HolaMundo
    {
        private readonly ILogger<HolaMundo> _logger;

        public HolaMundo(ILogger<HolaMundo> logger)
        {
            _logger = logger;
        }

        [Function("HolaMundo")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Hola mundo esta es mi primer funcion de Function APP!");
        }
    }
}

```

* Probarla local

```
dotnet run
```

* Loguearse a Azure desde CLI local con comando AZ
* Publicar la funcion en Azure
* Ejecutarla desde Internet
