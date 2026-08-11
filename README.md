# Support Ticket Management System

A full-stack support ticket management application built with AI-assisted development (Cursor). Uses **Angular 19** frontend, **.NET 9 Web API** backend, and **SQL Server** database.

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Frontend | Angular 19, TypeScript, Standalone Components |
| Backend | .NET 9, ASP.NET Core Web API, Entity Framework Core 9 |
| Database | Microsoft SQL Server |
| Testing | xUnit, WebApplicationFactory, EF Core InMemory |

## Prerequisites

- .NET 9 SDK
- Node.js 18+ and npm
- SQL Server 2019+ (or Docker)
- Angular CLI 19 (`npm install -g @angular/cli@19`)

## Quick Start

### 1. Database Setup

See [database/setup-notes.md](database/setup-notes.md).

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong!Passw0rd" \
  -p 1433:1433 --name support-ticket-sql \
  -d mcr.microsoft.com/mssql/server:2022-latest

sqlcmd -S localhost,1433 -U sa -P "YourStrong!Passw0rd" \
  -i database/schema-or-migrations/001_create_tables.sql
sqlcmd -S localhost,1433 -U sa -P "YourStrong!Passw0rd" \
  -i database/seed-data/seed.sql
```

### 2. Backend (.NET API)

```bash
cd src/backend
# Update connection string in SupportTicket.Api/appsettings.Development.json
dotnet run --project SupportTicket.Api
# API: http://localhost:5000
# Swagger: http://localhost:5000/openapi/v1.json (Development)
```

### 3. Frontend (Angular)

```bash
cd src/frontend
npm install
ng serve
# App: http://localhost:4200
```

### 4. Tests

```bash
cd src/backend
dotnet test
# 34 tests: state machine + API integration
```

## Features (Core)

- Create, list, view, and update support tickets
- Add comments to tickets
- Enforced status state machine with backend validation
- Keyword search and status filter
- Input validation with meaningful error messages
- Data persists across restarts (SQL Server)

## Status State Machine

```
Open         → In Progress
In Progress  → Resolved
Resolved     → Closed
Open         → Cancelled
In Progress  → Cancelled
```

Invalid transitions return HTTP 422 and are displayed in the UI.

## Project Structure

```
├── src/
│   ├── backend/                    # .NET 9 Solution
│   │   ├── SupportTicket.Api/      # Web API controllers
│   │   ├── SupportTicket.Core/     # Entities, DTOs, state machine
│   │   └── SupportTicket.Infrastructure/  # EF Core, services
│   └── frontend/                   # Angular 19 SPA
├── tests/
│   └── SupportTicket.Tests/        # xUnit tests
├── database/                       # SQL Server schema & seeds
├── ai-prompts/                     # Prompt history
└── [lifecycle artifacts]           # Requirements, design, etc.
```

## Configuration

Update `src/backend/SupportTicket.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=SupportTicketDB;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;Encrypt=False"
  },
  "FrontendUrl": "http://localhost:4200"
}
```

Frontend API URL is configured in `src/frontend/src/app/services/ticket.service.ts` (`http://localhost:5000`).

## License

Assessment project — not for production use.
