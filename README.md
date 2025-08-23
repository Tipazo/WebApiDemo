# WebApiDemo

Ejemplo de aplicaciÃ³n **API Rest**.

---

## 🚀 Requisitos
- [Visual Studio](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)  

---

## 📌 Configuración inicial

Para crear la tabla, los **Stored Procedures** y poder hacer rollback, ejecuta los siguientes scripts en este orden:

1. **Creación de tabla y procedimientos almacenados**  
   Ejecutar el archivo:  Database\StoredProcedures\01_Employee_Create_Table_And_Sp.sql

2. **Rollback (eliminar tabla y procedimientos almacenados)**  
	Ejecutar el archivo:  Database\StoredProcedures\02_Employee_Rollback.sql

---

## 🔧 Configuración en entorno local
Para probar la aplicaciónn en **Visual Studio** o **Visual Studio Code**:

1. Localiza el archivo `launchSettings.json` en el proyecto WebApiDemo.
2. Agrega o modifica la variable de entorno:

```json
"DB_HOST": "localhost"
"DB_USER": "your-user"
"DB_PASS": "your-pass"
"DB_PORT": "port-of-sqlserver"
"DB_DATABASE": "your-database"
"DB_TRUSTED_CONNECTION": "true-for-prod"
```

## 🌐 Despliegue en IIS
Si deseas montar la aplicaciÃ³n en un servidor IIS:
Crea una variable de entorno de sistema con la siguiente configuraciÃ³n:
Clave (Key): API_EMPLOYEE
Valor (Value): la URL generada por el IIS donde estÃ© publicado el proyecto WebApiDemo.

```DB_HOST = localhost ```
```DB_USER = your-user ```
```DB_PASS = your-pass ```
```DB_PORT = port-of-sqlserver ```
```DB_DATABASE = your-database ```
```DB_TRUSTED_CONNECTION = true-for-prod ```