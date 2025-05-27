# Azure CLI Command Reference

## Introducción

Este documento sirve como una guía de referencia rápida para los comandos más comunes de Azure CLI (Command-Line Interface). Azure CLI es una herramienta de línea de comandos multiplataforma que permite administrar recursos de Azure directamente desde la terminal. Este compendio incluye comandos para gestionar Resource Groups, Storage Accounts, Key Vault, App Service, Function Apps, Virtual Machines, Networking y más.

## Índice

- [Iniciar sesión y configuración de cuenta](#iniciar-sesión-y-configuración-de-cuenta)
- [Resource Groups](#resource-groups)
- [ARM Templates](#arm-templates)
- [Storage](#storage)
- [Key Vault](#key-vault)
- [App Service](#app-service)
- [Function Apps](#function-apps)
- [Redes](#redes)
- [Máquinas Virtuales](#máquinas-virtuales)
- [Contenedores](#contenedores)

## Iniciar sesión y configuración de cuenta

### Iniciar sesión en Azure
```bash
az login
```

### Establecer la suscripción actual
```bash
az account set --subscription <name or id>
```

## Resource Groups

### Crear Resource Group
```bash
az group create --name Rg-Az204-Clase-Tres --location eastus
```

### Eliminar Resource Group
```bash
az group delete --resource-group Rg-Temp
```

## ARM Templates

### Desplegar un ARM Template
```bash
az deployment group create --resource-group Rg-Az104-Lab3b --template-file ./new-template.json
```

## Storage

### Storage Account

#### Crear Storage Account
```bash
az storage account create --name sa4trainner4fapp --resource-group Rg-Az204-Clase8-Trainner --location eastus --sku Standard_LRS
```

```bash
az storage account create --name storemedia4trainner --resource-group Rg-Az204-Clase-Cuatro --location eastus --sku Standard_LRS --kind StorageV2
```

### Blob Storage

#### Crear un Blob Container
```bash
az storage container create --name demo-container --connection-string DefaultEndpointsProtocol=https;AccountName=storemedia4trainner;AccountKey=dq2C87Aupd9wN83aMY8qmSOq4r74b5CQC71dMILsXEMoDViGS8O7pkz/LClOEKAvhdv63w+cGN+2+AStoFISIw==;EndpointSuffix=core.windows.net
```

```bash
az storage container create --account-name sa4cosmo4trainner --name images --public-access blob
```

#### Subir un archivo a un blob container
```bash
az storage blob directory upload --container demo-container --source db.json --account-name storemedia4trainner --destination-path .
```

```bash
az storage blob directory upload --container demo-container --source db.json --connection-string DefaultEndpointsProtocol=https;AccountName=storemedia4trainner;AccountKey=dq2C87Aupd9wN83aMY8qmSOq4r74b5CQC71dMILsXEMoDViGS8O7pkz/LClOEKAvhdv63w+cGN+2+AStoFISIw==;EndpointSuffix=core.windows.net --destination-path .
```

#### Generar un SAS (Shared Access Signature) URL para un blob
```bash
az storage blob generate-sas --account-name cs4trainner --name template.json --container-name private-contaner --permission r --auth-mode login --as-user --full-uri --expiry "2023-01-08"
```

## Key Vault

### Crear una Key Vault
```bash
az keyvault create --name key4trainner --resource-grouo Rg-Az204-Clase8-Trainner --location eastus --sku standard --enable-purge-protection $true --retention-days 90
```

## App Service

### App Service Plan

#### Listar Planes
```bash
az appservice plan list
```

#### Crear Un Plan
```bash
az appservice plan create --name Plan-Free --resource-group Rg-Az204-Clase8-Trainner --location eastus --sku F1
```

```bash
az appservice plan create --name Plan-Free-Linux --resource-group Rg-Az204-Clase8-Trainner --location eastus --sku F1 --is-linux
```

#### Borrar un Plan
```bash
az appservice plan delete --name Plan-Free --resource-group Rg-Az204-Clase8-Trainner
```

### Web Apps

#### Listar WebApps
```bash
az webapp list
```

#### Crear una WebApp
```bash
az webapp create --name app-az204-frontend-trainner --plan ASP-RgAz204ClaseDos-a2d9 --resource-group Rg-Az204-Clase-Dos --runtime "dotnet:6"
```

#### Listar los Runtimes
```bash
az webapp list-runtimes --os-type linux
```

#### Hacer Deploy de una Web App Desde un Zip
```bash
dotnet publish -c Release
# Comprimir contenido de la carpeta publish en un zip
az webapp deploy --resource-group Rg-Az204-Clase-Siete-Trainner --name AwesomeApp4Trainner --src-path c:\temp\publish.zip
```

#### Hacer Deploy de una Web App desde Git
```bash
az webapp deployment source config --name appClaseDiez --resource-group Rg-Az204-Clase-Diez --repo-url --branch main
```

#### Configurar una App (Agregar una entrada en el appsettings)
```bash
az webapp config appsettings set -g Rg-Az204-Clase-Dos -n app-az204-frontend-trainner --settings ApiUrl=https://app-az204-api-trainner.azurewebsites.net/
```

## Function Apps

### Crear una function app (Con Application Insights Automático)
```bash
az functionapp create --name func4deploy --resource-group Rg-Az204-Clase-Cuatro --storage-account storemedia4trainner --runtime dotnet --consumption-plan-location eastus
```

### Crear Function App Eligiendo el Plan
```bash
az functionapp create --name Fun-Az204-Trainner --resource-group Rg-Az204-Clase8-Trainner --runtime dotnet --runtime-version 6 --os-type Linux --plan Plan-Free-Linux-Trainner --storage-account sa4trainner4fapp --functions-version 4
```

### Deploy de una Function App
```bash
az functionapp deployment source config-zip --resource-group rg-az204-lab-dos --name func4trainner --src func.zip
```

## Redes

### Discos

#### Crear un disco
```bash
az disk create --name Disk-Vm --size-gb 8 --resource-group Rg-Az104-Clase-Ocho --location eastus
```

### IP Pública

#### Crear Una IP Pública
```bash
az network public-ip create --name IP-PublicVm --resource-group Rg-Az500-Clase-Cuatro --location Eastus --sku Standard
```

### Virtual Network

#### Crear Una Virtual Network
```bash
az network vnet create --name Vnet-Az500-Clase-Cuatro --resource-group Rg-Az500-Clase-Cuatro --location eastus --adress-prefixes 10.0.0.0/16 --subnet-name SubNet-Jump --subnet-prefixes 10.0.3.0/24
```

#### Crear Una Subnet
```bash
az network vnet subnet create --name SubNet-Work --resource-group Rg-Az500-Clase-Cuatro --vnet-name VNet-Az500-Clase-Cuatro --address-prefixes 10.0.2.0/24
```

### Network Security Group

#### Crear un Network Security Group
```bash
az network nsg create --name Nsg-Az500-Clase-Cuatro --location eastus --resource-group Rg-Az500-Clase-Cuatro
```

#### Agregar una Regla para habilitar RDP
```bash
az network nsg rule create --resource-group Rg-Az500-Clase-Cuatro --nsg-name Nsg-Az500-Clase-Cuatro --priority 100 --name "Allow_RDP" --destination-port 3389 --access Allow --protocol tcp
```

## Máquinas Virtuales

### Ver Lista de imágenes disponibles
```bash
az vm image list
```

### Crear una Máquina Virtual Debian
```bash
az vm create --resource-group Rg-Az204-Clase-Seis-Trainner --name Vm-Az204-Clase-Seis-Trainner --image Debian --admin-username azureuser --admin-password Pa55w.rd1234
```

### Crear Una Maquina Virtual con Ip, NSG y VNet existentes
```bash
az vm create --name Vm-Jump --resource-group Rg-Az500-Clase-Cuatro --location Eastus --image Win2019Datacenter --public-ip-address IP-PublicVm --vnet-name Vnet-Az500-Clase-Cuatro --subnet SubNet-Jump --nsg Nsg-Az500-Clase-Cuatro --admin-username AzureUser --admin-password Pa55w.rd1234 --size Standard_DS1_v2
```

### Crear una Maquina Virtual Sin Ip Publica
```bash
az vm create --name Vm-Work --resource-group Rg-Az500-Clase-Cuatro --location Eastus --image Win2019Datacenter --vnet-name Vnet-Az500-Clase-Cuatro --subnet SubNet-Work --nsg Nsg-Az500-Clase-Cuatro --admin-username AzureUser --admin-password Pa55w.rd1234 --size Standard_DS1_v2 --public-ip-address ""
```

### Reiniciar una Maquina Virtual
```bash
az vm restart --name Vm-Tricampeon --resource-group Rg-Az104-Clase-Ocho
```

## Contenedores

### Azure Container Registry

#### Chequear disponibilidad de un nombre de un Azure Container Registry
```bash
az acr check-name --name acr4trainner
```

#### Crear un Azure Container Registry
```bash
az acr create --resource-group Rg-Az204-Clase-Seis-Trainner --name acr4trainner --sku Basic
```

#### Subir una imagen a un Container Registry
```bash
az acr build --registry acr4trainner --image ipcheck:latests .
```

```bash
az acr build --resource-group Rg-Az500-Clase-Cinco --registry acr4trainner --file Dockerfile . --image my-super-nginx
```

### Crear un container Instance desde una imagen en un Container Registry
```bash
az container create --resource-group Rg-Az204-Clase-Seis-Trainner --name docker-4-trainner-ext-2 --image acr4trainner.azurecr.io/ipcheck:latest2 --assign-identity
```