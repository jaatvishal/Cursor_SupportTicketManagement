# AI Prompts — Debugging

## Prompt 1: SQL Server Connection

**Prompt:**
> Backend fails to connect to SQL Server Docker container with "ConnectionError: Failed to connect to localhost:1433". The .env has DB_SERVER=localhost, DB_PORT=1433, DB_USER=sa. How do I fix this?

**AI Response Summary:**
- Check Docker container is running
- Verify SA password matches
- Add trustServerCertificate: true for self-signed certs
- Set encrypt: false for local dev

**Accepted:** trustServerCertificate fix  
**Changed:** Added both options to .env.example  
**Rejected:** None

---

## Prompt 2: Status Error Not Showing

**Prompt:**
> When I click an invalid status transition in the UI, the API returns 422 but no error message appears on screen. The handleStatusChange function catches the error and sets statusError state.

**AI Response Summary:**
- Error is caught but alert may not be rendered in all DOM positions
- Suggested placing error alert above status buttons unconditionally

**Accepted:** Moved error alert rendering  
**Changed:** None  
**Rejected:** None
