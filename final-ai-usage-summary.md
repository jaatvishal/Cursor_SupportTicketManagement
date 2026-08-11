# Final AI Usage Summary

## Tool: Cursor AI

**Project:** Support Ticket Management System (Option 1 — Backend-Heavy)  
**Duration:** Full lifecycle — planning through documentation  
**Total Prompt Sessions:** ~15 organized by activity phase

## Usage by Phase

| Phase | Prompts | Key Outcomes |
|-------|---------|-------------|
| Planning | 2 | Requirements analysis, implementation plan, task breakdown |
| Design | 3 | Architecture, API contract, data model, UI flows |
| Implementation | 5 | Database schema, backend API, state machine, frontend UI |
| Testing | 2 | Test strategy, 31 state machine unit tests, integration tests |
| Debugging | 2 | SQL Server connection fix, error display fix |
| Code Review | 1 | Security review, validation gaps, consistency check |
| Documentation | 3 | README, lifecycle artifacts, prompt history |

## What Was Accepted

- 3-tier architecture (React → Express → SQL Server)
- State machine as isolated pure service
- Zod for backend validation
- Layered backend (routes → services → repositories)
- Comprehensive test matrix for all transitions
- Lifecycle artifact structure from assessment template

## What Was Changed

- Added `trustServerCertificate` for local SQL Server (AI missed this initially)
- Simplified frontend state management (rejected Redux suggestion)
- Used raw SQL instead of ORM (rejected Prisma/TypeORM suggestion)
- Mirrored types in frontend instead of sharing backend types directly

## What Was Rejected

- Redux/state management library (unnecessary for 3 pages)
- ORM layer (raw SQL more transparent for assessment)
- API versioning (single client, no need)
- WebSocket real-time updates (out of scope)
- Winston structured logging (console sufficient)

## Validation Approach

1. **Tests first for state machine** — 31 unit tests verify all transitions before UI integration
2. **Manual UI testing** — Each user flow tested in browser
3. **Security review** — Checked for SQL injection, secret leaks, CORS config
4. **Consistency check** — API contract matches implementation, types match between layers

## Key Learnings

1. AI excels at boilerplate and test generation but needs human review for environment-specific config
2. Providing clear context (assessment requirements, tech stack) produces better output
3. Iterative prompting (implement → test → debug → review) produces higher quality than single-shot generation
4. Organizing prompt history by activity phase makes the development process traceable

## Prompt History Location

Full prompt history organized by activity:
- `ai-prompts/planning.md`
- `ai-prompts/design.md`
- `ai-prompts/implementation.md`
- `ai-prompts/testing.md`
- `ai-prompts/debugging.md`
- `ai-prompts/code-review.md`
- `ai-prompts/documentation.md`
