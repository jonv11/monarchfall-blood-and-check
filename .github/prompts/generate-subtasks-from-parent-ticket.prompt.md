---
name: generate-subtasks-from-parent-ticket
description: Decompose a parent Jira ticket into actionable subtasks aligned to MFBC conventions.
argument-hint: key="MFBC-###" sprint="..." deps="..."
---

You are decomposing a parent Jira ticket for Monarchfall: Blood & Check (MFBC) into actionable subtasks.

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.

If critical info is missing, ask at most two clarifying questions. Do not invent work not implied by the parent.

Be interactive and action-oriented:
- Fetch the parent ticket via `acli` when a key is provided.
- Propose subtasks with scope, acceptance criteria, and validation.
- Before creating Jira subtasks, show a compact preview and ask for confirmation.
- After confirmation, execute the actions and report results.

Inputs (use ${input:...} variables):
- Parent ticket key: ${input:key}
- Sprint context (if relevant): ${input:sprint}
- Known dependencies/linked issues: ${input:deps}

Interactive flow (follow in order):
1) Fetch the parent ticket: `acli jira workitem view ${input:key}`
2) Draft the subtask breakdown and dependency notes.
3) Show a compact preview (subtask titles + key AC).
4) Ask for confirmation to create subtasks.
5) On confirmation, run the `acli` actions and report the result.

Produce the output in this order:
1) Proposed subtask list (short titles)
2) For each subtask: purpose, scope (In/Out), acceptance criteria, validation
3) Suggested ordering and dependency notes
4) Items that should remain as a checklist in the parent
5) Action plan (exact `acli` commands to run after confirmation)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- Subtasks are distinct, non-overlapping, and measurable.
- Each subtask has clear acceptance criteria and validation.
- Dependencies are called out when applicable.
- Output is ready to convert to Jira subtasks.
