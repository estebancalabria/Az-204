# Lab 11 - Logging with Application Insights


## Setup del Entorno

* Crear el Resource Group

```
  az group create --name rg-az204-lab-11 --location westus 
```

* Crear el Log Analics Workspace

* Ver las tablas creadas es en Log Analitics Workspace en el portal

* Crear Application Insights
    * (para guardar datos en el worspace anterior)

## Crear Applicacion que escriba en el log de Application Insights


* Crear la applicacion

```bash
dotnet new console --name ApplicationInsightsLogger
```

* Ir al directorio de la aplicacion

```bash
cd ApplicationInsightsLogger
```

* Instalar las liberias

```bash
dotnet add package Microsoft.ApplicationInsights
dotnet add package Microsoft.ApplicationInsights.DependencyCollector
```

* Editarla con el Vscode

```bash
code .
```

* Escribir el siguiente Codigo en el Program.cs

```c#

using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;


Console.WriteLine("Increse el Connection String del Application Insights:");
string connectionString = Console.ReadLine()!;


TelemetryConfiguration configuration = new TelemetryConfiguration
{
    ConnectionString = connectionString
};

var telemetryClient = new TelemetryClient(configuration);

telemetryClient.TrackTrace("Aplicacion Iniciada");

Console.WriteLine("Aplicacion Iniciada");
telemetryClient.TrackEvent("AppStarted");

try
{
    Console.WriteLine("Ingrese el divisor:");
    int divisor = int.Parse(Console.ReadLine()!);
    int result = 10 / divisor;
    Console.WriteLine($"Resultado: {result}");
    telemetryClient.TrackEvent("DivisionCompleted");
}
catch (Exception ex)
{
    Console.WriteLine($"Ocurrio un error: {ex.Message}");
    telemetryClient.TrackException(ex);
}

telemetryClient.GetMetric("AppRuns").TrackValue(1);

telemetryClient.Flush();
// Esperar para asegurar que los datos se envien antes de que la aplicacion termine
System.Threading.Thread.Sleep(1000);
Console.WriteLine("Aplicacion Finalizada");
telemetryClient.TrackEvent("AppEnded");
```

* Ejecutar la aplicacion

```bash
dotnet run
```

> Recomiendo esperar unos minutos para que se efective la ingesta de los datos el en log

## Consultar el Log en Azure

* En Log Analitics Workspace se puede consultar con KQL en la opcion logs Ls Tablas
  * AppEvents                    <<<    **telemetryClient.TrackEvent("AppEnded")**
  * AppExceptions                <<<    **telemetryClient.TrackException(ex)**
  * AppTraces                    <<<    **telemetryClient.TrackTrace("Aplicacion Iniciada");**
  * AppMetrics                   <<<    **telemetryClient.GetMetric("AppRuns").TrackValue(1);**
  
* En el Application Insights se pueden consultar las tablas
  * customEvents                 <<<    **telemetryClient.TrackEvent("AppEnded")**
  * exceptions                   <<<    **telemetryClient.TrackException(ex)**
  * traces                       <<<    **telemetryClient.TrackTrace("Aplicacion Iniciada");**
  * customMetrics                <<<    **telemetryClient.GetMetric("AppRuns").TrackValue(1);**
* Tambien los eventos registrados los podemos ver en la parte de
    * Usage ... Events
* Tambien las excepciones se pueden ver en la parte de
    * Investigage ... Failures 

