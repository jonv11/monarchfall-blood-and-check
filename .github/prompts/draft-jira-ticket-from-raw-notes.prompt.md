---
name: draft-jira-ticket-from-raw-notes
description: Draft a complete Jira ticket from raw notes with MFBC conventions and hierarchy guidance.
argument-hint: notes="..." sprint="..." component="..." dependencies="..."
---

You are drafting a Jira ticket for Monarchfall: Blood & Check (MFBC) from raw notes.

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.
Use the naming guide at [docs/development/naming-conventions.md](../../docs/development/naming-conventions.md).

If critical info is missing, ask at most two clarifying questions. If sprint context is missing, ask for it explicitly.
Do not invent requirements; surface unknowns as questions.

Be interactive and action-oriented:
- Guide the user through a short, ordered flow.
- Execute required CLI actions yourself when inputs are sufficient.
- Before any action that changes Jira state (create/update/transition), present a short preview and ask for confirmation.
- After confirmation, run the action and report the result.

Inputs (use ${input:...} variables):
- Raw notes: ${input:notes}
- Sprint context (goal, timeframe, active epics/features): ${input:sprint}
- Target component/module (if known): ${input:component}
- Deadlines/dependencies (if any): ${input:dependencies}

Interactive flow (follow in order):
1) Validate inputs and ask up to two clarifying questions if critical info is missing.
2) Use `acli` to find likely parent candidates if a parent is not explicit. Example:
   - `acli jira workitem search --jql "project = MFBC AND status != Done"`
3) Draft the ticket content (see required sections below).
4) Show a compact preview (summary, issue type, hierarchy, key acceptance criteria).
5) Ask for confirmation to create or update the ticket.
6) On confirmation, perform the `acli` action and report the result.

Produce the output in this order:
1) Suggested summary line
2) Recommended issue type with justification (default to best-fit)
3) Suggested hierarchy (parent/child/subtask) with rationale, including reuse of an existing parent when appropriate
4) Jira ticket draft text with sections:
   - Context
   - Goal
   - Scope (In / Out)
   - Acceptance Criteria
   - Implementation Notes
   - Validation
   - Dependencies
   - Risks
   - Open Questions
5) Action plan (the exact `acli` commands you will run after confirmation)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- Issue type selection is explicit and justified.
- Hierarchy proposal is logical and improves organization.
- Acceptance criteria are measurable.
- Open questions are explicit and minimal.
- Output is ready to paste into Jira (ADF-ready).
