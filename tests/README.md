# Tests

## Structure

| Location | Purpose |
|----------|---------|
| `tests/SupportTicket.Tests/TicketStateMachineTests.cs` | Unit tests for status state machine (no DB) |
| `tests/SupportTicket.Tests/TicketStatusApiTests.cs` | API integration tests (InMemory DB) |

## Running Tests

```bash
cd src/backend
dotnet test
```

**Result:** 36 tests — state-machine unit tests plus API integration tests for
valid/invalid transitions, both terminal states, and invalid input.
