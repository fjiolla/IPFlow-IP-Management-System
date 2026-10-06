# IPFlow - Intellectual Property Management System

A full-stack intellectual property management platform built with Angular, TypeScript, ASP.NET Core Web API, C#, Entity Framework Core, and SQL Server.

## Project Overview

IPFlow is a comprehensive IP management system designed for law firms and corporate legal departments to manage patents, trademarks, litigation cases, and client information. The system implements role-based access control with Admin, Lawyer, and Client roles.

## Technology Stack

### Backend
- **ASP.NET Core 8.0** - Web API Framework
- **C#** - Primary programming language
- **Entity Framework Core 8.0** - ORM for database operations
- **SQL Server** - Relational database
- **JWT** - Authentication and authorization
- **BCrypt** - Password hashing
- **Swagger** - API documentation

### Frontend
- **Angular 18+** - Frontend framework
- **TypeScript** - Type-safe JavaScript
- **Angular Material** - UI component library
- **RxJS** - Reactive programming
- **Angular Router** - Client-side routing
- **HttpClient** - HTTP communication

## Features

### Core Modules

1. **Authentication & Authorization**
   - JWT-based authentication
   - Role-based access control (Admin, Lawyer, Client)
   - Secure password hashing with BCrypt
   - Token-based session management

2. **Patent Management**
   - Create and manage patent applications
   - Track filing dates and expiry dates
   - Status tracking (Pending, Approved, Rejected, Expired)
   - Assign lawyers to patents
   - Client association

3. **Trademark Management**
   - Trademark registration tracking
   - Class number management
   - Renewal date monitoring
   - Status management
   - Lawyer assignment

4. **Litigation Case Management**
   - Case creation and tracking
   - Hearing date management
   - Case status updates (Open, Closed, Pending)
   - Case type categorization
   - Document management

5. **Client Management**
   - Client information storage
   - Contact details management
   - Association with patents, trademarks, and cases

6. **Dashboard & Analytics**
   - Total patents overview
   - Active trademarks count
   - Pending cases summary
   - Renewals due within 30 days
   - Upcoming deadlines (patents, trademarks, hearings)
   - Recent activity feed

## Backend Architecture

### Layered Architecture

```
Controllers → Services → Repositories → Data Access (Entity Framework) → Database
```

### Project Structure

```
IPFlowAPI/
├── Controllers/          # API endpoints
│   ├── AuthController.cs
│   ├── PatentsController.cs
│   ├── TrademarksController.cs
│   ├── CasesController.cs
│   ├── ClientsController.cs
│   └── DashboardController.cs
├── Services/            # Business logic layer
│   ├── AuthService.cs
│   ├── PatentService.cs
│   ├── TrademarkService.cs
│   ├── CaseService.cs
│   ├── ClientService.cs
│   └── DashboardService.cs
├── Repositories/        # Data access layer
│   ├── UserRepository.cs
│   ├── PatentRepository.cs
│   ├── TrademarkRepository.cs
│   ├── CaseRepository.cs
│   └── ClientRepository.cs
├── Models/              # Entity models
│   ├── User.cs
│   ├── Role.cs
│   ├── Client.cs
│   ├── Patent.cs
│   ├── Trademark.cs
│   ├── Case.cs
│   └── Document.cs
├── DTOs/                # Data transfer objects
│   ├── AuthDTOs.cs
│   ├── PatentDTOs.cs
│   ├── TrademarkDTOs.cs
│   ├── CaseDTOs.cs
│   ├── ClientDTOs.cs
│   └── DashboardDTO.cs
├── Data/                # Database context
│   └── ApplicationDbContext.cs
└── Program.cs           # Application entry point
```

### Database Schema

#### Tables and Relationships

**Users**
- UserId (PK)
- Name
- Email (Unique)
- PasswordHash
- RoleId (FK → Roles)
- CreatedAt

**Roles**
- RoleId (PK)
- RoleName
- Seeded: Admin, Lawyer, Client

**Clients**
- ClientId (PK)
- Name
- Email (Unique)
- Phone
- Address
- CreatedAt

**Patents**
- PatentId (PK)
- ApplicationNumber (Unique)
- Title
- Description
- FilingDate
- ExpiryDate
- Status
- ClientId (FK → Clients)
- LawyerId (FK → Users)
- CreatedAt

