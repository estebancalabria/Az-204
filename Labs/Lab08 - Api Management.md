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



