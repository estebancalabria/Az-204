# Laboratorio 7

Acceder a un Key Vault desde un App Service con Deployment Slots

## Resumen de Pasos

Parte 1
* Crear Resource Group
* Crear un plan
* Crear una Web App
* Crear local una app MVC
* Crear un slot de deployment en el portal
* Hacer deploy de la app
* Modificar las enviroement Variables test todo

Parte 2
* Crer un key Vault y crear secreto
* Darle permiso a la app para acceder al key vault
* Leer el secreto del key vault desde el app service



## Detalle de Pasos
## Parte 1

### Crear Resource Group (Azure)

```cli
 az group create --name Rg-Az204-Clase-Siete --location eastus 
```

### Crear un plan

```cli
 az appservice plan create --name Plan-S1 --resource-group Rg-Az204-Clase-Siete --location eastus --sku S1  
```

### Crear una Web App

```cli
 az webapp create --name secreatApp4trainner --resource-group Rg-Az204-Clase-Siete --plan Plan-S1 
```

### Crear local una app MVC (Local)

```cli
 dotnet new mvc --name demo-mv
```

#### Modificar el appsettings

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Titulo" : "Este es el titulo de mi app",
  "AllowedHosts": "*"
}

```

#### Modificar el HomeController

```csharp
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private string _titulo = "Titulo Sin Definir";

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _titulo = configuration["Titulo"]!.ToString();
    }

    public IActionResult Index()
    {
        this.ViewBag.Titulo = this._titulo;
        return View();
    }
```

#### Modificar el Index.cshtml

```html
@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <h1>@ViewBag.Titulo</h1>
    <p>Learn about <a href="https://learn.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
</div>
```

### Crear un slot de deployment en el portal (Azure)

```cli
az webapp deployment slot create --name secreatApp4trainner --resource-group Rg-Az204-Clase-Siete --slot testing   
```

### Publicar la App al Slot

#### Compilamos la app

```clie
dotnet publish -c release
```

#### Comprimimos el compilado

```powershell
powershell
Compress-Archive -Path ".\bin\Release\net8.0\Publish\*" -DestinationPath ".\deploy3.zip"
exit
```

##### Hacer deploy de la webapp

```
az login
az webapp deploy --resource-group Rg-Az204-Clase-Siete --name secreatApp4trainner  --src-path ./deploy3.zip --slot testing
```

### Modificar las enviroement Variables

Y a la opcion enviroment Variable y cambiarle el valor de Titulo

### Hacer Swap de slots y Ver el sitio

Hacer el swap de testing a prod
Verificar que el sitio se vea bien con la envitoment variable en ambos entornos

## Parte 2

### Crear un key vault

Importante :: En la segunda solapa usar Vault Access Policy

### Darle permisos a la app para acceder al key vault

#### Crear un managed Identity en el app service

#### Darle acceso al app service al Key Vault

### Modificarlas enviroment variables para que lean del key vault

@Microsoft.KeyVault(SecretUri=https://kv4trainner2.vault.azure.net/secrets/TituloApp)