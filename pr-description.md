# PR Description

## Summary

Implement Core Support Ticket Management System with React frontend, Express/TypeScript backend, and SQL Server database. Includes enforced status state machine, comments, search/filter, validation, and integration tests.

## Features Implemented

- Create, list, view, and update support tickets
- Add comments to tickets
- Status state machine with 5 valid transitions
- Keyword search and status filter
- Backend validation with Zod
- Frontend error state display
- SQL Server persistence with schema and seed data

## Technical Changes

### Backend (`src/backend/`)
- Express API with layered architecture (routes → services → repositories)
- State machine service with transition validation
- Zod input validation middleware
- mssql connection pooling
- Error handling middleware (400/404/422/500)

### Frontend (`src/frontend/`)
- React SPA with Vite and TypeScript
- Ticket list with search/filter
- Create ticket form with validation
- Ticket detail with edit, comments, status actions
- API client with error handling

### Database (`database/`)
- Schema migration script (Users, Tickets, Comments)
- Seed data (5 users, 5 tickets, 7 comments)
- Setup documentation

### Tests (`tests/`)
- 31 state machine unit tests
- API integration tests (valid/invalid transitions, validation)

## Database Changes

- New tables: Users, Tickets, Comments
- CHECK constraints on priority and status enums
- Foreign keys with cascade delete on comments
- Indexes on status, assignedTo, ticketId

## Testing Done

- [x] State machine unit tests: 31/31 passed
- [x] All valid transitions verified
- [x] All 15 invalid transitions rejected
- [x] Terminal states confirmed
- [x] Integration tests configured (require SQL Server)

## AI Usage Summary

Cursor AI was used across all lifecycle phases: planning, design, implementation, testing, debugging, review, and documentation. See `ai-prompts/` for detailed prompt history.

## Screenshots / Demo Notes

1. Start SQL Server, run migrations, start backend and frontend
2. Navigate to http://localhost:5173
3. View seeded tickets, search by keyword, filter by status
4. Create a new ticket, add comments, change status through valid transitions
5. Attempt invalid transition (e.g., Open → Resolved) to see error handling

## Known Limitations

- No authentication (Stretch feature)
- No pagination on ticket list
- Users are seeded only (no CRUD UI)
- Integration tests require manual SQL Server setup

## Future Improvements

- JWT authentication with protected routes
- User management CRUD
- Filter by priority and assignee
- Pagination and sorting
- Swagger/OpenAPI documentation
- Docker Compose for one-command setup
- CI workflow with GitHub Actions
