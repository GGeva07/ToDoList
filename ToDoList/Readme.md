# ToDoList API

Una API REST desarrollada en ASP.NET Core para la gestión de tareas y usuarios, con autenticación JWT y Entity Framework Core.

## Propósito

Esta API permite:
- Gestionar usuarios (registro, login, CRUD)
- Administrar tareas por usuario
- Autenticación y autorización con JWT
- Operaciones CRUD completas para tareas y usuarios

## Características

- **Autenticación JWT**: Sistema seguro de autenticación
- **Entity Framework Core**: ORM para manejo de base de datos
- **Code First**: Migraciones automáticas de base de datos
- **Swagger UI**: Documentación interactiva de la API
- **Respuestas estandarizadas**: Formato JSON consistente
- **SQL Server**: Base de datos principal

## Tecnologías Utilizadas

- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server / SQL Server Express
- JWT (JSON Web Tokens)
- Swagger/OpenAPI
- Dependency Injection

## Configuración

### Prerrequisitos

- .NET 8.0 SDK
- SQL Server o SQL Server Express
- Visual Studio 2022 o VS Code

### Instalación

1. **Clonar el repositorio**
   ```bash
   git clone <tu-repositorio>
   cd ToDoList.API
   ```

2. **Instalar paquetes NuGet**
   ```bash
   dotnet restore
   ```
   
   O usando Package Manager Console:
   ```powershell
   Install-Package Microsoft.EntityFrameworkCore.SqlServer
   Install-Package Microsoft.EntityFrameworkCore.Tools
   Install-Package Microsoft.EntityFrameworkCore.Design
   Install-Package Swashbuckle.AspNetCore
   Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
   ```

3. **Configurar la cadena de conexión**
   
   Edita `appsettings.json`:
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

4. **Crear la migración inicial**
   ```bash
   dotnet ef migrations add InitialCreate
   ```

5. **Ejecutar la aplicación**
   ```bash
   dotnet run
   ```

   La aplicación iniciará en `https://localhost:5005` o `http://localhost:5262`

## Base de Datos

La aplicación utiliza **Code First** approach y creará automáticamente:

### Tablas
- **Usuarios**: Información de usuarios registrados
- **Tareas**: Tareas asociadas a usuarios

## Estructura del Proyecto

```
ToDoList/
├── Controllers/          # Controladores de la API
│   ├── LoginController.cs
│   ├── UsuarioController.cs
│   └── TareaController.cs
├── Models/              # Modelos de datos
│   ├── Usuario.cs
│   ├── Tarea.cs
│   └── Login.cs
├── Context/             # Contexto de base de datos
│   └── TodoListDBContext.cs
├── Services/            # Lógica de negocio
├── Interfaces/          # Contratos de servicios
└── Program.cs           # Configuración principal
```

## Cómo Probar la API

### 1. Swagger UI

Acceder a: `https://localhost:5005/swagger`

### 2. Endpoints Principales

#### Autenticación
```http
POST /api/Login/Login
Content-Type: application/json

{
  "usuarioNombre": "admin",
  "Correo": "admin@example.com",
  "Contrasenia": "admin123"
}
```

#### Registro
```http
POST /api/Login/Sign-In
Content-Type: application/json

{
  "usuarioNombre": "nuevoUsuario",
  "Correo": "nuevo@email.com",
  "Contrasenia": "password123"
}
```

#### Obtener Tareas
```http
GET /api/Tarea/Get-Tareas
```

#### Crear Tarea
```http
POST /api/Tarea/Post-Tarea
Content-Type: application/json

{
  "Nombre": "Mi nueva tarea",
  "Contenido": "Descripción de la tarea",
  "Estado": "Pendiente",
  "idUsuario": 1
}
```

### 3. Formato de Respuesta

Todas las respuestas siguen este formato estándar:

**Éxito:**
```json
{
  "success": true,
  "message": "Operación exitosa",
  "errorCode": 200,
  "data": { /* datos solicitados */ }
}
```

**Error:**
```json
{
  "success": false,
  "message": "Descripción del error",
  "errorCode": 404
}
```

### 4. Códigos de Estado HTTP

- `200` - OK: Operación exitosa
- `400` - Bad Request: Datos inválidos
- `401` - Unauthorized: Credenciales incorrectas
- `404` - Not Found: Recurso no encontrado
- `500` - Internal Server Error: Error del servidor

## Ejemplos de Uso

### Flujo completo de prueba:

1. **Registrar usuario nuevo**
2. **Hacer login** para obtener token JWT
3. **Crear tareas** asociadas al usuario
4. **Consultar tareas** por usuario
5. **Actualizar** tareas
6. **Eliminar tareas** completadas


## Seguridad

- Autenticación JWT implementada
- Cadenas de conexión seguras
- Validación de datos de entrada
- Manejo de errores centralizado

## Solución de Problemas

### Error de conexión a base de datos
- Verificar que SQL Server esté ejecutándose
- Comprobar la cadena de conexión en `appsettings.json`
- Ejecutar `dotnet ef database update`

### Swagger no aparece
- Verificar que la aplicación esté en modo Development
- Acceder a `/swagger` en lugar de `/swagger/index.html`

### Errores de migración

# Eliminar migraciones
dotnet ef migrations remove

# Crear nueva migración
dotnet ef migrations add InitialCreate

# Aplicar cambios
dotnet ef database update
```
