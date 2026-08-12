# Tool Workflow

## Primary Tool: Cursor AI

This project was developed entirely using Cursor as the primary AI-assisted development tool.

## Workflow Phases

### 1. Planning & Requirements
- Used Cursor to analyze assessment requirements and break them into actionable tasks
- Generated requirements analysis, acceptance criteria, and implementation plan documents
- Prompts stored in `ai-prompts/planning.md`

### 2. Design
- Designed architecture (Angular 19 → ASP.NET Core 9 → SQL Server)
- Defined API contract, data model, and UI flows with AI assistance
- Prompts stored in `ai-prompts/design.md`

### 3. Implementation
- Iterative feature development: database → backend API → frontend UI
- Used feature branches (dev → feature/*) with meaningful commits
- Prompts stored in `ai-prompts/implementation.md`

### 4. Testing
- AI-assisted test strategy and state machine integration tests
- Validated all valid/invalid transitions programmatically
- Prompts stored in `ai-prompts/testing.md`

### 5. Debugging
- Used AI to diagnose connection issues, validation errors, and state machine edge cases
- Prompts stored in `ai-prompts/debugging.md`

### 6. Code Review
- AI-assisted self-review for security, validation gaps, and error handling
- Prompts stored in `ai-prompts/code-review.md`

### 7. Documentation
- Generated lifecycle artifacts, README, and setup instructions
- Prompts stored in `ai-prompts/documentation.md`

## Git Workflow

```
main ───────────────────────────────────────────── (stable releases)
  │
  └── dev ──────────────────────────────────────── (integration branch)
        │
        ├── feature/dotnet-backend ────────────── (.NET API + tests)
        ├── feature/angular-frontend ──────────── (Angular components)
        └── feature/documentation ─────────────── (lifecycle artifacts)

cursor/support-ticket-dotnet-angular-35fa ───────── (SQL Express + review fixes)
```

## Cursor-Specific Configuration

See `tool-specific/cursor-workflow/` for Cursor rules and workflow notes.

## Evidence Provenance

The original phase files under `ai-prompts/` are reconstructed activity
summaries, not complete transcript exports. This limitation is stated explicitly
in `ai-prompts/README.md`. Authentic corrections, failed commands, and the
assessment-remediation exchange are preserved in `ai-prompts/raw-session-log.md`.
