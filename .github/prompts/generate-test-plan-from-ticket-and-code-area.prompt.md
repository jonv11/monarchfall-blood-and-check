---
name: generate-test-plan-from-ticket-and-code-area
description: Create a test plan from a Jira ticket and code area aligned to MFBC conventions.
argument-hint: key="MFBC-###" area="..." patterns="..." risks="..."
---

You are producing a test plan for a Jira ticket in Monarchfall: Blood & Check (MFBC).

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.

If critical info is missing, ask at most two clarifying questions. Do not invent requirements or fake coverage metrics.

Be interactive and action-oriented:
- Fetch the ticket via `acli` when a key is provided.
- Draft the test plan with names and commands.
- Before posting the plan to Jira, show a compact preview and ask for confirmation.
- After confirmation, execute the action and report results.

Inputs (use ${input:...} variables):
- Jira ticket key: ${input:key}
- Target code area/modules: ${input:area}
- Existing tests or patterns: ${input:patterns}
- Known risks or edge cases: ${input:risks}

Interactive flow (follow in order):
1) Fetch the ticket: `acli jira workitem view ${input:key}`
2) Draft the test plan and proposed test names.
3) Show a compact preview (test items + key assertions).
4) Ask for confirmation to add the plan as a Jira comment or update the ticket.
5) On confirmation, run the `acli` action and report the result.

Produce the output in this order:
1) Test plan (unit/integration/manual items)
2) Coverage expectations and key assertions
3) Proposed test file/class names following MFBC conventions
4) Minimal list of required test commands
5) Action plan (exact `acli` commands to run after confirmation)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- Test plan is specific and tied to ticket scope.
- Proposed names follow MFBC naming conventions.
- Includes negative and edge cases where applicable.
- Includes CLI smoke tests only if relevant.
- Output is actionable and concise.
