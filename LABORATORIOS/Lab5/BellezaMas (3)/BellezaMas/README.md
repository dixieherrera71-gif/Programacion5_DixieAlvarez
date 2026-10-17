# Belleza & Más

## Sistema de Gestión de Inventario

Belleza & Más es una aplicación web desarrollada en ASP.NET Core para la gestión de inventario de una tienda de productos de belleza.

El sistema permite administrar productos, categorías, proveedores, usuarios y movimientos de entrada y salida de inventario.

---

## Tecnologías utilizadas

- ASP.NET Core
- Razor Pages
- Web API
- Entity Framework Core
- SQL Server
- LINQ
- JWT
- PBKDF2 para hash de contraseñas
- Swagger
- QuestPDF
- Docker
- Bootstrap
- CSS

---

## Arquitectura del proyecto

La solución está dividida en dos proyectos independientes:

### BellezaMas.Api

Contiene:

- API REST
- Entity Framework Core
- Acceso a SQL Server
- Autenticación JWT
- Autorización por roles
- CRUD
- Movimientos de inventario
- Generación de reportes PDF
- Swagger

### BellezaMas.Web

Contiene:

- Interfaz desarrollada con Razor Pages
- Login
- Dashboard
- Gestión de productos
- Gestión de categorías
- Consulta de proveedores
- Movimientos de inventario
- Gestión de usuarios
- Reportes
- Consumo de BellezaMas.Api mediante HttpClient

La aplicación Web no posee referencia directa al proyecto API. La comunicación se realiza mediante HTTP.

---

## Funcionalidades principales

### Autenticación

El sistema permite iniciar sesión utilizando correo electrónico y contraseña.

Las contraseñas no se almacenan en texto plano. Se utiliza PBKDF2 para generar hashes seguros.

La API genera un token JWT al realizar un inicio de sesión válido.

### Roles

El sistema posee dos roles:

- Administrador
- Usuario

El Administrador tiene acceso a las funciones administrativas del sistema.

El Usuario posee acceso únicamente a las funcionalidades autorizadas.

---

## Productos

El módulo de productos permite:

- Consultar productos
- Buscar productos
- Ordenar resultados
- Paginar resultados
- Crear productos
- Editar productos
- Relacionar productos con categorías
- Relacionar productos con proveedores
- Controlar stock
- Definir stock mínimo
- Identificar productos con stock bajo

---

## Categorías

Permite:

- Consultar categorías
- Crear categorías
- Editar categorías
- Administrar su estado
- Relacionarlas con productos

---

## Proveedores

El sistema permite consultar los proveedores registrados y utilizar sus datos al registrar productos.

---

## Movimientos de inventario

El módulo permite registrar:

- Entradas de productos
- Salidas de productos

Cada movimiento modifica automáticamente el stock del producto.

El sistema evita realizar una salida cuando la cantidad solicitada supera el stock disponible mediante una excepción personalizada:

`StockInsuficienteException`

---

## Manejo de errores

El proyecto utiliza:

- Validaciones con DataAnnotations
- try-catch
- ILogger
- Excepciones personalizadas
- Respuestas HTTP apropiadas

Entre los códigos utilizados se encuentran:

- 200 OK
- 201 Created
- 400 Bad Request
- 401 Unauthorized
- 403 Forbidden
- 404 Not Found
- 409 Conflict
- 500 Internal Server Error

---

## Reportes

El sistema genera un reporte PDF de movimientos de inventario mediante QuestPDF.

El reporte permite seleccionar:

- Fecha inicial
- Fecha final

El PDF incluye los movimientos realizados durante el periodo seleccionado.

---

## Dashboard

El panel principal muestra información resumida del inventario:

- Cantidad de productos
- Cantidad de categorías
- Cantidad de proveedores
- Productos con stock bajo
- Productos recientes

---

## Base de datos

El sistema utiliza SQL Server con Entity Framework Core.

Las principales tablas son:

- Productos
- Categorias
- Proveedores
- MovimientosInventario
- Usuarios
- Roles
- __EFMigrationsHistory

Las tablas son generadas mediante migraciones de Entity Framework Core.

---

## Configuración local

La cadena de conexión utilizada durante el desarrollo debe configurarse en:

`appsettings.json`

Ejemplo:

```json
{
  "ConnectionStrings": {
    "InventarioDb": "Server=(localdb)\\MSSQLLocalDB;Database=BellezaMasDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}