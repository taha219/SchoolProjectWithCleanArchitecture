# ✅ Project Features

* ✅ **Clean Architecture**  
  Used to separate responsibilities across different layers (Data, Core, Infrastructure, and Services).

* ✅ **CQRS (Command Query Responsibility Segregation) Design Pattern**  
  Separates read (Queries) and write (Commands) operations for better scalability and testability.

* ✅ **MediatR**  
  Helps implement the CQRS pattern and decouples components by handling requests through mediators.

* ✅ **AutoMapper**  
  Simplifies object-to-object mapping, especially between Entities and DTOs.

* ✅ **Entity Framework Core**  
  ORM used to interact with the database efficiently and in a type-safe manner.

* ✅ **Generic Repository Pattern**  
  Reusable data access logic to promote clean and maintainable code.

* ✅ **Soft Delete using Interceptors**  
  Intercepts `EntityState.Deleted` and converts it to soft delete (`IsDeleted = true`, `DateDeleted = now`).  
  Implemented via a custom `SaveChangesInterceptor`.

* ✅ **ASP.NET Core Identity with Roles**  
  Added identity management for users using `AppUser`, including roles (`Admin`, `Student`, etc.).  
  Supports user authentication, role-based authorization, and token generation.

* ✅ **Authorization Policies**  
  Configured authorization policies to restrict access to endpoints based on user roles or claims using `[Authorize(Policy = "...")]`.

* ✅ **JWT Authentication Setup**  
  Secure APIs using JWT bearer tokens.  
  Configured token validation parameters, token creation, and role-based access.  
  Integrated with Swagger for testing secured endpoints.

* ✅ **Swagger + Accept-Language Header**  
  Swagger UI is configured with JWT support and an `Accept-Language` header.  
  This enables testing localization directly through Swagger (e.g., switching between Arabic and English).

* ✅ **Pagination Helper**  
  Implements efficient paging in queries with a consistent structure with ordering and search.

* ✅ **ApiResponse & Wrapper Classes**  
  Provides standardized API responses with success status and messages.

* ✅ **Expression Trees (LINQ Expressions)**  
  Used for efficient projection of selected fields directly from the database.

* ✅ **Dependency Injection**  
  For better modularity, testing, and separation of concerns.

* ✅ **FluentValidation**  
  For validating incoming data in command models.

* ✅ **Localization**  
  Supports both Arabic and English languages with `RequestLocalizationOptions` and shared resource files.


