# Laboratorio 10b - Alternativa utilizndo el Cli y Procesando Mensajes Apenas llegan

## Setup Laboratorio

* Crear el Resource Group

```bash
  az group create --name rg-az204-lab-10 --location westus
```

* Crear el Service Bus

```bash
az servicebus namespace create --resource-group rg-az204-lab-10 --name bus4trainner --location westus --sku Standard          
```

* Crear una queue

```bash
az servicebus queue create --resource-group rg-az204-lab-10 --namespace-name bus4trainner --name queue4trainner                
```

*  Crear una policy de listen, send y Manage para la queue

```bash
az servicebus queue authorization-rule create --resource-group rg-az204-lab-10 --namespace-name bus4trainner --queue-name queue4trainner --name root --rights Listen Send Manage
```


