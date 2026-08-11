# Debugging Notes

## Issue 1: SQL Server Connection Failure

### Problem
Backend failed to connect to SQL Server on first run with error: `ConnectionError: Failed to connect to localhost:1433`.

### How I Investigated
1. Checked if SQL Server Docker container was running: `docker ps`
2. Verified port 1433 was exposed
3. Checked `.env` credentials matched Docker SA password
4. Tested connection with `sqlcmd -S localhost,1433 -U sa -P "..." -Q "SELECT 1"`

### How AI Helped
Asked Cursor to diagnose the mssql connection configuration. AI identified that `trustServerCertificate` must be `true` for local Docker SQL Server instances using self-signed certificates.

### What I Validated
- Confirmed `DB_TRUST_SERVER_CERTIFICATE=true` in `.env`
- Confirmed `DB_ENCRYPT=false` for local development
- Verified connection with sqlcmd before starting the API

### Final Fix
Updated `src/backend/src/config/database.ts` to read `DB_TRUST_SERVER_CERTIFICATE` from environment and set `trustServerCertificate: true` in mssql options.

---

## Issue 2: Status Transition Error Not Displayed in UI

### Problem
When clicking an invalid status transition button, the API returned 422 but the frontend didn't show the error message.

### How I Investigated
1. Checked browser Network tab — confirmed 422 response with error message
2. Traced frontend code in `TicketDetailPage.tsx` — `handleStatusChange` caught the error but `statusError` state wasn't rendered in all cases

### How AI Helped
Cursor identified that the error was being caught but the `statusError` alert was placed inside the status card which wasn't visible when `allowedTransitions` was empty.

### What I Validated
- Tested invalid transition (Open → Resolved) via UI
- Confirmed error alert now appears in the status panel
- Verified valid transitions still work correctly

### Final Fix
Ensured `statusError` alert renders above status action buttons regardless of transition availability.

---

## Issue 3: Zod Validation Error Format Mismatch

### Problem
Frontend expected `error.details` as string array, but some Zod errors returned nested path formats.

### How I Investigated
1. Sent POST with empty title — checked response format
2. Found `details` array contained `"title: Title is required"` format

### How AI Helped
AI suggested mapping Zod errors to `path: message` format in the validate middleware.

### What I Validated
- Empty title submission shows "Validation failed: title: Title is required"
- Invalid priority value shows appropriate message
- Frontend `handleResponse` correctly joins details array

### Final Fix
Updated `src/backend/src/middleware/validate.ts` to format Zod errors as `"path: message"` strings in the details array.
