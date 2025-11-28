# Laboratorio 10c - Service Bus Topics

## Setup Laboratorio

* Crear el Resource Group

```bash
  az group create --name rg-az204-lab-10 --location westus
```

* Crear el Service Bus

```bash
az servicebus namespace create --resource-group rg-az204-lab-10 --name bus4trainner --location westus --sku Standard          
```

* Crear un topico

```bash
az servicebus topic create --resource-group rg-az204-lab-10 --namespace-name bus4trainner --name topic-2
```

* Crear una subscripcion

```bash
az servicebus topic subscription create --resource-group rg-az204-lab-10 --namespace-name bus4trainner --topic-name topic-1 --name app-2  
```

* Probar el funcionamiento de los topicos y las subscripciones desde el portal
