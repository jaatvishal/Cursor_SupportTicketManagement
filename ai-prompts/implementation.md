# AI Prompts — Implementation

## Prompt 1: Database Schema

**Prompt:**
> Create SQL Server migration script for Users, Tickets, and Comments tables. Include CHECK constraints for priority and status enums, foreign keys, and indexes. Also create seed data with 5 users, 5 tickets across different statuses, and 7 comments.

**AI Response Summary:**
- Generated 001_create_tables.sql with all constraints
- Generated seed.sql with realistic sample data
- Tickets cover all 5 statuses

**Accepted:** Both SQL scripts  
**Changed:** Used GETUTCDATE() instead of GETDATE() for timestamps  
**Rejected:** None

---

## Prompt 2: Backend State Machine

**Prompt:**
> Implement the ticket status state machine as a pure TypeScript service. Valid transitions: Open→In Progress, In Progress→Resolved, Resolved→Closed, Open→Cancelled, In Progress→Cancelled. Include isValidTransition, validateTransition, and getAllowedTransitions functions.

**AI Response Summary:**
- stateMachine.ts with VALID_TRANSITIONS map
- Pure functions with descriptive error messages
- Type-safe with TicketStatus type

**Accepted:** Full state machine service  
**Changed:** None  
**Rejected:** None

---

## Prompt 3: Backend API

**Prompt:**
> Build the Express REST API with routes for tickets CRUD, status changes, comments, and user listing. Use Zod validation, layered architecture (routes→services→repositories), and proper error handling.

**AI Response Summary:**
- Complete backend with 7 endpoints
- Zod schemas for all write operations
- Error handler middleware mapping errors to HTTP codes

**Accepted:** Full backend implementation  
**Changed:** Added separate PATCH /status endpoint  
**Rejected:** Class-based controllers (used functional approach)

---

## Prompt 4: Frontend UI

**Prompt:**
> Build React frontend with ticket list (search/filter), create form, and detail page (edit, comments, status actions). Show only valid status transition buttons. Display API errors clearly.

**AI Response Summary:**
- 3 pages with React Router
- Search with debounce, status filter dropdown
- Status action buttons driven by getAllowedTransitions
- Error alerts for API failures

**Accepted:** Full frontend  
**Changed:** Added edit mode toggle on detail page  
**Rejected:** Material UI (used custom CSS for simplicity)

---

## Prompt 5: Environment Configuration

**Prompt:**
> Set up environment configuration for SQL Server connection. Create .env.example with all required variables. Ensure no secrets are committed.

**AI Response Summary:**
- .env.example with all DB variables
- Database config reading from environment
- .gitignore for .env files

**Accepted:** Configuration setup  
**Changed:** Added DB_TRUST_SERVER_CERTIFICATE for Docker  
**Rejected:** None
