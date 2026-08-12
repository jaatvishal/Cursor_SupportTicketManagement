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
- SQL Server Express (local `localhost\SQLEXPRESS`)
- Angular CLI 19 (`npm install -g @angular/cli@19`)

## Quick Start

### 1. Database Setup (SQL Server Express — no Docker)

Ensure **SQL Server (SQLEXPRESS)** is running. The API now automatically creates the
`SupportTicketDB` tables and inserts sample data the first time it starts.

You can also initialize it manually from the **repository root** on Windows:

```bat
database\setup-local.bat
```

Or manually:
```bat
sqlcmd -S localhost\SQLEXPRESS -E -i database\schema-or-migrations\000_create_database.sql
sqlcmd -S localhost\SQLEXPRESS -E -d SupportTicketDB -i database\schema-or-migrations\001_create_tables.sql
sqlcmd -S localhost\SQLEXPRESS -E -d SupportTicketDB -i database\seed-data\seed.sql
```

See [database/setup-notes.md](database/setup-notes.md) for full details.

### 2. Backend (.NET API)

```bash
cd src/backend
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
# 36 tests: state machine + API integration
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
├── evidence-index.md               # Source/test/commit evidence map
└── [lifecycle artifacts]           # Requirements, design, etc.
```

Reviewers can start with [evidence-index.md](evidence-index.md) for direct links
to backend source, frontend source, tests, database scripts, and introducing
commits.

## Configuration

Connection string in `src/backend/SupportTicket.Api/appsettings.Development.json`:

```
Data Source=localhost\SQLEXPRESS;Initial Catalog=SupportTicketDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True
```

Uses **Windows Authentication** — no SQL username/password required.

## Run locally in Cursor or Visual Studio

### Cursor / VS Code terminal

Open the repository folder and use two terminals:

```bat
cd src\backend
dotnet run --project SupportTicket.Api
```

```bat
cd src\frontend
npm install
npm start
```

Then open `http://localhost:4200`. Starting the API creates and seeds the database
automatically. Refresh **Tables** in SSMS after the API starts.

### Visual Studio 2022

1. Open `src/backend/SupportTicket.sln`.
2. Set `SupportTicket.Api` as the startup project.
3. Press `F5` or `Ctrl+F5`.
4. Start the Angular frontend separately with `npm start` from `src/frontend`.

Visual Studio must run under the Windows account that can access
`localhost\SQLEXPRESS`, because the connection uses Integrated Security.

## License

Assessment project — not for production use.
