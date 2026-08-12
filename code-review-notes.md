# Code Review Notes

## AI-Assisted Review Summary

Used Cursor to perform a self-review of the codebase focusing on:
- Security (SQL injection, secrets, CORS)
- Validation completeness
- Error handling coverage
- State machine correctness
- Code consistency

## My Review Observations

### Strengths
1. State machine is isolated in a pure service — easy to test
2. EF Core generates parameterized SQL for user-supplied search/filter values
3. ASP.NET Core controllers validate all write requests
4. Separate PATCH endpoint for status changes enforces state machine independently
5. Frontend only shows valid transition buttons (defense in depth)
6. SQL Express uses Windows Integrated Security; no password is committed

### Areas for Improvement
1. No request rate limiting (acceptable for assessment scope)
2. No pagination on ticket list (Stretch feature)
3. `createdBy` selected manually in UI rather than from auth session
4. No optimistic UI updates on status change
5. Comments are not editable/deletable (by design, but worth noting)

## Changes Made After Review

1. Configured `TrustServerCertificate=True` for local SQL Express
2. Standardized controller errors as `{ error, details? }`
3. Added status error display in the Angular ticket detail component
4. Added `.gitignore` entries for .NET and Angular build output
5. Added automatic database schema creation and idempotent seed initialization
6. Added terminal-state and invalid-status HTTP integration tests
7. Reconciled lifecycle artifacts after the React/Express → Angular/.NET pivot

## Suggestions Rejected (and why)

| Suggestion | Reason |
|-----------|--------|
| Add NgRx for state management | Over-engineering for three pages; component state and one Angular service are sufficient |
| Add a third-party logging framework | ASP.NET Core logging is sufficient for Core scope |
| Add API versioning (/api/v1/) | Unnecessary for single-client app |
| Add WebSocket for real-time updates | Out of Core scope |
| Replace EF Core with hand-written ADO.NET | EF Core already provides parameterization, mapping, and testable provider substitution |

## Review Evidence

- Backend source: `src/backend/SupportTicket.Api`, `SupportTicket.Core`,
  `SupportTicket.Infrastructure`
- Frontend source: `src/frontend/src/app`
- Test source: `tests/SupportTicket.Tests`
- Requirement mapping: `acceptance-criteria.md`
- Authentic remediation history: `ai-prompts/raw-session-log.md`
