# Review Fixes

Changes made after AI-assisted code review:

## 1. Database Connection Security

**File:** `src/backend/src/config/database.ts`  
**Change:** Added `trustServerCertificate` and `encrypt` options from environment variables.  
**Why:** Local Docker SQL Server requires trusting self-signed certificates.

## 2. Validation Error Format

**File:** `src/backend/src/middleware/validate.ts`  
**Change:** Standardized Zod error output to `"field: message"` format in details array.  
**Why:** Frontend expects consistent error format for display.

## 3. Status Error Display

**File:** `src/frontend/src/pages/TicketDetailPage.tsx`  
**Change:** Added `statusError` state and alert rendering in status panel.  
**Why:** Invalid transitions returned 422 but error wasn't visible to users.

## 4. Git Ignore

**File:** `.gitignore`  
**Change:** Added entries for `.env`, `node_modules/`, `dist/`, build artifacts.  
**Why:** Prevent accidental commit of secrets and dependencies.

## 5. Consistent API Error Responses

**File:** `src/backend/src/middleware/errorHandler.ts`  
**Change:** Mapped state machine errors to 422, not-found to 404, generic to 500.  
**Why:** HTTP status codes should match error semantics for frontend handling.

## 6. Frontend API Error Handling

**File:** `src/frontend/src/api/tickets.ts`  
**Change:** `handleResponse` joins `details` array into error message string.  
**Why:** Display field-level validation errors to users.