**Trademarks**
- TrademarkId (PK)
- ApplicationNumber (Unique)
- Name
- Description
- RegistrationDate
- RenewalDate
- Status
- ClassNumber
- ClientId (FK → Clients)
- LawyerId (FK → Users)
- CreatedAt

**Cases**
- CaseId (PK)
- CaseNumber (Unique)
- Title
- Description
- Status
- OpenDate
- CloseDate
- CaseType
- NextHearingDate
- ClientId (FK → Clients)
- LawyerId (FK → Users)
- CreatedAt

**Documents**
- DocumentId (PK)
- FileName
- DocumentType
- FilePath
- FileSize
- PatentId (FK → Patents, nullable)
- TrademarkId (FK → Trademarks, nullable)
- CaseId (FK → Cases, nullable)
- UploadedAt

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - User login

### Patents
- `GET /api/patents` - Get all patents (with pagination, filtering)
- `GET /api/patents/{id}` - Get patent by ID
- `POST /api/patents` - Create new patent
- `PUT /api/patents/{id}` - Update patent
- `DELETE /api/patents/{id}` - Delete patent

### Trademarks
- `GET /api/trademarks` - Get all trademarks (with pagination, filtering)
- `GET /api/trademarks/{id}` - Get trademark by ID
- `POST /api/trademarks` - Create new trademark
- `PUT /api/trademarks/{id}` - Update trademark
- `DELETE /api/trademarks/{id}` - Delete trademark

### Cases
- `GET /api/cases` - Get all cases (with pagination, filtering)
- `GET /api/cases/{id}` - Get case by ID
- `POST /api/cases` - Create new case
- `PUT /api/cases/{id}` - Update case
- `DELETE /api/cases/{id}` - Delete case

### Clients
- `GET /api/clients` - Get all clients (with pagination)
- `GET /api/clients/{id}` - Get client by ID
- `POST /api/clients` - Create new client
- `PUT /api/clients/{id}` - Update client
- `DELETE /api/clients/{id}` - Delete client

### Dashboard
- `GET /api/dashboard` - Get dashboard statistics and data

## Setup Instructions

### Prerequisites

- .NET 8.0 SDK
- Node.js 18+ and npm

### Backend Setup

1. Navigate to backend:
```bash
cd Backend/IPFlowAPI
```

2. Restore packages:
```bash
dotnet restore
```

3. Create database:
```bash
export PATH="$PATH:$HOME/.dotnet/tools"
dotnet ef database update
```

4. Run backend:
```bash
dotnet run
```

Backend runs at: http://localhost:5006

### Frontend Setup

1. Navigate to frontend:
```bash
cd Frontend/ipflow-app
```

2. Install dependencies:
```bash
npm install
```

3. Run frontend:
```bash
npm start
```

Frontend runs at: http://localhost:4200

### Default Login

- Email: admin@ipflow.com
- Password: password123

Register via API or create new users through Swagger UI at http://localhost:5006/swagger

## Key Implementation Details

### Database Normalization
- Third Normal Form (3NF) compliance
- Foreign key constraints with appropriate delete behaviors
- Unique constraints on application numbers and emails
- Indexed columns for optimized querying

### OOP Principles
- Dependency Injection throughout the application
- Interface-based design (Repository and Service patterns)
- Single Responsibility Principle (SRP)
- Separation of Concerns (Controllers, Services, Repositories)

### Security Features
- JWT token authentication with 24-hour expiration
- BCrypt password hashing
- Role-based authorization
- CORS policy configuration
- HTTPS enforcement

### Performance Optimizations
- Async/await pattern for all I/O operations
- Entity Framework query optimization
- Pagination support for large datasets
- Selective data loading with Include statements

### RESTful API Design
- Standard HTTP methods (GET, POST, PUT, DELETE)
- Consistent response formats
- Proper status codes
- Query parameter support for filtering and pagination

## Development Practices

- Clean code without excessive comments (self-documenting)
- Consistent naming conventions
- Modular architecture
- LINQ queries for database operations
- DTO pattern for data transfer
- Async repository pattern

## Future Enhancements

- Document upload functionality
- Email notifications for upcoming deadlines
- Advanced search and filtering
- Reporting and analytics
- Audit logging
- Multi-tenancy support
- Export to PDF functionality

## License

This project is developed as a portfolio demonstration project.

## Author

**Leena Shah**
- Email: shah.leena.287@gmail.com
- LinkedIn: linkedin.com/in/fjiolla
- GitHub: github.com/fjiolla
- Portfolio: fjiolla.vercel.app
