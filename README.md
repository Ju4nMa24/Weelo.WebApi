# Documentación Técnica Web Api Weelo

Para el correcto funcionamiento del web api se deben ejecutar los siguientes pasos:

1. **Migración de Base de datos (EF Code First):** Se debe ejecutar los siguientes comandos en la consola de Nugets de Visual Studio para la creación de la base de datos con su respectivas tablas:

~~~CMD
Add-Migration Initial -p Weelo.Repository.SqlServer -s Weelo.Repository.SqlServer
~~~

~~~CMD
Update-database -p Weelo.Repository.SqlServer -s Weelo.Repository.SqlServer
~~~

_NOTA:_ 

1.1. Sino se desea realizar la migración se adjunta script de base datos para su ejecución en Sql Server _(nombre de archivo: WeeloDB.sql)_.
1.2 Si se genera el siguiente error en la migración: <p style="color:Red">**"The name 'Initial' is used by an existing migration."**</p>

Se debe eliminar el contenido de la carpeta **Migrations** en la biblioteca de clases **Weelo.Repository.SqlServer** para su correcto funcionamiento.


1. **Ejecución de los apis:** Para la ejecución de cada una de las acciones expuestas en los apis, inicialmente se debe consumir el servicio de Authentication (para la generación del JWT) _({{host}}/api/Auth/GenerateJwt)_, dicho api recibe el siguiente request:

~~~JSON
REQUEST DE EJEMPLO
{
  "IdentificationNumber": "123456789",
  "BirthDay": "07-02-2022",
  "ActualDate": "2022-02-07T01:01:48.243Z"
}
~~~

_NOTA:_ Para la primera prueba se debe usar el usuario registrado por defecto en la base de datos, ya que se agrego validación de usuarios existentes para el uso del api.

3. **Consumo de apis:** Al consumir cada uno de los apis ya sea por postman o por medio de un cliente se debe agregar la cabecera de **Authentication** la cual debe ser tipo **Bearer**:

~~~HEADER
Authentication: Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxMjM0NTY3ODkiLCJyb2xlIjoiMjQtMDctMTk5NyIsImFjdG9ydCI6IjcvMi8yMDIyIDE6MDE6NDgxMjM0NTY3ODkiLCJuYmYiOjE2NDQxOTkxMDIsImV4cCI6MTY0NDIwMDAwMiwiaWF0IjoxNjQ0MTk5MTAyLCJpc3MiOiJ7XCJJZGVudGlmaWNhdGlvbk51bWJlclwiOlwiMTIzNDU2Nzg5XCIsXCJCaXJ0aERheVwiOlwiMjQtMDctMTk5N1wiLFwiQWN0dWFsRGF0ZVwiOlwiMjAyMi0wMi0wN1QwMTowMTo0OC4yNDNaXCJ9IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSJ9.LkbEfn-acpn9NbFCi0baoSYreNWI9tENInswxj3bVm2D66JuAThh_9gPzab9C6e9rQT_Yoly9ujqtANSvIGFUA 
~~~

4. **Colección de Postman:** Se adjunta url de la colección de postman la cual contiene la configuración de los endpoints expuestos, adicional se genera archivo tipo JSON (Weelo.Api.postman_collection.json) para importar en postman si dado el caso falla url adjunta:

**Postman:** https://www.postman.com/speeding-crater-141527/workspace/weelo-api/collection/8167258-e217b1fe-e7ea-4ab4-90db-e457b381f252

5. **Administración de repositorio GitHub:** Se trabajo en la rama de _Developer_ pero también la rama master está homologada.

**Url de repositorio:** https://github.com/Ju4nMa24/Weelo.WebApi.git
