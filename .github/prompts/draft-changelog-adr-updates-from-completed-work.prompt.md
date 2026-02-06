---
name: draft-changelog-adr-updates-from-completed-work
description: Decide if CHANGELOG/ADR updates are needed and draft the entries.
argument-hint: key="MFBC-###" pr="..." changes="..."
---

You are drafting documentation updates for completed work in Monarchfall: Blood & Check (MFBC).

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.
Reference the ADR template at [docs/decisions/TEMPLATE.md](../../docs/decisions/TEMPLATE.md).

If critical info is missing, ask at most two clarifying questions. Do not invent changes not present in the work.

Be interactive and action-oriented:
- Gather work details from Jira/PR summaries.
- Decide whether CHANGELOG and/or ADR updates are required.
- Before editing files, show a compact preview and ask for confirmation.
- After confirmation, update the files and report results.

Inputs (use ${input:...} variables):
- Jira ticket key: ${input:key}
- PR or commit summary (if available): ${input:pr}
- Relevant change notes (features/fixes/breaking changes): ${input:changes}

Interactive flow (follow in order):
1) Fetch ticket/PR context if provided:
   - `acli jira workitem view ${input:key}`
   - `gh pr view ${input:pr} --json title,body,files`
2) Decide if CHANGELOG and/or ADR updates are required.
3) Draft the CHANGELOG entry and/or ADR summary/template.
4) Show a compact preview (decision + draft text).
5) Ask for confirmation to update files.
6) On confirmation, apply updates and report the result.

Produce the output in this order:
1) Decision: CHANGELOG update needed? ADR update needed? (with rationale)
2) Draft CHANGELOG entry (if applicable)
3) Draft ADR summary or template fill (if applicable)
4) Rationale for why updates are required (or not)
5) Action plan (exact `git`/`gh`/`acli` commands and files to update after confirmation)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- Correctly determine whether CHANGELOG/ADR updates are needed.
- Drafts follow repo doc conventions.
- Output is ready for review/commit.
