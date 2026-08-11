# Implementation Plan

## Overview

Build a 3-tier Support Ticket Management System: React frontend, Express/TypeScript backend, SQL Server database. Focus on the status state machine as the core engineering challenge.

## Task Breakdown

### Phase 1: Foundation
1. Project structure and repository scaffolding
2. Database schema (Users, Tickets, Comments)
3. Seed data (5 users, 5 tickets, 7 comments)
4. Environment configuration

### Phase 2: Backend API
1. Database connection layer (mssql)
2. State machine service with transition validation
3. Ticket repository (CRUD + comments)
4. REST routes with Zod validation
5. Error handling middleware

### Phase 3: Frontend
1. React app with Vite + TypeScript
2. Ticket list page with search/filter
3. Create ticket form with validation
4. Ticket detail page with edit, comments, status actions
5. Error state display

### Phase 4: Testing
1. State machine unit tests (all transitions)
2. API integration tests (valid/invalid transitions)
3. Validation tests

### Phase 5: Documentation & Artifacts
1. Lifecycle documents (requirements, design, test strategy, etc.)
2. AI prompt history
3. README and setup instructions

## Milestones

| Milestone | Deliverable |
|-----------|------------|
| M1 | Database schema + seeds running |
| M2 | Backend API with state machine |
| M3 | Frontend UI with all core features |
| M4 | Tests passing |
| M5 | All artifacts committed |

## AI Usage Plan

| Phase | AI Role |
|-------|---------|
| Planning | Requirements analysis, task breakdown |
| Design | Architecture, API contract, data model |
| Implementation | Code generation with review and iteration |
| Testing | Test case generation, edge case identification |
| Debugging | Error diagnosis, connection troubleshooting |
| Review | Security check, validation gaps |
| Documentation | Artifact generation, README |

## Risks

| Risk | Impact | Mitigation |
|------|--------|-----------|
| SQL Server not available locally | Blocks development | Docker setup documented |
| State machine edge cases missed | Invalid transitions allowed | Comprehensive test matrix |
| Frontend/backend type mismatch | Runtime errors | Shared type definitions |
| Secrets in repo | Security violation | .env.example only, .gitignore |

## Mitigation

- Docker-based SQL Server for consistent local dev
- Test every valid and invalid transition combination
- Mirror backend types in frontend
- Never commit .env files
