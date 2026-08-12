# AI Prompts — Debugging

> Provenance: reconstructed activity summary, not a verbatim transcript. It has
> been superseded by the failures recorded in `raw-session-log.md` and
> `../debugging-notes.md`.

## Prompt 1: Stack correction

**Prompt:**
> frontend is Angular with leatest versions, backend is Dot net core with
> Leates Versions

**AI Response Summary:**
- Replaced the prototype with Angular 19 and ASP.NET Core 9
- Recorded EF Core 10/.NET 9 and Angular CLI/Node compatibility failures

**Accepted:** Angular/.NET implementation
**Changed:** Angular 19 was selected as the latest compatible major
**Rejected:** The first React/Express implementation

---

## Prompt 2: SQL Express tables not visible

**Prompt:**
> i am unable to the tables, in my local db, i am created the Db for you

**AI Response Summary:**
- Creating the database does not execute schema scripts
- Added startup `DatabaseInitializer` using EF Core `EnsureCreatedAsync`
- Added idempotent seed data and retained manual setup scripts

**Accepted:** Automatic initialization and local run guidance
**Changed:** Connection targets `SupportTicketDB`, not `master`
**Rejected:** None
