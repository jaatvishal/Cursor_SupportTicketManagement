# Debugging Notes

These notes record failures that actually occurred during the Cursor session.
The earlier version incorrectly retained debugging notes from the abandoned
React/Express prototype; those notes were removed during the assessment review.

## Issue 1: Requested stack differed from the first implementation

### Problem
The first generated implementation used React and Express. The user corrected
the requirement: “frontend is Angular with latest versions, backend is Dot net
core with latest versions.”

### How I Investigated
1. Checked the installed runtimes: Node 22.14 was present, but `dotnet` was not.
2. Installed .NET 9 SDK.
3. Tried the latest Angular CLI. Angular CLI 22 rejected Node 22.14 because it
   required Node 22.22.3 or newer.
4. Selected Angular 19, the latest major compatible with the available runtime.

### How AI Helped
Cursor rebuilt the repository as an ASP.NET Core 9 solution with Angular 19
standalone components and removed the prototype source.

### What I Validated
- `dotnet build` succeeded with zero warnings/errors.
- Angular `npm run build` succeeded.
- Repository searches were used to find stale prototype references.

### Final Fix
The delivered stack is Angular 19, ASP.NET Core 9, EF Core 9, and SQL Server.
The runtime constraint and pivot are also recorded in
`ai-prompts/raw-session-log.md`.

---

## Issue 2: EF Core package and integration-test provider failures

### Problem
Two concrete failures occurred:

1. Adding the unversioned latest EF Core package selected EF Core 10, which is
   incompatible with `net9.0` (`NU1202`).
2. API integration tests initially registered both SQL Server and InMemory
   providers, producing: “Only a single database provider can be registered.”

### How I Investigated
1. Read the NuGet compatibility error and pinned EF Core packages to 9.0.4.
2. Traced service registration through `DependencyInjection.cs` and the test
   application factory.
3. Added an explicit `Testing` environment that registers only InMemory.

### How AI Helped
Cursor changed dependency registration to select exactly one provider based on
the host environment and updated `CustomWebApplicationFactory`.

### What I Validated
`dotnet test` passed all state-machine and HTTP integration tests.

### Final Fix
- Production/Development: EF Core SQL Server.
- Testing: EF Core InMemory.
- Package versions aligned to .NET 9.

---

## Issue 3: SQL Express database existed but no tables were visible

### Problem
The user created `SupportTicketDB` in SSMS, but creating a database alone did
not execute the table and seed scripts.

### How I Investigated
1. Confirmed the configured instance was `localhost\SQLEXPRESS`.
2. Compared EF Core property naming with the original SQL script and corrected
   the script to PascalCase column names.
3. Distinguished database creation from schema creation and seeding.

### How AI Helped
Cursor added `DatabaseInitializer.InitializeAsync`, which calls EF Core
`EnsureCreatedAsync` and inserts sample data only when `Users` is empty.
Manual `.bat`, PowerShell, and SQL setup paths remain available.

### What I Validated
- Existing data is not deleted by startup initialization.
- Automated tests still pass.
- The documented connection uses Windows Integrated Security and no password.

### Final Fix
Starting `SupportTicket.Api` on Windows now creates `Users`, `Tickets`, and
`Comments` in an empty `SupportTicketDB`, then seeds 5 users, 5 tickets, and
7 comments.

---

## Issue 4: Assessment review found documentation drift

### Problem
The assessment scored the project at 68/100 and identified React/Express/Zod
references in documents for an Angular/.NET implementation.

### How I Investigated
Ran a repository-wide search for `React`, `Express`, `Zod`, `mssql`,
`localhost:3001`, `Jest`, and `Supertest`, then checked each match against the
delivered source.

### How AI Helped
Cursor produced a file-by-file audit, updated the affected artifacts, added a
traceability matrix, and preserved this remediation prompt as authentic prompt
evidence.

### What I Validated
The final repository-wide stale-stack search is recorded in
`test-results.md`. Historical prototype terms remain only where explicitly
labelled as an abandoned attempt.
