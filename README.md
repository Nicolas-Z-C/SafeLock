# Safelock

Clon centralizado de tipo Steam, desarrollado como proyecto práctico de **AppSec** dentro de una transición de carrera hacia seguridad de aplicaciones y red team.

Un único administrador puede crear y publicar juegos para la venta. Los usuarios pueden comprarlos usando una wallet ficticia y dejar reseñas sobre los juegos que poseen.

Este proyecto no busca solo ser funcional: busca ser un ejercicio deliberado de diseño seguro por capas, control de acceso, y buenas prácticas de desarrollo — documentando en el camino las decisiones de seguridad tomadas (y los errores corregidos) como parte del aprendizaje.

## Stack

- **.NET 10**
- **ASP.NET Core Web API** — backend
- **Blazor WebAssembly** — frontend
- **Entity Framework Core** (comandos/escritura) + **Dapper** (queries/lectura)
- **SQL Server**
- **MediatR** — patrón CQRS
- **FluentValidation** — validación de entrada vía pipeline
- **Docker / Docker Compose**

## Arquitectura

Clean Architecture con principios de DDD, dependencias apuntando siempre hacia el dominio:

```
Domain            → sin dependencias
Application       → Domain
Infrastructure    → Application
Shared            → sin dependencias (contratos/DTOs compartidos)
WebApi            → Application + Infrastructure + Shared
WebAssembly       → Shared (únicamente)
```

`WebAssembly` nunca referencia `Application` ni `Infrastructure`: al compilarse a ensamblados descargables por el navegador, cualquier lógica de negocio o acceso a datos ahí quedaría expuesta y sería decompilable. Toda regla de negocio vive del lado del servidor.

## Estructura del repositorio

```
Safelock/
├── Safelock.sln
├── Directory.Packages.props
├── docker-compose.yml
├── .env
└── src/
    ├── Safelock.Domain/
    ├── Safelock.Application/
    ├── Safelock.Infrastructure/
    ├── Safelock.Shared/
    └── Safelock.Web/
        ├── Safelock.WebApi/
        └── Safelock.WebAssembly/
```

## Cómo levantar el proyecto

Requiere Docker y Docker Compose instalados.

```bash
git clone <url-del-repo>
cd Safelock
cp .env.example .env   # completar valores antes de continuar
docker compose up --build
```

- API disponible en `http://localhost:8080`
- Cliente web disponible en `http://localhost:5000`

## Roadmap

### Fase 0 — Diseño
- [x] Definición de arquitectura (Clean Architecture + DDD + CQRS)
- [x] Modelo de dominio (entidades, atributos, relaciones)
- [x] Diagramas de flujo y casos de uso
- [x] Definición de stack y contenedores

### Fase 1 — Scaffolding
- [x] Estructura de solución y proyectos (`dotnet new`)
- [x] Configuración de Central Package Management
- [x] Dockerfiles (WebApi, WebAssembly) + docker-compose
- [ ] Configuración inicial de EF Core (DbContext, primera migración)
- [ ] Configuración de Dapper para queries

### Fase 2 — Identity y control de acceso
- [ ] Registro y login (Admin único / Usuario)
- [ ] Hashing de contraseñas (BCrypt/Argon2)
- [ ] Autenticación JWT
- [ ] Autorización basada en roles (RBAC)
- [ ] Rate limiting en endpoints de login

### Fase 3 — Catálogo de juegos
- [ ] CRUD de juegos (solo Admin)
- [ ] Carga de archivos/imágenes con validación
- [ ] Endpoints de consulta pública (queries vía Dapper)

### Fase 4 — Wallet y compras
- [ ] Modelo de Wallet con métodos de negocio (`Debitar`, `Acreditar`)
- [ ] Flujo de compra y control de integridad transaccional
- [ ] Manejo de concurrencia

### Fase 5 — Reseñas
- [ ] CRUD de reseñas (solo sobre juegos comprados)
- [ ] Sanitización de input (prevención de XSS almacenado)
- [ ] Validación con FluentValidation vía pipeline de MediatR

### Fase 6 — Frontend (Blazor WebAssembly)
- [ ] Configuración de CORS entre WebApi y WebAssembly
- [ ] Autenticación desde el cliente (manejo de JWT)
- [ ] Vistas de catálogo, wallet, biblioteca y reseñas

### Fase 7 — Endurecimiento (AppSec)
- [ ] Revisión de IDOR en endpoints sensibles (wallet, biblioteca, reseñas)
- [ ] Logging estructurado sin datos sensibles
- [ ] Manejo de secretos fuera del código fuente
- [ ] Documento `SECURITY_LOG.md` con decisiones de seguridad tomadas y corregidas

## Licencia

Este proyecto está bajo la licencia MIT. Ver [LICENSE](LICENSE) para más detalles.