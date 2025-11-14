# Laboratorio 6 : Autenticacion

* Ir a Microsoft Entra
* Ir a App Registrations
* Registrar la Aplicacion
  * Anotarse
      * ApplicationID (El nombre del usuario de la applicacion
      * TennantID
* Informar que tipo de Aplicacion vamos a hacer
  * Authentication...Add Platform
  * Elegir una app web con ID Token

> https://auth0.com/blog/id-token-access-token-what-is-the-difference/

* Crear una aplicacion con Autenticacion

```cmd
dotnet new mvc --name AuthDemo --auth SingleOrg --client-id  <CLIENTID> --tenant-id <TENANTID> --domain estebancalabriagmail.onmicrosoft.com
```

* Modificar el HomeController para que se pueda acceder al Index


```c#

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

```


* Correr la aplicacion

```
dotnet run
```


* Verificar
    * Que corra por HTTPS
    * Y el puerto https en el que corre

* Cambiar el puerto en el app registration en la parte de Authorization
  
* Ingresar a la aplicacione n una ventana de incognito

* Darle a sign In
   * La primera vez te va a pedir permios, solo la primera vez
