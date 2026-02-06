# Prompt: Jira Ticket Readiness Review (DoR)

## Context
You are reviewing a Jira ticket for Monarchfall: Blood & Check (MFBC) to confirm it is ready to start work. The output must follow MFBC conventions and be suitable for a Jira comment or update.

## Goal
Review a ticket for readiness using MFBC's priority order (guidelines -> conventions -> good practices) and produce a concise readiness report.

## Scope (In / Out)
### In
- Analyze ticket fields and description to identify gaps, risks, and required clarifications.
- Produce a readiness verdict and actionable next steps.
- Include relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

### Out
- Estimation, prioritization, or implementation decisions.

## Inputs
- Jira ticket content (summary, description, fields)
- Current sprint goals (if relevant)
- Linked issues or dependencies (if known)

## Output / Deliverables
- Readiness verdict: Ready / Needs Info / Blocked
- Missing information list (actionable)
- Proposed edits to the ticket (concise, mapped to sections)
- Suggested follow-up questions (minimal)

## Acceptance Criteria
- Output explicitly prioritizes guidelines -> conventions -> good practices.
- Verdict is explicit and justified.
- Missing info is specific and actionable.
- Proposed edits map to ticket sections.
- Output is brief and suitable for a Jira comment or update.
- Includes relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

## Implementation Notes
- Ask at most two clarifying questions if critical info is missing.
- Do not invent requirements.
- Keep recommendations aligned to MFBC conventions.

## Validation
- Run on three existing tickets and confirm it flags real gaps without over-asking.

---

## Prompt
You are reviewing an MFBC Jira ticket for readiness. Prioritize: guidelines -> conventions -> good practices.

If critical info is missing, ask at most two clarifying questions. Do not invent requirements.

Inputs:
- Ticket content:
- Sprint goals (if relevant):
- Linked issues/dependencies:

Produce the output in this order:
1) Readiness verdict: Ready / Needs Info / Blocked (with justification)
2) Missing information (actionable list)
3) Proposed edits mapped to ticket sections
4) Follow-up questions (minimal)
5) Relevant commands (`acli`, `git`, `gh`) to access or create required artifacts

Acceptance criteria reminders:
- Guidelines -> conventions -> good practices is explicit.
- Verdict is justified.
- Output is brief and suitable for a Jira comment/update.
