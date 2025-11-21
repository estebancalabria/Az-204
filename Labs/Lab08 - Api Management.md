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
    * Web service URL : https://web4httpbin.azurewebsites.net/  (URL de nuesta app service)
    * API URL suffix : DEJAR VACIO

---

* Agregar una un endpoint y configurar la parte de Frontend
    * Apretar el boton que dice "+ Operation"
      * Completar
          * Display Name : Echo Headers
          * URL : GET /
       
---

* Configurar la parte de Backend
    * Seleccionar la Operacion que acabamos de crear
    * Selegir el lapicito para editar el backend
    * Completar
          * Service URL : https://web4httpbin.azurewebsites.net/headers  (Check Override)

---

* Probar que funcione
    * Se puede probar desde la solapa "Test" y probar agregarle un header manualmente
    * Si lo probamos desde el navegador va a dar un error porque por defecto las api de servicio Api Management estan protegidas por una subscription key
    * Si quiero deshabilitar la subscripcion por key debo hacerlo en  la solapa "Settings"

---

* Agregar una regla de Inbound para agregar un header antes de que llegue al backend
      * Ir a la vista de design de la operacion "Echo Headers"
      * En donde dice "Inbound Processing" vamos a clickear el boton +Add Policy
      * Elegir la politica "Set Headers"
      * Completar el dialogo para agregar un Header

La regla de Inbound en la vista de codigo deberia quedar asi : 
```xml
<!--
    - Policies are applied in the order they appear.
    - Position <base/> inside a section to inherit policies from the outer scope.
    - Comments within policies are not preserved.
-->
<!-- Add policies as children to the <inbound>, <outbound>, <backend>, and <on-error> elements -->
<policies>
    <!-- Throttle, authorize, validate, cache, or transform the requests -->
    <inbound>
        <base />
        <set-backend-service base-url="https://web4httpbin.azurewebsites.net/headers" />
        <set-header name="Mensaje" exists-action="append">
            <value>No se olviden de seguirme en redes</value>
        </set-header>
    </inbound>
    <!-- Control if and how the requests are forwarded to services  -->
    <backend>
        <base />
    </backend>
    <!-- Customize the responses -->
    <outbound>
        <base />
    </outbound>
    <!-- Handle exceptions and customize error responses  -->
    <on-error>
        <base />
    </on-error>
</policies>
```

---

* Probar que funcione

## Tarea 3 : Manipular respuesta de la API

---

* Agregar una operacion nueva para configurarle Outbound Processing
      * Completar
          * Display Name : Get Legacy Data
          * URL : GET /xml

---

* Configurar la parte de Backend
    * Seleccionar la Operacion que acabamos de crear
    * Selegir el lapicito para editar el backend
    * Completar
          * Service URL : https://web4httpbin.azurewebsites.net/xml?  (Check Override)

> NOTA. Para httpin no es lo mismo /xml que /xml/ (Este ultimo da not found. El apim automaticamente agrega la barra final al backend a pesar de que no se la especifiquemos y en este caso cuando probemos suele dar Not Fount por el motivo anterior. Para que no suceda y el ejemplo funcione a la url del backend le vamos a poner un ? (signo de interrogacion al final). Este es un hack necesario.

---

* Configurar una politica de outbound. En este caso la politica de outbound la debemos programar en xml

```xml
<!--
    - Policies are applied in the order they appear.
    - Position <base/> inside a section to inherit policies from the outer scope.
    - Comments within policies are not preserved.
-->
<!--
    - Policies are applied in the order they appear.
    - Position <base/> inside a section to inherit policies from the outer scope.
    - Comments within policies are not preserved.
-->
<!-- Add policies as children to the <inbound>, <outbound>, <backend>, and <on-error> elements -->
<policies>
    <!-- Throttle, authorize, validate, cache, or transform the requests -->
    <inbound>
        <base />
        <set-backend-service base-url="https://web4httpbin.azurewebsites.net/xml?" />
    </inbound>
    <!-- Control if and how the requests are forwarded to services  -->
    <backend>
        <base />
    </backend>
    <!-- Customize the responses -->
    <outbound>
        <base />
        <xml-to-json kind="javascript-friendly" apply="always" consider-accept-header="false" />
    </outbound>
    <!-- Handle exceptions and customize error responses  -->
    <on-error>
        <base />
    </on-error>
```

> NOTA . consider-accept-header debe ir en false

---

* Probar que funcione
