# Pasos

## Hacer Deploy por  Linea de Comandos

1. Requisitos Instalamos el CLI az en nuestra pc local

2. Nos logueamos en nuestro cli local

```bash
  az login
```

3. Creamos el Resource Group

4. Creamos el App Service Plan

5. Creamos el App Service

6. Creamos una aplicacion en .NET localmente

7. Compilamos la aplicacion de .NET

```
  dotnet publish -c Release
```

8. Comprimimos el directorio Release

9. Hicimos Deploy de la aplicacion por linea de comando

```
az webapp deploy --resource-group Rg-Az204-Clase-Siete-Trainner --name AwesomeApp4Trainner --src-path app.zip
```

10. Verificamos la Instalacion correcta


