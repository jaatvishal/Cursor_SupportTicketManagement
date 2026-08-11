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

- Clean, readable code with TypeScript
- Meaningful Git commit history with feature branches
- Full lifecycle artifacts in repository
- No secrets committed
- README with setup instructions

## Assumptions

1. Users are pre-seeded; no registration/login UI needed
2. SQL Server is available locally (Docker or installed instance)
3. Single-tenant internal tool (no multi-org support)
4. `createdBy` is selected from seeded users in the UI (no auth session)
5. Comments are append-only (no edit/delete)

## Clarifications (Questions for a Product Owner)

1. Should agents be able to reassign tickets to other agents only, or can any user reassign?
2. Should there be email notifications on status changes?
3. Is there a maximum comment length?
4. Should cancelled tickets be reopenable?

## Edge Cases

1. Transitioning a ticket that's already in a terminal state (Closed/Cancelled)
2. Assigning to a non-existent user ID
3. Creating a ticket with empty title or description
4. Searching with special SQL characters (handled via parameterized queries)
5. Concurrent status updates on the same ticket
6. Adding comments to non-existent tickets
