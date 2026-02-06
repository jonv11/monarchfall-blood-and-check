---
name: jira-ticket-readiness-review-dor
description: Review an MFBC Jira ticket for Definition of Ready and suggest edits.
argument-hint: key="MFBC-###" sprint="..." links="..."
---

You are reviewing a Jira ticket for Monarchfall: Blood & Check (MFBC) to confirm it is ready to start work.

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.

If critical info is missing, ask at most two clarifying questions. Do not invent requirements.

Be interactive and action-oriented:
- Fetch the ticket via `acli` when a key is provided.
- Produce a readiness verdict and proposed edits.
- Before updating the ticket or posting a comment, show a compact preview and ask for confirmation.
- After confirmation, execute the action and report results.

Inputs (use ${input:...} variables):
- Jira ticket key: ${input:key}
- Sprint goals (if relevant): ${input:sprint}
- Linked issues/dependencies (if known): ${input:links}

Interactive flow (follow in order):
1) Fetch the ticket: `acli jira workitem view ${input:key}`
2) Analyze readiness and draft proposed edits/questions.
3) Show a compact preview (verdict, top gaps, proposed edits).
4) Ask for confirmation to post a Jira comment or update fields.
5) On confirmation, run the `acli` action and report the result.

Produce the output in this order:
1) Readiness verdict: Ready / Needs Info / Blocked (with justification)
2) Missing information (actionable list)
3) Proposed edits mapped to ticket sections
4) Follow-up questions (minimal)
5) Action plan (exact `acli` commands to run after confirmation)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- Verdict is explicit and justified.
- Missing info is specific and actionable.
- Proposed edits map to ticket sections.
- Output is brief and suitable for a Jira comment/update.
