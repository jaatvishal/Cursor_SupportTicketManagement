# AI Prompt Evidence and Provenance

This folder contains two different evidence types. They are deliberately
separated so a reviewer can distinguish historical reconstruction from
contemporaneous evidence.

## 1. Reconstructed phase summaries

The following files were prepared after implementation from the Cursor session
and repository history:

- `planning.md`
- `design.md`
- `implementation.md`
- `testing.md`
- `debugging.md`
- `code-review.md`
- `documentation.md`

They preserve prompt intent, response summaries, and accept/change/reject
decisions. They are **not complete verbatim transcript exports**. The first
submission did not state this limitation clearly, which made the history appear
more polished and complete than the available evidence supported.

## 2. Authentic session evidence

`raw-session-log.md` records user prompt excerpts, failed commands, corrections,
and validation outcomes that occurred in the actual Cursor conversation. It
includes the abandoned React/Express attempt, the Angular/.NET correction,
package/runtime failures, SQL Express setup issue, and assessment remediation.

The log does not invent missing assistant messages. Where a full response was
not preserved as repository text, it is explicitly labelled as a summary.

## Traceability

- Requirements → source/tests/commits: `../acceptance-criteria.md`
- Debugging investigation and fixes: `../debugging-notes.md`
- Test commands and results: `../test-results.md`
- Git workflow: `../tool-workflow.md`

## Going forward

For future work, prompt evidence should be committed during each development
phase, including failed attempts, before moving to the next phase. This avoids
retrospective reconstruction and keeps prompt history aligned with Git history.
