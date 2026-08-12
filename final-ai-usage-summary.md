# Final AI Usage Summary

## Tool: Cursor AI

**Project:** Support Ticket Management System (Option 1 — Backend-Heavy)  
**Duration:** Full lifecycle — planning through documentation  
**Prompt evidence:** reconstructed phase summaries plus an authentic session log

## Usage by Phase

| Phase | Prompts | Key Outcomes |
|-------|---------|-------------|
| Planning | 2 | Requirements analysis, implementation plan, task breakdown |
| Design | 3 | Architecture, API contract, data model, UI flows |
| Implementation | 5 | Database schema, backend API, state machine, frontend UI |
| Testing | 2 | xUnit state-machine tests and ASP.NET Core integration tests |
| Debugging | 4 | Stack pivot, package compatibility, test provider, SQL Express schema |
| Code Review | 1 | Security review, validation gaps, consistency check |
| Documentation | 3 | README, lifecycle artifacts, prompt history |

## What Was Accepted

- 3-tier architecture (Angular → ASP.NET Core → SQL Server)
- State machine as isolated pure service
- ASP.NET Core controller and service validation
- Layered backend (Api → Core → Infrastructure)
- Comprehensive test matrix for all transitions
- Lifecycle artifact structure from assessment template

## What Was Changed

- Replaced the incorrect React/Express prototype with Angular 19/.NET 9
- Pinned EF Core 9 after unversioned latest selected incompatible EF Core 10
- Selected Angular 19 after Angular CLI 22 rejected the available Node version
- Configured SQL Express with Integrated Security and automatic initialization
- Mirrored types in frontend instead of sharing backend types directly

## What Was Rejected

- NgRx state management (unnecessary for three pages)
- Hand-written ADO.NET (EF Core provides safe parameterization and mapping)
- API versioning (single client, no need)
- WebSocket real-time updates (out of scope)
- Third-party logging framework (ASP.NET Core logging is sufficient)

## Validation Approach

1. **State-machine tests** — valid, invalid, same-state, and terminal transitions
2. **Frontend validation** — Angular production build passed; local Windows
   browser-to-SQL-Express verification remains a documented manual step
3. **Security review** — Checked for SQL injection, secret leaks, CORS config
4. **Consistency check** — repository-wide stale-stack search and contract review

## Key Learnings

1. AI excels at boilerplate and test generation but needs human review for environment-specific config
2. Providing clear context (assessment requirements, tech stack) produces better output
3. A stack pivot must update code, tests, and every artifact in the same change
4. Polished summaries are not substitutes for raw failures and corrections

## Prompt History Location

Prompt evidence:
- `ai-prompts/README.md` — provenance and limitations
- `ai-prompts/raw-session-log.md` — authentic prompts, failures, and corrections
- Reconstructed phase summaries:
- `ai-prompts/planning.md`
- `ai-prompts/design.md`
- `ai-prompts/implementation.md`
- `ai-prompts/testing.md`
- `ai-prompts/debugging.md`
- `ai-prompts/code-review.md`
- `ai-prompts/documentation.md`
