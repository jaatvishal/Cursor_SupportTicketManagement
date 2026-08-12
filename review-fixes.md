# Review Fixes

Changes made after AI-assisted code review and the 68/100 assessment:

## 1. Database Connection Security

**File:** `src/backend/SupportTicket.Api/appsettings.Development.json`
**Change:** Configured `localhost\SQLEXPRESS` with Windows Integrated Security,
encryption, and trusted local certificate.
**Why:** Match the actual local database without committing credentials.

## 2. Validation Error Format

**File:** `src/backend/SupportTicket.Api/Controllers/TicketsController.cs`
**Change:** Standardized ASP.NET Core validation responses as
`ApiErrorResponse(error, details)`.
**Why:** The Angular service can display consistent field-level errors.

## 3. Status Error Display

**File:** `src/frontend/src/app/pages/ticket-detail/ticket-detail.component.ts`
**Change:** Added `statusError` state and template alert rendering.
**Why:** Invalid transitions returned 422 but error wasn't visible to users.

## 4. Git Ignore

**File:** `.gitignore`  
**Change:** Added entries for `.env`, `node_modules/`, `dist/`, build artifacts.  
**Why:** Prevent accidental commit of secrets and dependencies.

## 5. Consistent API Error Responses

**File:** `src/backend/SupportTicket.Api/Controllers/TicketsController.cs`
**Change:** Mapped invalid transitions to 422, missing resources to 404, and
validation failures to 400.
**Why:** HTTP status codes should match error semantics for frontend handling.

## 6. Frontend API Error Handling

**File:** `src/frontend/src/app/services/ticket.service.ts`
**Change:** `handleError` joins the API `details` array into the displayed message.
**Why:** Display field-level validation errors to users.

## 7. Assessment Traceability

**Files:** `acceptance-criteria.md`, `requirements-analysis.md`
**Change:** Resolved product questions and mapped each Core criterion to source,
tests, and introducing commits.
**Why:** Replace unsupported completion claims with inspectable evidence.

## 8. Documentation Stack Alignment

**Files:** `api-contract.md`, `debugging-notes.md`, `reflection.md`,
`pr-description.md`, `test-strategy.md`, and prompt artifacts
**Change:** Removed stale React/Express/Zod claims and documented the real
Angular/.NET pivot and failures.
**Why:** Keep lifecycle evidence synchronized with delivered code.

## 9. Database Initialization

**File:** `src/backend/SupportTicket.Infrastructure/Data/DatabaseInitializer.cs`
**Change:** Create tables and seed an empty SQL Express database at API startup.
**Why:** Creating `SupportTicketDB` in SSMS did not create its schema.
