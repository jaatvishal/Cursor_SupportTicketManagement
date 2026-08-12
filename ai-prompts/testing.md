# AI Prompts — Testing

> Provenance: reconstructed activity summary, not a verbatim transcript. See
> `README.md` and `raw-session-log.md` in this folder.

## Prompt 1: State Machine Test Strategy

**Prompt:**
> Create comprehensive xUnit tests for the ticket status state machine. Test all
> 5 valid transitions, all invalid transitions, same-status transitions, and
> terminal states with Theory/MemberData.

**AI Response Summary:**
- 31 test cases covering all transition combinations
- Parameterized tests with xUnit Theory/MemberData
- Terminal state verification
- getAllowedTransitions tests

**Accepted:** Full test suite  
**Changed:** None  
**Rejected:** None

---

## Prompt 2: API Integration Tests

**Prompt:**
> Create ASP.NET Core integration tests with WebApplicationFactory that test
> valid status transitions through the HTTP API, invalid transitions returning
> 422, and validation rejection. Use EF Core InMemory so tests do not depend on
> the developer's SQL Express instance.

**AI Response Summary:**
- `TicketStatusApiTests.cs` using the real controller pipeline
- `CustomWebApplicationFactory` with the Testing environment
- Tests for full lifecycle, terminal guards, and invalid input

**Accepted:** Integration test structure  
**Changed:** Fixed dual EF provider registration discovered during the first run
**Rejected:** None
