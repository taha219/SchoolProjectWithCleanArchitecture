# 🎓 SchoolProject API

An enterprise-grade ASP.NET Core Web API built with Clean Architecture, featuring CQRS, Identity, JWT, Localization, and more.

---

## ✅ Key Features

✅ **Clean Architecture**  
Used to separate responsibilities across different layers:  
- **Data** (Entities, Migrations)  
- **Core** (CQRS, Mapping, Validators)  
- **Infrastructure** (Repositories, DBContext, Identity)  
- **Service** (Business logic and integrations)  
- **API** (Controllers)

✅ **CQRS (Command Query Responsibility Segregation) Design Pattern**  
Separates read (**Queries**) and write (**Commands**) operations for better scalability, separation of concerns, and testability.  
Each entity/module has its own folder structure for `Commands`, `Queries`, `Handlers`, `Validators`, etc.

✅ **MediatR**  
Used for dispatching commands and queries through handler pipelines with minimal coupling.

✅ **AutoMapper with Mapping Profiles**  
Centralized mapping configurations between entities and DTOs using clean Mapping Profiles.  
Improves reusability and clarity of object transformation logic.
**Partial Classes for Separation**  

✅ **Entity Framework Core**  
ORM used to interact with the database in a performant and type-safe manner.

✅ **Generic Repository Pattern**  
Encapsulates data access logic with reusable, strongly typed repository methods.

✅ **Soft Delete using EF Core Interceptors**  
Handles logical deletes via SaveChangesInterceptor.  
Intercepts `EntityState.Deleted` and sets `IsDeleted = true` and `DateDeleted = now`.

✅ **ASP.NET Core Identity with Roles**  
Identity-based user management using `AppUser` entity.  
Supports role-based access control (e.g., Admin, User, Manager, etc.).

✅ **Authorization Policies**  
Custom policies defined for fine-grained control using `[Authorize(Policy = "OnlyAdmins")]`.

✅ **JWT Authentication with Refresh Token**  
Secures API with JWT Bearer Tokens.  
Implements access token + refresh token logic with:
- Token validation parameters
- Token renewal endpoint
- Secure refresh token storage and validation
- Swagger integration for testing

✅ **Swagger + Accept-Language Header**  
Configured to support:
- JWT authentication
- Language switching using `Accept-Language` header (e.g., `ar-EG`, `en-US`)

✅ **Pagination Helper**  
Supports pagination, sorting, and searching with a consistent response shape.

✅ **ApiResponse & Wrapper Classes**  
Standardizes all API responses with consistent structure:
`json
{
  "isSuccess": true,
  "message": "Operation successful",
  "data": { ... }
}`

✅ **Email Sending via SMTP**  
Implemented email sending using MailKit and CQRS (SendEmailCommand, Handler, Service).
