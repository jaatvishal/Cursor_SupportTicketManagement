# Tool Workflow

## Primary Tool: Cursor AI

This project was developed entirely using Cursor as the primary AI-assisted development tool.

## Workflow Phases

### 1. Planning & Requirements
- Used Cursor to analyze assessment requirements and break them into actionable tasks
- Generated requirements analysis, acceptance criteria, and implementation plan documents
- Prompts stored in `ai-prompts/planning.md`

### 2. Design
- Designed architecture (3-tier: React → Express → SQL Server)
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
        ├── feature/database-setup ────────────── (schema + seeds)
        ├── feature/backend-api ───────────────── (REST API + state machine)
        ├── feature/frontend-ui ───────────────── (React components)
        ├── feature/integration-tests ─────────── (state machine tests)
        └── feature/documentation ─────────────── (lifecycle artifacts)
```

## Cursor-Specific Configuration

See `tool-specific/cursor-workflow/` for Cursor rules and workflow notes.
