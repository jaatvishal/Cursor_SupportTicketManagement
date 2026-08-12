# AI Prompts — Design

> Provenance: reconstructed activity summary, not a verbatim transcript. See
> `README.md` and `raw-session-log.md` in this folder.

## Prompt 1: Architecture Design

**Prompt:**
> Design the architecture for a Support Ticket Management System using Angular
> 19, ASP.NET Core 9, Entity Framework Core 9, and SQL Server. Include the
> status state machine and validation strategy.

**AI Response Summary:**
- 3-tier architecture diagram
- Layered backend: Api → Core → Infrastructure
- State machine as pure service module
- Controller/service validation and SQL CHECK constraints

**Accepted:** Architecture, layered backend, state machine isolation  
**Changed:** Added PATCH endpoint specifically for status changes  
**Rejected:** GraphQL suggestion (REST is simpler for assessment)

---

## Prompt 2: API Contract

**Prompt:**
> Define the REST API contract for all ticket endpoints. Include request/response formats, validation rules, and error responses for the state machine.

**AI Response Summary:**
- 7 endpoints documented with full request/response schemas
- Validation rules per endpoint
- HTTP status codes: 400, 404, 422, 500

**Accepted:** Full API contract  
**Changed:** Added /tickets/users endpoint for dropdown data  
**Rejected:** None

---

## Prompt 3: Data Model

**Prompt:**
> Design the SQL Server database schema for Users, Tickets, and Comments. Include the state machine diagram and seed data plan.

**AI Response Summary:**
- ERD with relationships
- Table definitions with constraints
- State machine ASCII diagram
- 5 users, 5 tickets, 7 comments seed plan

**Accepted:** Schema, constraints, seed plan  
**Changed:** Added indexes on status and assignedTo  
**Rejected:** None
