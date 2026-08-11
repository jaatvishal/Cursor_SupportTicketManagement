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
2. All SQL queries use parameterized inputs (no string concatenation)
3. Zod validation on all write endpoints
4. Separate PATCH endpoint for status changes enforces state machine independently
5. Frontend only shows valid transition buttons (defense in depth)
6. No secrets in committed files (.env.example only)

### Areas for Improvement
1. No request rate limiting (acceptable for assessment scope)
2. No pagination on ticket list (Stretch feature)
3. `createdBy` selected manually in UI rather than from auth session
4. No optimistic UI updates on status change
5. Comments are not editable/deletable (by design, but worth noting)

## Changes Made After Review

1. Added `trustServerCertificate` config for local SQL Server
2. Improved error message formatting in validate middleware
3. Added status error display in ticket detail UI
4. Added `.gitignore` to exclude `.env`, `node_modules`, `dist`
5. Ensured all API error responses follow consistent `{ error, details? }` format

## Suggestions Rejected (and why)

| Suggestion | Reason |
|-----------|--------|
| Add Redux for state management | Over-engineering for 3 pages; local state sufficient |
| Add Winston logging | Console logging adequate for assessment scope |
| Add API versioning (/api/v1/) | Unnecessary for single-client app |
| Add WebSocket for real-time updates | Out of Core scope |
| Use an ORM (Prisma/TypeORM) | Raw SQL with mssql is simpler and more transparent for assessment |
