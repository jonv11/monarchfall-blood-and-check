---
name: implementation-plan-from-ticket
description: Create an implementation plan from a Jira ticket aligned to MFBC conventions.
argument-hint: key="MFBC-###" deps="..." areas="..."
---

You are turning a Jira ticket into an actionable implementation plan for Monarchfall: Blood & Check (MFBC).

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.

If critical info is missing, ask at most two clarifying questions. Do not invent requirements.

Be interactive and action-oriented:
- Fetch the ticket via `acli` when a key is provided.
- Draft the plan with steps, risks, and test strategy.
- Before posting the plan to Jira, show a compact preview and ask for confirmation.
- After confirmation, execute the action and report results.

Inputs (use ${input:...} variables):
- Jira ticket key: ${input:key}
- Related tickets/dependencies: ${input:deps}
- Relevant repo areas/modules: ${input:areas}

Interactive flow (follow in order):
1) Fetch the ticket: `acli jira workitem view ${input:key}`
2) Draft the plan and identify doc updates.
3) Show a compact preview (steps + risks + test plan).
4) Ask for confirmation to add the plan as a Jira comment or update the ticket.
5) On confirmation, run the `acli` action and report the result.

Produce the output in this order:
1) Ordered implementation steps
2) Risks and mitigations (tied to steps)
3) Test plan summary aligned to MFBC conventions
4) Likely impacted files/modules
5) Required doc updates (CHANGELOG, ADRs) if applicable
6) Action plan (exact `acli` commands to run after confirmation)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- Steps are concrete and sequenced.
- Risks are realistic and tied to steps.
- Test plan aligns with MFBC conventions.
- Output is actionable and brief.
