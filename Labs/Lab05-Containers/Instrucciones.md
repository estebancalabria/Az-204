# Deploy de Contenedores

## Deploy desde Container Registry Publicos

### Deploy Conteiner en Container Instance desde Docker Hub

* Usamos la imagen nginx

### Deploy Container en App Services desde Docker Hub

* Usamos la imagen nginx

### Deploy Container en App Service desde Microsoft Artifact Registry

* Imagen Usada
    * mcr.microsoft.com/azuredocs/aci-helloworld  (Ojo no es docker Hub)
          * Registry URL : mcr.microsoft.com/
          * Image Name : azuredocs/aci-helloworld
* Ingress
    * Check "Accept Traffic from Anywhere"
    * Check "Insecure Connections"
    * Port 80

---

## Deploy desde Azure Container Registry

### Crear un ACR (Azure Container Registry) 

* Elegir un nombre Unico
* Una vez creada ir a Properties y elegir Admin User
 
### Subir la imagen de nuesta App a nuestro ACR

* Abrir el CLI de Azure
* Crear una App MVC nueva

```cmd
  dotnet new mvc --name DemoApp
```

* Ir al directorio de la APP
* Crear el archivo DockerFile
