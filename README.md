ToDoList API

Una API REST desarrollada en **ASP.NET Core 8.0** para la gestión de tareas y usuarios, con autenticación **JWT** y **Entity Framework Core**.


Esta API permite:

* Gestionar usuarios (registro, login, CRUD).
* Administrar tareas por usuario.
* Autenticación y autorización con JWT.
* Operaciones CRUD completas para tareas y usuarios.



Características

* **Autenticación JWT**: Sistema seguro de autenticación.
* **Entity Framework Core**: ORM para manejo de base de datos.
* **Code First**: Migraciones automáticas de base de datos.
* **Swagger UI**: Documentación interactiva de la API.
* **Respuestas estandarizadas**: Formato JSON consistente.
* **SQL Server**: Base de datos principal.

---

Tecnologías Utilizadas

* **ASP.NET Core 8.0**
* **Entity Framework Core**
* **SQL Server / SQL Server Express**
* **JWT (JSON Web Tokens)**
* **Swagger/OpenAPI**
* **Dependency Injection**

Configuración

Prerrequisitos

* .NET 8.0 SDK
* SQL Server o SQL Server Express
* Visual Studio 2022 o VS Code

Instalación

1. Clonar el repositorio:

   ```bash
   git clone <tu-repositorio>
   cd ToDoList.API
   ```

2. Instalar paquetes NuGet:

   ```bash
   dotnet restore
   ```

**Package Manager Console**:

   ```bash
   Install-Package Microsoft.EntityFrameworkCore.SqlServer
   Install-Package Microsoft.EntityFrameworkCore.Tools
   Install-Package Microsoft.EntityFrameworkCore.Design
   Install-Package Swashbuckle.AspNetCore
   Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
   ```

3. Configurar la cadena de conexión en `appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
       "AppConnection": "Server=localhost\\SQLEXPRESS;Database=TodolistBD;Trusted_Connection=True;TrustServerCertificate=True;"
     },
     "Jwt": {
       "Key": "Kj8mP3nQ7rT2vW5xZ9cF6bN1aS4dG8hL0oI7uY3eR6tQ9wE2sA5zX8cV1bM4nF7gH3jK",
       "Issuer": "TodoListAPI",
       "Audience": "TodoListClients"
     }
   }
   ```

La aplicación iniciará en:
[https://localhost:5005](https://localhost:5005)
[http://localhost:5262](http://localhost:5262)



## Base de Datos

La aplicación utiliza **Code First approach** y generará automáticamente las tablas:

* **Usuarios** → Información de usuarios registrados.
* **Tareas** → Tareas asociadas a usuarios.

## Estructura del Proyecto

```plaintext
📂 ToDoList/
├── 📂 src/                          # Código fuente principal
│   ├── 📂 core/                     # Núcleo de la aplicación
│   │   ├── 📂 ToDoListAPI.Core.Application   # Lógica de aplicación
│   │   │   ├── 📂 DTOs
│   │   │   ├── 📂 Fabricas
│   │   │   ├── 📂 Interfaces
│   │   │   ├── 📂 Services
|   |   |   |   └── 📂 Cache
|   |   |   |
│   │   │   └── ServiceRegistration.cs
│   │   ├── 📂 ToDoListAPI.Core.Domain        # Entidades de dominio
│   │   │   ├── 📂 Common
│   │   │   ├── 📂 Entities
│   │   │   ├── 📂 Enum
│   │   │   └── 📂 Interfaces
│
│   ├── 📂 Infrastructure/           # Persistencia e infraestructura
│   │   ├── 📂 ToDoListAPI.Infrastructure.Persistence
│   │   │   ├── 📂 Configurations
│   │   │   ├── 📂 Context
│   │   │   ├── 📂 Fabricas
│   │   │   ├── 📂 Migrations
│   │   │   ├── 📂 Repository
│   │   │   └── ServiceRegistration.cs
│
│   ├── 📂 Presentation/             # Capa de presentación (Web API)
│   │   ├── 📂 Web
│   │   │   ├── 📂 Controllers       # Controladores de la API
│   │   │   ├── appsettings.json     # Configuración de la app
│   │   │   ├── Program.cs           # Punto de entrada
│   │   │   └── ...
│
└── 📄 README.md                     # Documentación principal del proyecto

```
Formato de Respuesta

✅ Éxito:

```json
{
  "success": true,
  "message": "Operación exitosa",
  "errorCode": 200,
  "data": { /* datos solicitados */ }
}
```

❌ Error:

```json
{
  "success": false,
  "message": "Descripción del error",
  "errorCode": 404
}
```

---

Códigos de Estado HTTP

* **200 - OK** → Operación exitosa
* **400 - Bad Request** → Datos inválidos
* **401 - Unauthorized** → Credenciales incorrectas
* **404 - Not Found** → Recurso no encontrado
* **500 - Internal Server Error** → Error del servidor

