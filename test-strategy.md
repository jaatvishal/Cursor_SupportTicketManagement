# Test Strategy

## Test Scope

Focus on the **status state machine** as the signature judgment piece, with supporting validation tests.

## Unit Tests

**File:** `tests/SupportTicket.Tests/TicketStateMachineTests.cs`
**Runner:** xUnit on .NET 9 (no external database required)

| Test Group | Cases |
|-----------|-------|
| Valid transitions | Open→In Progress, Open→Cancelled, In Progress→Resolved, In Progress→Cancelled, Resolved→Closed |
| Invalid transitions | 15 invalid combinations (e.g., Open→Resolved, Closed→Open) |
| Same-status | All 5 statuses allow staying in same status |
| Terminal states | Closed and Cancelled have zero allowed transitions |
| getAllowedTransitions | Correct arrays for each status |

**Total:** ~25 test cases

## API / Integration Tests

**File:** `tests/SupportTicket.Tests/TicketStatusApiTests.cs`
**Runner:** xUnit + `WebApplicationFactory<Program>` + EF Core InMemory

| Test Group | Cases |
|-----------|-------|
| Valid transitions via API | Open→In Progress→Resolved→Closed (full lifecycle) |
| Invalid transitions | Open→Resolved, Open→Closed, Closed→Open |
| Validation | Invalid status value, empty required fields |
| Terminal states | Closed→Open and Cancelled→Open both return HTTP 422 |

## Component Tests

Not implemented in Core — the state machine logic is backend-enforced and tested via unit/integration tests. Frontend error display is validated manually.

## Edge Case Tests

Covered within unit tests:
- Terminal state transitions (Closed→anything, Cancelled→anything)
- Skipping states (Open→Resolved, Open→Closed)
- Backward transitions (In Progress→Open, Resolved→In Progress)

## Tests Not Covered (and why)

| Area | Reason |
|------|--------|
| Frontend component tests | Core tier requires one meaningful test tier; state machine integration tests satisfy this |
| E2E browser tests | Out of Core scope; manual validation sufficient |
| Performance/load tests | Not required for assessment scope |
| Concurrent update tests | Edge case; would need transaction-level locking |
| Authentication tests | Auth not implemented (Stretch) |

## Running Tests

```bash
cd src/backend
dotnet test
```

The API integration tests are committed source, run without local SQL Server,
and exercise the real ASP.NET Core HTTP pipeline. Local SQL Server persistence
is verified separately with the queries in `database/setup-notes.md`.
