# WebApiDemo

Ejemplo de aplicaci贸n **API Rest**.

---

## 馃殌 Requisitos
- [Visual Studio](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)  

---

## ?? Configuración inicial
Pasos para crear la tabla y crear los stored procedure y hacer rollback ejecuta los siguientes scripts

1. Ejecutar 
2. Para hacer rollback

---

## 馃敡 Configuraci贸n en entorno local
Para probar la aplicaci贸n en **Visual Studio** o **Visual Studio Code**:

1. Localiza el archivo `launchSettings.json` en el proyecto WebApiDemo.
2. Agrega o modifica la variable de entorno:

```json
"DB_HOST": "localhost"
```
```json
"DB_USER": "your-user"
```
```json
"DB_PASS": "your-pass"
```
```json
"DB_PORT": "port-of-sqlserver"
```
```json
"DB_DATABASE": "your-database"
```

## 馃寪 Despliegue en IIS
Si deseas montar la aplicaci贸n en un servidor IIS:
Crea una variable de entorno de sistema con la siguiente configuraci贸n:
Clave (Key): API_EMPLOYEE
Valor (Value): la URL generada por el IIS donde est茅 publicado el proyecto WebApiDemo.

```DB_HOST = localhost ```
```DB_USER = your-user ```
```DB_PASS = your-pass ```
```DB_PORT = port-of-sqlserver ```
```DB_DATABASE = your-database ```
