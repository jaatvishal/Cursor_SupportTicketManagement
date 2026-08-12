# Cursor Session Log — Authentic Corrections and Failures

This is a repository-friendly log of material events from the actual Cursor
conversation. User quotations and command errors are verbatim where marked.
Assistant actions are summarized when a complete transcript export was not
available. This is not presented as a complete transcript.

## Event 1 — Initial assessment request

**User prompt summary:** Build the full Core Support Ticket Management System,
use SQL Server, include all lifecycle artifacts and prompt history, and maintain
a meaningful Git history.

**What happened:** The first implementation used React, Express, Zod, and
`mssql`. That stack was not the one subsequently required.

**Accepted:** Core domain, state-machine rules, SQL schema concepts, artifact
structure.

**Changed later:** Entire application stack and all stack-specific tests.

**Why this matters:** The first submission’s phase summaries omitted this failed
attempt. The assessment correctly identified the resulting documentation drift.

---

## Event 2 — Stack correction

**User prompt (verbatim):**

> frontend is Angular with leatest versions ,
> backend is Dot net core with Leates Versions

**Assistant action summary:** Replaced the prototype with Angular and .NET,
checked installed runtimes, and scaffolded a layered ASP.NET Core solution.

**Failure captured from terminal (verbatim):**

```text
dotnet: command not found
```

**Correction:** Installed .NET 9 SDK (`9.0.316`).

**Failure captured from NuGet (verbatim):**

```text
NU1202: Package Microsoft.EntityFrameworkCore.SqlServer 10.0.10 is not
compatible with net9.0. Package Microsoft.EntityFrameworkCore.SqlServer
10.0.10 supports: net10.0
```

**Correction:** Pinned EF Core packages to 9.0.4, matching the .NET 9 target.

**Failure captured from Angular CLI (verbatim):**

```text
The Angular CLI requires a minimum Node.js version of v22.22.3 or v24.15.0
or v26.0.0.
```

**Correction:** Used Angular 19, the latest major compatible with the available
Node 22.14 runtime. This was a compatibility decision, not an unsupported claim
that Angular 19 was the newest Angular release.

**Accepted:** Angular standalone components, ASP.NET Core controllers, EF Core.

**Rejected:** Retaining React/Express for convenience.

---

## Event 3 — Integration-test provider conflict

**Prompt context:** Run the mandatory state-machine API integration tests.

**Failure captured from `dotnet test` (verbatim):**

```text
Services for database providers 'Microsoft.EntityFrameworkCore.SqlServer',
'Microsoft.EntityFrameworkCore.InMemory' have been registered in the service
provider. Only a single database provider can be registered in a service
provider.
```

**Assistant action summary:** Added a `Testing` environment and changed
dependency registration so tests use only EF Core InMemory while local runtime
uses SQL Server.

**Validation captured:**

```text
Passed! - Failed: 0, Passed: 34, Skipped: 0, Total: 34
```

**Accepted:** In-process HTTP tests through `WebApplicationFactory`.

**Changed:** Test host provider registration.

---

## Event 4 — Local SQL Express requirement

**User prompt (verbatim excerpt):**

> Data Source=localhost\SQLEXPRESS;Initial Catalog=master;Integrated
> Security=True;Persist Security Info=False;Pooling=False;
> MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;
> Command Timeout=0 -> use this local db, and create all the required database,
> tables etc. Seed the data as well. Also we dont need to use docker for now.

**Assistant action summary:**

- Changed the application catalog from `master` to `SupportTicketDB`.
- Configured Windows Integrated Security.
- Added database, schema, and seed scripts.
- Added `.bat` and PowerShell setup entry points.
- Removed Docker from the active local setup path.

**Accepted:** Local SQL Express and no committed password.

**Changed:** `Initial Catalog=SupportTicketDB`; `master` remains only the
administrative context used to create the application database.

---

## Event 5 — Database visible, tables missing

**User prompt (verbatim excerpt):**

> i am unable to the tables ,in my local db,i am created the Db for you

**Evidence:** The supplied SSMS screenshot showed `SupportTicketDB` but did not
show application tables.

**Diagnosis:** Creating the database did not execute the table/seed scripts.

**Assistant action summary:** Added
`SupportTicket.Infrastructure/Data/DatabaseInitializer.cs`. At API startup it:

1. calls `EnsureCreatedAsync`;
2. stops if users already exist;
3. otherwise inserts 5 users, 5 tickets, and 7 comments.

**Validation:** Existing tests remained green and Angular production build
succeeded. The cloud agent could not connect to the user’s Windows-local named
instance, so the local SSMS verification remained a documented user step.

---

## Event 6 — Branch and run guidance

**User prompts (verbatim excerpts):**

> bro which branch have this ,please tell me the every branch uses

> yes i am currentlyon cursor/support-ticket-dotnet-angular-35fa,but how i run
> frontend and backend code? i need to run sepratly if yes then how?

**Assistant response summary:** Identified
`cursor/support-ticket-dotnet-angular-35fa` as the active complete branch and
documented separate backend (`dotnet run`) and frontend (`npm start`) terminals.

**Repository effect:** Local run guidance was added to `README.md` and
`database/setup-notes.md`.

---

## Event 7 — Assessment feedback and remediation

**User prompt (verbatim excerpt):**

> Overall Signal: 68/100
> Indicative Standing: Developing (60-74)
>
> Complete lifecycle artifacts and working stack, but prompt history and
> evidence lean templated rather than iterative.
>
> kindly understand this report.
> make improvement in this reports

**Findings accepted:**

- Clarification questions were not resolved.
- Prompt summaries lacked failed attempts and provenance.
- API contract still used port 3001.
- Debugging/review/reflection/PR files referenced React/Express/Zod.
- Test and source evidence needed explicit paths.

**Remediation actions:**

1. Added a requirement → source → test → commit traceability matrix.
2. Converted product questions into explicit scope decisions.
3. Rewrote stale lifecycle artifacts for Angular/.NET.
4. Added this honest provenance file rather than fabricating old transcripts.
5. Added API tests for Cancelled terminal-state rejection and invalid status.
6. Re-ran backend tests, Angular build, and stale-reference searches.

**Rejected:** Inventing full historical AI responses that were not preserved.

**Reason:** Fabricated transcripts would increase apparent evidence while
reducing authenticity—the exact problem highlighted by the assessment.
