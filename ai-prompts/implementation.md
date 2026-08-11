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

## Prompt 3: Backend API (.NET 9)

**Prompt:**
> Build the ASP.NET Core Web API with controllers for tickets CRUD, status changes, comments, and user listing. Use Entity Framework Core 9 with SQL Server, layered architecture (Api→Core→Infrastructure), and proper error handling with HTTP 422 for invalid transitions.

**AI Response Summary:**
- Complete .NET 9 solution with 3 projects
- TicketStateMachine service in Core layer
- EF Core DbContext with SQL Server
- REST controllers with validation

**Accepted:** Full backend implementation  
**Changed:** Added separate PATCH /status endpoint  
**Rejected:** Minimal API endpoints (used controllers for clarity)

---

## Prompt 4: Frontend UI (Angular 19)

**Prompt:**
> Build Angular 19 frontend with standalone components for ticket list (search/filter), create form, and detail page (edit, comments, status actions). Show only valid status transition buttons. Display API errors clearly.

**AI Response Summary:**
- Angular 19 SPA with standalone components
- TicketService with HttpClient
- Status action buttons driven by getAllowedTransitions
- Error alerts for API failures

**Accepted:** Full frontend  
**Changed:** Used Angular 19 (latest compatible with Node 22)  
**Rejected:** NgModules (used standalone components)

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
