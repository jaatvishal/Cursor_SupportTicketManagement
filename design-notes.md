# Design Notes

## Architecture Overview

```
┌─────────────────────┐     HTTP/JSON     ┌─────────────────────┐     EF Core     ┌─────────────────┐
│  Angular 19 SPA     │ ◄───────────────► │  .NET 9 Web API     │ ◄─────────────► │  SQL Server     │
│  (Standalone)       │  localhost:5000   │  (ASP.NET Core)     │                 │  SupportTicketDB│
└─────────────────────┘                   └─────────────────────┘                 └─────────────────┘
```

## Backend Design (.NET 9)

### Project Structure
- **SupportTicket.Api** — Controllers, Program.cs, CORS, OpenAPI
- **SupportTicket.Core** — Entities, DTOs, enums, state machine, interfaces
- **SupportTicket.Infrastructure** — EF Core DbContext, TicketService, DI

### Key Design Decisions
1. **State machine as static service** in Core layer — pure, testable
2. **PATCH /api/tickets/{id}/status** — separate endpoint for state transitions
3. **Entity Framework Core 9** with SQL Server provider
4. **Record types for DTOs** — immutable request/response objects
5. **HTTP 422** for invalid state transitions (UnprocessableEntity)

## Frontend Design (Angular 19)

- **Standalone components** — no NgModules
- **Pages:** TicketList, TicketCreate, TicketDetail
- **Services:** TicketService with HttpClient
- **Routing:** Angular Router with lazy-ready structure
- **Error handling:** Centralized in service, displayed via alert components

## Database Design

See [data-model.md](data-model.md). EF Core maps to existing SQL Server schema with Users, Tickets, Comments tables.

## Validation Strategy

| Layer | Approach |
|-------|----------|
| Frontend | Template-driven forms, required field checks |
| API Controller | Request validation before service call |
| Service | Business rules, user existence, state machine |
| Database | CHECK constraints on enums |

## Testing Strategy

- **Unit tests:** TicketStateMachine (28 tests)
- **Integration tests:** WebApplicationFactory + InMemory DB (6 tests)
- Total: 34 passing tests
