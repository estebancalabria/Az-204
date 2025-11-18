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

* 
