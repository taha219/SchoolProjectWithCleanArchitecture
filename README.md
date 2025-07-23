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
- Implemented email sending using MailKit and CQRS (SendEmailCommand, Handler, Service).
- Confirm  User Email when Regitering User
  
✅ **Password Reset with OTP (One-Time Password)**
- Secure password reset mechanism using OTP verification via Email or SMS using (Vonage sms API)
- OTP is generated and stored securely with expiration and usage tracking.
- OTP is sent to the user’s email or mobile number via configured providers.
- Users confirm their identity by submitting the OTP before resetting the password.

✅ **Hangfire**
 - Add Enqueue Job send Welcoming Messege to user Email After User registeration
 - Add Schedule Job Send Confirm Mail After User registeration after 2 minutes
 - Add Recurring Job delete expired otp every week
 - Add Recurring Job send confirm link to Email to all users who did not confirm Email every 3 days

✅ **SignalR Notifications**
- Real-time notifications using SignalR to inform students when a grade is added or updated.
- Used custom IUserIdProvider to extract UserId from JWT claims
- Targeted specific users via Clients.User(userId).SendAsync(...)
- Frontend connects to /notificationHub using JWT token
- Triggered automatically after adding or updating student grade
