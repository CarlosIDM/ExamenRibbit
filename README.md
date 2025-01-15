# API de Productos - Proyecto en ASP.NET Core 8

Este proyecto es una API REST para gestionar productos en una base de datos SQLite. 
La API permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre productos.

## Requisitos

- .NET 8 SDK o superior
- SQLite (la base de datos se configura automáticamente)
- Visual Studio o cualquier editor de tu preferencia

## Configuración de SQLite

La base de datos SQLite se configura en el archivo `appsettings.json`. El proyecto utiliza una base de datos SQLite local almacenada en un archivo llamado `productos.db` en la raíz del proyecto.

A continuación, se muestra la configuración de conexión en el archivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=productos.db"
  }
}
