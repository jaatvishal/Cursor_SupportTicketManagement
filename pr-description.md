# PR Description

## Summary

Implement the Core Support Ticket Management System with Angular 19,
ASP.NET Core 9, Entity Framework Core 9, and SQL Server Express. Includes an
enforced status state machine, comments, search/filter, validation, automatic
local database initialization, and HTTP integration tests.

## Features Implemented

- Create, list, view, and update support tickets
- Add comments to tickets
- Status state machine with 5 valid transitions
- Keyword search and status filter
- Backend request and business-rule validation
- Frontend error state display
- SQL Server persistence with schema and seed data

## Technical Changes

### Backend (`src/backend/`)
- ASP.NET Core solution with Api → Core → Infrastructure layers
- State machine service with transition validation
- EF Core SQL Server persistence
- Controller validation and typed DTO records
- Semantic HTTP errors (400/404/422)

### Frontend (`src/frontend/`)
- Angular 19 SPA with standalone components and TypeScript
- Ticket list with search/filter
- Create ticket form with validation
- Ticket detail with edit, comments, status actions
- API client with error handling

### Database (`database/`)
- Schema migration script (Users, Tickets, Comments)
- Seed data (5 users, 5 tickets, 7 comments)
- Setup documentation

### Tests (`tests/`)
- xUnit state-machine tests
- ASP.NET Core `WebApplicationFactory` integration tests

## Database Changes

- New tables: Users, Tickets, Comments
- CHECK constraints on priority and status enums
- Foreign keys with cascade delete on comments
- Indexes on status, assignedTo, ticketId

## Testing Done

- [x] State-machine and API integration tests pass
- [x] All valid transitions verified
- [x] All 15 invalid transitions rejected
- [x] Terminal states confirmed
- [x] API tests run with EF Core InMemory (no local SQL Server required)

## AI Usage Summary

Cursor AI was used across all lifecycle phases: planning, design, implementation, testing, debugging, review, and documentation. See `ai-prompts/` for detailed prompt history.

## Screenshots / Demo Notes

1. Start SQL Server Express, then start backend and frontend
2. Navigate to http://localhost:4200
3. View seeded tickets, search by keyword, filter by status
4. Create a new ticket, add comments, change status through valid transitions
5. Attempt invalid transition (e.g., Open → Resolved) to see error handling

## Known Limitations

- No authentication (Stretch feature)
- No pagination on ticket list
- Users are seeded only (no CRUD UI)
- Multi-instance optimistic concurrency is not implemented

## Future Improvements

- JWT authentication with protected routes
- User management CRUD
- Filter by priority and assignee
- Pagination and sorting
- Interactive Swagger UI (OpenAPI JSON is already generated in Development)
- Automated deployment/CI
- CI workflow with GitHub Actions
