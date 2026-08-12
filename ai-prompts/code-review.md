# AI Prompts — Code Review

> Provenance: reconstructed activity summary, not a verbatim transcript. See
> `README.md` and `raw-session-log.md` in this folder.

## Prompt 1: Security and Quality Review

**Prompt:**
> Review the Support Ticket Management codebase for: SQL injection vulnerabilities, secrets in code, missing validation, error handling gaps, and state machine correctness. Suggest improvements.

**AI Response Summary:**
- No SQL injection (all parameterized queries)
- No secrets in committed files
- Suggested: add .gitignore, improve error format consistency
- State machine correctly isolated and tested
- Suggested but rejected: NgRx, replacing EF Core, API versioning

**Accepted:** .gitignore, error format improvements  
**Changed:** Added consistent error response format  
**Rejected:** NgRx, replacing EF Core, API versioning, WebSocket suggestions
