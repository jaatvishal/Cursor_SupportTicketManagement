# Reflection

## What I Built

A full-stack Support Ticket Management System with:
- React frontend (3 pages: list, create, detail)
- Express/TypeScript REST API (7 endpoints)
- SQL Server database (3 tables, seed data)
- Enforced status state machine (5 valid transitions, 15 rejected)
- 31 unit tests for state machine rules
- Complete lifecycle artifacts documenting the AI-assisted development process

## How I Used AI (Across the Lifecycle)

| Phase | How Cursor Helped |
|-------|------------------|
| Planning | Broke down assessment requirements into tasks, identified risks |
| Design | Suggested 3-tier architecture, API contract, data model |
| Implementation | Generated backend services, frontend components, database scripts |
| Testing | Created comprehensive state machine test matrix |
| Debugging | Diagnosed SQL Server connection and error display issues |
| Review | Identified security gaps, validation improvements |
| Documentation | Generated all lifecycle artifact templates |

## What AI Helped With Most

1. **State machine test matrix** — AI generated all 20 transition combinations (5 valid + 15 invalid) ensuring complete coverage
2. **Boilerplate generation** — API routes, repository layer, React pages were scaffolded quickly
3. **Documentation artifacts** — All 15+ lifecycle documents generated from templates
4. **Error handling patterns** — Consistent middleware and frontend error display

## What AI Got Wrong

1. **Initial mssql config** — Didn't include `trustServerCertificate` by default for Docker SQL Server
2. **Over-engineering suggestions** — Recommended Redux and ORMs which were unnecessary for this scope
3. **Frontend type imports** — Occasionally used backend types directly instead of mirroring in frontend

## How I Validated AI Output

1. Ran all unit tests after state machine implementation
2. Manually tested each UI flow (create, edit, comment, status change)
3. Verified invalid transitions return 422 with descriptive messages
4. Checked that no secrets appear in committed files
5. Reviewed generated SQL for injection vulnerabilities (all parameterized)

## What I Would Improve Next

1. Add Docker Compose for one-command full stack startup
2. Implement authentication (JWT) as Stretch evidence
3. Add Swagger/OpenAPI documentation
4. Set up CI pipeline with GitHub Actions
5. Add frontend component tests with React Testing Library

## Reusable Workflow

### Prompts
- Organized by activity in `ai-prompts/` (planning, design, implementation, testing, debugging, review, documentation)
- Each prompt includes context, AI response summary, and what was accepted/rejected

### Rules
- Follow existing code patterns
- Minimal scope changes
- No new dependencies unless necessary
- Validate AI output with tests

### Templates
- Lifecycle artifact templates from Part C submission guide
- PR description template
- API contract format
