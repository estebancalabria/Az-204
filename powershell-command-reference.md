
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
