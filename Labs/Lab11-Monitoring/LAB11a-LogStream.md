# 🧪 Laboratorio: Logging con Azure App Service y .NET MVC

---

### 🎯 Objetivo

Registrar logs con `ILogger` y verlos en tiempo real en el **Log Stream** desde el portal de Azure y desde la línea de comandos.

---

### 1. 🧱 Crear el proyecto MVC

```bash
dotnet new mvc -n LoggingDemoApp
cd LoggingDemoApp
```

---

### 2. 🔧 Modificar el `HomeController`

Editá `Controllers/HomeController.cs`:

```csharp
private readonly ILogger<HomeController> _logger;

public HomeController(ILogger<HomeController> logger)
{
    _logger = logger;
}

public string LogDemo()
{
    _logger.LogInformation("Log: This is an information log message.");
    _logger.LogWarning("Log: This is a warning log message.");
    _logger.LogError("Log: This is an error log message.");

    // Console.WriteLine NO se ve en Azure
    Console.WriteLine("Console: Logging demo completed. Check the logs for messages.");

    return "Logging demo completed. Check the logs for messages.";
}
```

📎 URL: `/home/logdemo`

---

### 3. 📦 Instalar paquete para Azure Logging

```bash
dotnet add package Microsoft.Extensions.Logging.AzureAppServices
```

---

### 4. 🔧 Configurar el logging en `Program.cs`

Editá `Program.cs`:

```csharp
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddAzureWebAppDiagnostics(); // 🔑 Necesario para Log Stream
builder.Logging.SetMinimumLevel(LogLevel.Trace);
```

---

### 5. ⚙️ Configurar niveles en `appsettings.json`

Editá `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Trace",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

### 6. 🧪 Probar local

```bash
dotnet run
```

Accedé a: `http://localhost:5000/home/logdemo`

✔️ Deberías ver los logs en consola local.

---

### 7. 📤 Publicar la app

```bash
dotnet publish -c Release -o publish
cd publish
zip -r ../deploy.zip *
cd ..
```

---

### 8. 🌐 Crear recursos en el Portal Azure

1. Iniciá sesión en [portal.azure.com](https://portal.azure.com)
2. Crear:

   * Resource Group: `Rg-LoggingDemo`
   * App Service Plan (Free o Básico)
   * Web App (ej: `app4trainer`, runtime: .NET 8/9)

---

### 9. 🚀 Subir la app a Azure

```bash
az webapp deployment source config-zip \
  --name app4trainer \
  --resource-group Rg-LoggingDemo \
  --src deploy.zip
```

---

### 10. ✅ Activar logs desde el Portal

En el **Portal de Azure**:

1. Ir a la Web App.
2. En el menú izquierdo, buscar **App Service logs**.
3. Activar:

   * ✅ Application Logging (Filesystem): **Information** o **Verbose**
4. Guardar.

---

### 11. 🔍 Ver logs en vivo

#### A. Desde la terminal

```bash
az webapp log tail --name app4trainer --resource-group Rg-LoggingDemo
```

#### B. Desde el portal

1. Ir a la Web App en el Portal de Azure.
2. En el menú lateral, buscar **Log Stream**.
3. Abrí el streaming y visitá:

   ```
   https://app4trainer.azurewebsites.net/home/logdemo
   ```

✔️ Verás logs como:

```
[Information] Log: This is an information log message.
[Warning] Log: This is a warning log message.
[Error] Log: This is an error log message.
```

---

### ⚠️ Importante

* `Console.WriteLine` **no aparece** en Azure Log Stream. Solo se ven los logs enviados por `ILogger`.
* `AddAzureWebAppDiagnostics()` es **imprescindible** para conectarte al sistema de logging de App Service.


