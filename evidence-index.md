# Implementation Evidence Index

This index makes source and test evidence directly inspectable. All paths are
relative to the repository root and are committed as text source files.

## Backend source

| Evidence | Path |
|----------|------|
| HTTP endpoints and validation | `src/backend/SupportTicket.Api/Controllers/TicketsController.cs` |
| API startup, CORS, database initialization | `src/backend/SupportTicket.Api/Program.cs` |
| Ticket state machine | `src/backend/SupportTicket.Core/Services/TicketStateMachine.cs` |
| Domain entities | `src/backend/SupportTicket.Core/Entities/` |
| Request/response DTOs | `src/backend/SupportTicket.Core/DTOs/TicketDtos.cs` |
| EF Core database mapping | `src/backend/SupportTicket.Infrastructure/Data/AppDbContext.cs` |
| Automatic schema and seed initialization | `src/backend/SupportTicket.Infrastructure/Data/DatabaseInitializer.cs` |
| Ticket business/data operations | `src/backend/SupportTicket.Infrastructure/Services/TicketService.cs` |

## Frontend source

| Evidence | Path |
|----------|------|
| Routes | `src/frontend/src/app/app.routes.ts` |
| Typed API service and error handling | `src/frontend/src/app/services/ticket.service.ts` |
| Ticket list/search/filter | `src/frontend/src/app/pages/ticket-list/` |
| Create form | `src/frontend/src/app/pages/ticket-create/` |
| Detail/edit/comments/status UI | `src/frontend/src/app/pages/ticket-detail/` |

## Test source

| Evidence | Path |
|----------|------|
| State-machine unit tests | `tests/SupportTicket.Tests/TicketStateMachineTests.cs` |
| HTTP API integration tests | `tests/SupportTicket.Tests/TicketStatusApiTests.cs` |
| Test project/package references | `tests/SupportTicket.Tests/SupportTicket.Tests.csproj` |

Run all backend tests:

```bash
cd src/backend
dotnet test
```

Run the frontend production build:

```bash
cd src/frontend
npm install
npm run build
```

## Database evidence

| Evidence | Path |
|----------|------|
| Create database | `database/schema-or-migrations/000_create_database.sql` |
| Create tables/constraints/indexes | `database/schema-or-migrations/001_create_tables.sql` |
| Repeatable sample data | `database/seed-data/seed.sql` |
| Local SQL Express setup | `database/setup-notes.md` |

## Git evidence

| Commit | Purpose |
|--------|---------|
| `06fa104` | SQL Server schema and seed data |
| `449b17a` | .NET backend, state machine, and initial tests |
| `e230caf` | Angular frontend |
| `01256f3` | Lifecycle artifacts |
| `179e5b6` | SQL Express Integrated Security setup |
| `c996086` | Automatic database initialization/seeding |
| `b6900a1` | Additional terminal-state and validation tests |

Feature branches are retained on the remote as workflow evidence:
`feature/dotnet-backend`, `feature/angular-frontend`,
`feature/documentation`, and `dev`.

## Lifecycle traceability

- Criterion-level mapping: `acceptance-criteria.md`
- Architecture and decisions: `design-notes.md`
- API routes and responses: `api-contract.md`
- Test execution: `test-results.md`
- Authentic AI iteration: `ai-prompts/raw-session-log.md`
