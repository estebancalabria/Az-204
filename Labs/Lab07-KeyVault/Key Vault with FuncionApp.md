# Laboratorio 7B - Key Vault With Function App

> (Similar al laboratorio oficial Lab 07: Access resource secrets more securely across services)
> https://microsoftlearning.github.io/AZ-204-DevelopingSolutionsforMicrosoftAzure/Instructions/Labs/AZ-204_lab_07.html

* Crear un Resource Group

```bash
az group create --name rg-az204-clase-07 --location westus  
```

* Crear un Storage Account

```powershell
New-AzStorageAccount -ResourceGroupName "rg-az204-clase-07" -Name cs4secureapp -Location 'westUS' -SkuName Standard_LRS
```

* Crear un container en el storage account

    * Opcion 1
      
```powershell
New-AzStorageContainer -Name "drop" -Permission Off -Context (Get-AzStorageAccount -ResourceGroupName "rg-az204-clase-07" -Name "cs4secureapp").Context
```

    * Opcion 2 : Con accont Key
    
```powershell
$storageAccountKey = (Get-AzStorageAccountKey -ResourceGroupName "rg-az204-clase-07" -AccountName "cs4secureapp")[0].Value

New-AzStorageContainer -Name "drop" -Permission Off -Context (New-AzStorageContext -StorageAccountName cs4secureapp -StorageAccountKey $storageAccountKey)
```

* Crear un archivo con un json en el Azure Cli

```powershell
Set-Content -Path "datos.json" -Value (Invoke-RestMethod -Uri "https://jsonplaceholder.typicode.com/users/1/todos" | ConvertTo-Json -Compress)
```

* Subir el archivo datos.json a un blob de un storage account

```
Set-AzStorageBlobContent -File "datos.json" -Container "drop" -Blob "datos.json" -Context (Get-AzStorageAccount -ResourceGroupName "rg-az204-clase-07" -Name "cs4secureapp").Context -Force



```

* 

* Crear un Key Vault

```powershell
New-AzKeyVault -Name kv4secureapp -ResourceGroupName "rg-az204-clase-07" -Location 'westUS' -DisableRbacAuthorization -EnablePurgeProtection:$false -SoftDeleteRetentionInDays 7
```

* Crear un Access Policy para nuestro usuario
> Este paso es necesario si lo hacemos desde el cli, desde el portal lo hace automaticamente

```powershell
Set-AzKeyVaultAccessPolicy -VaultName kv4secureapp -UserPrincipalName "esteban.calabria_gmail.com#EXT#@estebancalabriagmail.onmicrosoft.com" -PermissionsToKeys get,wrapKey,unwrapKey,sign,verify,list -PermissionsToSecrets list,set,get,delete -PassThru 
```

* Crear una funtion app

```powershell
New-AzFunctionApp -Name func4secureapp -ResourceGroupName "rg-az204-clase-07" -Location 'westUS' -StorageAccountName cs4secureapp -Runtime dotnet -RuntimeVersion 8 -DisableApplicationInsights -FunctionsVersion 4 -OSType Windows
```
> OJO: Desde el Portal o el comando Az se puede utilizar dotnet 9. Pero desde el comando de powershell no se puede crear una futionapp con dotnet 9

* Darle un Indentity (Service Principal) a la FuntionApp

```bash
az functionapp identity assign --name func4secureapp --resource-group rg-az204-clase-07
```

* Obtener el connection String Del Storage Account

```bash
az storage account show-connection-string --name cs4secureapp --resource-group rg-az204-clase-07
```

* Guardar ese Connection String como secreto del Key Vault

```powershell
$connectionString = az storage account show-connection-string --name cs4secureapp --resource-group rg-az204-clase-07 --query connectionString -o tsv

Set-AzKeyVaultSecret -VaultName kv4secureapp -Name storagecredentials -SecretValue (ConvertTo-SecureString -String $connectionString -AsPlainText -Force)
```
* Crear una access policy para que la function app pueda leer secretos del key vault

```powershell
$identityfuncapp = az functionapp show --name func4secureapp --resource-group rg-az204-clase-07 --query identity.principalId --output tsv

az keyvault set-policy   --name kv4secureapp  --object-id $identityfuncapp  --secret-permissions get list
```

* Crear una configuracion en la Function App que lea el secreto del Key Vault

```az
az functionapp config appsettings set --name func4secureapp --resource-group rg-az204-clase-07  --settings "StorageConnectionString=@Microsoft.KeyVault(SecretUri=https://kv4secureapp.vault.azure.net/secrets/storagecredentials)"
```

* Crear LOCALMENTE una function APP

```cmd
func init --worker-runtime dotnet-isolated --target-framework net8.0 --force
```
  
* Editar el proyecto con el Visual Studio Code

```cmd
code .
```

* Agregar una funcion de tipo HTTP Trigger

```
func new --template "HTTP trigger" --name "FileParser"
```

* Modificar el localsettings.json para agregar el valor que vamos a leer del Key Vault para probar

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
        "StorageConnectionString" : "[TEST VALUE]"
    }
}
```

* Editar el Archivo FileParser.cs para que lea el valor de StorageConnectionString de los settings

```c#
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using System.Net;

namespace FunctionApp
{
    public class FileParser(ILogger<FileParser> logger)
    {
        private readonly ILogger<FileParser> _logger = logger;

        [Function("FileParser")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString") ?? "No connection string found.";
            await response.WriteStringAsync(connectionString);

            return response;
        }
    }
}
```

*  Ejecutar y probar la funcion localmente

```cmd
  func start
```

* Lo probamos Localmente

* Loguearse en Azure

```
az login
```

* Hacer deploy de la Function App en Azure

```cmd
func azure functionapp publish func4secureapp --dotnet-version 8.0
```

* Ir al portal y probar la function app
    * El el portal en la parte de Overview Aparece la Fuction
    * Elegir la funcion y en la parte de Code+test podes sacar la url de la funcion con la key en el querystring

