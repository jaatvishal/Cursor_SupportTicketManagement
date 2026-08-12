# Requirement Analysis

## Selected Project Option

**Option 1: Backend-Heavy — Support Ticket Management System**

## My Understanding

A small internal support ticket application where users create tickets, assign them to agents, add comments, and progress tickets through a defined lifecycle. The core challenge is enforcing a status state machine on the backend — invalid transitions must be rejected. Users are seeded (no user management UI). Data persists in SQL Server.

## Functional Requirements

### Core (Mandatory)
1. **Create ticket** — title, description, priority, optional assignee
2. **List tickets** — from database with keyword search and status filter
3. **View ticket detail** — full ticket info with comments
4. **Update ticket** — title, description, priority, assignee
5. **Status transitions** — enforced state machine (5 valid transitions)
6. **Add comments** — message with author and timestamp
7. **Search/filter** — keyword search on title/description, filter by status
8. **Validation** — backend rejects invalid input; frontend shows errors
9. **Persistence** — SQL Server, data survives restart
10. **Integration tests** — prove state machine rules

### Stretch (Not Implemented)
- Authentication / JWT
- User CRUD and role management
- Filter by priority/assignee, sorting, pagination
- Swagger/OpenAPI docs
- Docker Compose, CI workflow

## Non-Functional Requirements

- Clean, readable C# and TypeScript
- Meaningful Git commit history with feature branches
- Full lifecycle artifacts in repository
- No secrets committed
- README with setup instructions

## Assumptions

1. Users are pre-seeded; no registration/login UI needed
2. SQL Server Express is available locally as `localhost\SQLEXPRESS`
3. Single-tenant internal tool (no multi-org support)
4. `createdBy` is selected from seeded users in the UI (no auth session)
5. Comments are append-only (no edit/delete)

## Clarifications and Decisions

No product owner response was available during the assessment, so the following
scope decisions were recorded and implemented:

| Question | Decision | Evidence |
|----------|----------|----------|
| Who can be assigned? | The UI limits assignees to Admin and Agent users. Authentication is out of Core scope, so any current UI user can perform the reassignment. | `ticket-create.component.ts`, `ticket-detail.component.ts` |
| Email notifications? | Not implemented; notifications are outside Core scope. | `acceptance-criteria.md` |
| Maximum comment length? | A comment must be non-empty. No additional maximum was specified, so SQL Server uses `NVARCHAR(MAX)`. | `TicketsController.cs`, `001_create_tables.sql` |
| Can Cancelled tickets reopen? | No. Cancelled and Closed are terminal states. | `TicketStateMachine.cs`, `TicketStateMachineTests.cs` |
| How is local database access handled? | SQL Server Express with Windows Integrated Security; the API creates and seeds an empty database on startup. | `appsettings.Development.json`, `DatabaseInitializer.cs` |

## Edge Cases

1. Transitioning a ticket that's already in a terminal state (Closed/Cancelled)
2. Assigning to a non-existent user ID
3. Creating a ticket with empty title or description
4. Searching with special SQL characters (handled via parameterized queries)
5. Concurrent status updates on the same ticket (identified risk; distributed
   optimistic concurrency is not included in Core and is documented as a limitation)
6. Adding comments to non-existent tickets
