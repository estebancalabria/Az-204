
# Resource Groups

- Crear un Resource Group

```powershell
   New-AzResourceGroup -Name "rg-az204-clase-06" -Location 'westUS'
```

---

# App Service Plans

- Crear un App Service Plan

```powershell
New-AzAppServicePlan -Name "Plan-S1"  -Location 'westUS' -ResourceGroupName "rg-az204-clase-06" -Tier "S1"
```

- Darle un System Asinged Managed Identity a la function app
```
Set-AzFunctionApp -Name func4secureapp -ResourceGroupName rg-az204-clase-07 -IdentityType SystemAssigned
```

---

# App Service

- Crear un App Service

```powershell
New-AzWebApp -ResourceGroupName "rg-az204-clase-06" -Name web4Trainner -Location 'westUS' -AppServicePlan "Plan-S1-"      
```

---

# Storage Acoount

- Crear un Storage Account                           
```powershell
New-AzStorageAccount -ResourceGroupName "rg-az204-clase-07" -Name cs4secureapp -Location 'westUS' -SkuName Standard_LRS
```

---

# Key Vault

- Crear un Key Vaulr
```powershell
New-AzKeyVault -Name kv4secureapp -ResourceGroupName "rg-az204-clase-07" -Location 'westUS' -DisableRbacAuthorization -EnablePurgeProtection:$false -SoftDeleteRetentionInDays 7
```

- Crear un Acces Policy para un usuario

``` powershell
Set-AzKeyVaultAccessPolicy -VaultName kv4secureapp -UserPrincipalName "esteban.calabria_gmail.com#EXT#@estebancalabriagmail.onmicrosoft.com" -PermissionsToKeys get,wrapKey,unwrapKey,sign,verify,list -PermissionsToSecrets list,set,get,delete -PassThru 
```

- Crear Secreto en el Key Vault

```powershell
  $SecretName = "NombreDeTuSecreto"
$SecretValue = "ValorDelSecreto"

Set-AzKeyVaultSecret -VaultName $KeyVaultName -Name $SecretName -SecretValue (ConvertTo-SecureString -String $SecretValue -AsPlainText -Force)
```

# Function App

- Crear una Function App

```powershell
New-AzFunctionApp -Name func4secureapp -ResourceGroupName "rg-az204-clase-07" -Location 'westUS' -StorageAccountName cs4secureapp -Runtime dotnet -RuntimeVersion 8 -DisableApplicationInsights -FunctionsVersion 4 -OSType Windows
```
> Desde el Portal o el comando Az se puede utilizar dotnet 9. Pero desde el comando de powershell no se puede crear una futionapp con dotnet 9

