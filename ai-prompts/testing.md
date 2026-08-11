# AI Prompts — Testing

## Prompt 1: State Machine Test Strategy

**Prompt:**
> Create comprehensive unit tests for the ticket status state machine. Test all 5 valid transitions, all invalid transitions (at least 15 cases), same-status transitions, and terminal states. Use Jest with test.each for parameterized tests.

**AI Response Summary:**
- 31 test cases covering all transition combinations
- Parameterized tests with test.each
- Terminal state verification
- getAllowedTransitions tests

**Accepted:** Full test suite  
**Changed:** None  
**Rejected:** None

---

## Prompt 2: API Integration Tests

**Prompt:**
> Create API integration tests using Supertest that test valid status transitions through the full HTTP API, invalid transitions returning 422, and validation rejection. Make tests skippable when SQL Server is not available.

**AI Response Summary:**
- Integration test file with describe.skip pattern
- RUN_INTEGRATION_TESTS env flag
- Tests for full lifecycle and invalid transitions

**Accepted:** Integration test structure  
**Changed:** Used describeIfDb pattern for cleaner skipping  
**Rejected:** None
