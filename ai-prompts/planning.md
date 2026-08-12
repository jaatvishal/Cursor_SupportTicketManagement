# AI Prompts — Planning

> Provenance: reconstructed activity summary, not a verbatim transcript. See
> `README.md` and `raw-session-log.md` in this folder.

## Prompt 1: Requirements Analysis

**Prompt:**
> I need to build a Support Ticket Management System for an AI-assisted engineering assessment. The project is Option 1 (Backend-Heavy). Core requirements include: create/list/view/update tickets, add comments, enforced status state machine (Open→In Progress→Resolved→Closed, Open→Cancelled, In Progress→Cancelled), keyword search, status filter, SQL Server database, input validation, and integration tests for the state machine. Help me analyze the requirements and create a task breakdown.

**AI Response Summary:**
- Broke down requirements into 5 phases: Foundation, Backend, Frontend, Testing, Documentation
- Identified state machine as the core engineering challenge
- Listed edge cases: terminal states, invalid transitions, missing users
- Suggested 3-tier architecture

**Accepted:** Phase breakdown, architecture suggestion, edge case list  
**Changed:** Added SQL Server-specific setup considerations  
**Rejected:** Suggestion to implement auth in Core (kept as Stretch)

---

## Prompt 2: Implementation Plan

**Prompt:**
> Create an implementation plan with milestones, risks, and AI usage plan for each phase. Use the assessment's required repository structure.

**AI Response Summary:**
- 5 milestones from database to artifacts
- Risk matrix with mitigations
- AI usage plan mapped to each phase

**Accepted:** Full implementation plan structure  
**Changed:** None  
**Rejected:** None
