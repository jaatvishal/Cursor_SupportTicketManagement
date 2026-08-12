# Candidate Information

**Name:** Vishal Jaat  
**Role:** Full-Stack Developer  
**Primary Technology Stack:** .NET 9, Angular 19, SQL Server  

**Primary AI Tool Used:** Cursor  
**Project Option Selected:** Option 1 — Backend-Heavy Support Ticket Management System  

**Assessment Start Date:** August 2026  
**Submission Date:** August 2026  

## Project Summary

Built a full-stack Support Ticket Management System with Angular 19 frontend,
.NET 9 Web API backend, and SQL Server database. Includes an enforced ticket
status state machine, comments, search/filter, validation, and 36 unit/API
integration tests.

## Tools Used

| Tool | Purpose |
|------|---------|
| Cursor AI | Planning, design, implementation, testing, debugging, review, documentation |
| .NET 9 + ASP.NET Core | Backend REST API |
| Angular 19 | Frontend SPA (standalone components) |
| Entity Framework Core 9 | ORM for SQL Server |
| SQL Server | Relational data persistence |
| xUnit + WebApplicationFactory | Unit and integration testing |

## Setup Summary

1. Start SQL Server (Docker or local)
2. Run database migration and seed scripts
3. `dotnet run --project src/backend/SupportTicket.Api`
4. `ng serve` in `src/frontend`
5. `dotnet test` in `src/backend`

See [README.md](README.md) for detailed setup instructions.
