# Reflection

## What I Built

A full-stack Support Ticket Management System with:
- Angular 19 frontend using standalone components (list, create, detail)
- ASP.NET Core 9 REST API with Api, Core, and Infrastructure projects
- Entity Framework Core 9
- SQL Server database (3 tables, seed data)
- Enforced status state machine (5 valid transitions, 15 rejected)
- xUnit state-machine and API integration tests
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
2. **Boilerplate generation** — ASP.NET Core projects and Angular standalone
   components were scaffolded quickly
3. **Documentation artifacts** — All 15+ lifecycle documents generated from templates
4. **Error handling patterns** — Consistent middleware and frontend error display

## What AI Got Wrong

1. **Wrong initial stack** — The first implementation used React/Express even
   though the final requirement was Angular/.NET. This caused code and document
   churn and should have been clarified before implementation.
2. **Incompatible “latest” packages** — Unversioned EF Core selected version 10,
   which does not support .NET 9. Angular CLI 22 also required a newer Node patch.
3. **Documentation drift** — Generated lifecycle files retained prototype
   references after the stack pivot. The assessment correctly identified this.
4. **Over-polished prompt evidence** — The initial prompt files summarized ideal
   outcomes and omitted failed attempts, reducing authenticity.

## How I Validated AI Output

1. Ran all unit tests after state machine implementation
2. Built the Angular production bundle and inspected each routed UI flow; a
   complete browser-to-local-SQL-Express run must be performed on Windows
3. Verified invalid transitions return 422 through `WebApplicationFactory`
4. Checked that no secrets appear in committed files
5. Confirmed EF Core handles user-supplied search/filter values as parameters

## What I Would Improve Next

1. Add optimistic concurrency using a row-version token for multi-instance safety
2. Implement authentication (JWT) as Stretch evidence
3. Add interactive Swagger UI on top of the generated OpenAPI document
4. Set up CI with GitHub Actions
5. Add Angular component tests for forms and error states

## Reusable Workflow

### Prompts
- Organized by activity in `ai-prompts/` (planning, design, implementation, testing, debugging, review, documentation)
- Original phase files are explicitly labelled as reconstructed summaries.
- `ai-prompts/raw-session-log.md` contains authentic prompts, failures, corrections,
  and validation evidence captured from the Cursor session.

### Rules
- Follow existing code patterns
- Minimal scope changes
- No new dependencies unless necessary
- Validate AI output with tests

### Templates
- Lifecycle artifact templates from Part C submission guide
- PR description template
- API contract format

## Ownership and Assessment Response

The 68/100 review was fair: breadth was stronger than provenance. I accepted the
stack mismatch and shallow prompt-history findings. I did not manufacture old
transcripts to improve the appearance of evidence; instead, I labelled
reconstructed records honestly, preserved the real remediation exchange, added
source/test/commit traceability, and synchronized the documents with the code.
