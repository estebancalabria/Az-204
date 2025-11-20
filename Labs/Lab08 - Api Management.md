# Laboratorio 08 : Api Management

* Crear un Resource Group

```bash
  az group create --name rg-az204-lab-08 --location westus 
```

---

* Crear un App Service Plan B1 para Linux

```bash
  az appservice plan create --name Plan-B1-Linux --resource-group rg-az204-lab-08 --location westus --sku B1 --is-linux       
```

---

* Crear un App Service con la imagen kennethreitz/httpbin:latest de Docker

```bash
az webapp create --name web4httpbin --plan Plan-B1-Linux --resource-group rg-az204-lab-08 --deployment-container-image-name kennethreitz/httpbin:latest 
```

---

* Acceder a la la URL publica del app service y verificar que funcione correctamente

---

* Crear el servicio de Api Management

```bash
az apim create --resource-group rg-az204-lab-08 --name apim4httpbin --publisher-name "Esteban Calabria" --publisher-email "mct.esteban.calabria@gmail.com" --location westus --sku-name Consumption 
```

> Nota : Tarda bastante en crear

---

* Ir al servicio de Api Management y crear una Api HTTP

    * Seccion Api
    * Definir nueva Api HTTP
    * Display Name : HTTPBin Api
    * Web service URL : web4httpbin.azurewebsites.net  (URL de nuesta app service)
    * API URL suffix : DEJAR VACIO

---
