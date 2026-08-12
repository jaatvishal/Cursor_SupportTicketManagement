# Acceptance Criteria

## Core

- [x] A user can create a ticket via the UI
- [x] A user can view all tickets from the database
- [x] A user can open a ticket detail view
- [x] A user can update ticket fields and reassign
- [x] A user can add comments
- [x] Status changes only through valid transitions; invalid ones are rejected
- [x] Keyword search and status filter work
- [x] Data remains available after restart
- [x] Backend validation prevents invalid records
- [x] No secrets committed to the repo
- [x] State-machine integration tests pass

## Validation

- [x] Required fields (title, description, createdBy) enforced on create
- [x] Priority must be Low/Medium/High/Critical
- [x] Status must be a valid enum value
- [x] Invalid status transitions return HTTP 422 with descriptive message
- [x] Frontend displays validation errors from backend

## Error Handling

- [x] 400 for validation failures with field-level details
- [x] 404 for non-existent tickets/users
- [x] 422 for invalid status transitions
- [x] 500 for unexpected server errors (no stack traces exposed)
- [x] Frontend shows error alerts for API failures

## Testing

- [x] Unit tests for state machine (all valid/invalid transitions)
- [x] Integration tests for API status transitions (with DB)
- [x] Tests for validation rejection (empty fields, invalid status)

## Documentation

- [x] README with setup instructions
- [x] Database setup notes with schema and seed scripts
- [x] API contract documented
- [x] Environment variable example (.env.example)
- [x] Full lifecycle artifacts in repository
- [x] AI prompt history organized by activity

## Traceability Matrix

| Acceptance criterion | Implementation evidence | Test/evidence | Introducing commit |
|----------------------|-------------------------|---------------|--------------------|
| Create a ticket | `TicketsController.CreateTicket`, `TicketService.CreateTicketAsync`, `ticket-create.component.ts` | `CreateTicket_WithoutRequiredFields_ShouldReturn400`; Angular build | `449b17a`, `e230caf` |
| List and view tickets | `TicketsController.GetTickets/GetTicket`, `ticket-list.component.ts`, `ticket-detail.component.ts` | API exercised by `TicketStatusApiTests`; Angular build | `449b17a`, `e230caf` |
| Update and reassign | `TicketsController.UpdateTicket`, `TicketService.UpdateTicketAsync` | Source review and Angular build; local demo step remains | `449b17a` |
| Add comments | `TicketsController.AddComment`, `TicketService.AddCommentAsync` | Source review and Angular build; local demo step remains | `449b17a`, `e230caf` |
| Enforce valid transitions | `TicketStateMachine.cs`, `TicketsController.UpdateStatus` | `TicketStateMachineTests.cs`, `TicketStatusApiTests.cs` | `449b17a` |
| Reject terminal-state transitions | `TicketStateMachine.cs` | `InvalidTransition_FromClosed_ShouldReturn422`, `InvalidTransition_FromCancelled_ShouldReturn422` | `449b17a`, `b6900a1` |
| Search and status filter | `TicketService.GetTicketsAsync`, `ticket-list.component.ts` | Parameterized EF Core query; manual UI flow | `449b17a`, `e230caf` |
| Backend validation | `TicketsController` request validation | `CreateTicket_WithoutRequiredFields_ShouldReturn400`, `InvalidStatusValue_ShouldReturn400` | `449b17a`, `b6900a1` |
| SQL Server persistence and seed data | `AppDbContext.cs`, `DatabaseInitializer.cs`, `database/` scripts | Local setup/verification queries in `database/setup-notes.md` | `06fa104`, `c996086` |
| No committed credentials | Integrated Security connection string; `.gitignore` | Repository review | `179e5b6`, `330084d` |

Commit identifiers are stable Git evidence. The Angular build proves
compilation, not browser behavior; full browser-to-SQL-Express verification is
explicitly retained as a local Windows demo step rather than claimed as an
automated test.
