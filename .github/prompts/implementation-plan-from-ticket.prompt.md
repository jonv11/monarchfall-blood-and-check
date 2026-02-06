# Prompt: Implementation Plan From Ticket

## Context
You are turning a Jira ticket into an actionable implementation plan for Monarchfall: Blood & Check (MFBC). The output must follow MFBC conventions and be concise.

## Goal
Create a step-by-step implementation plan from a Jira ticket, prioritizing guidelines -> conventions -> good practices.

## Scope (In / Out)
### In
- Derive ordered implementation steps from the ticket.
- Identify risks and mitigations.
- Provide a test plan summary aligned with MFBC conventions.
- Call out files/modules likely impacted.
- Explicitly note required doc updates when applicable (e.g., CHANGELOG, ADRs).
- Include relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

### Out
- Actual code changes or estimates.

## Inputs
- Jira ticket summary and description
- Related tickets or dependencies
- Relevant repo areas/modules

## Output / Deliverables
- Ordered implementation steps
- Risk list with mitigations
- Test plan summary aligned to MFBC conventions
- Likely impacted files/modules
- Doc update callouts (CHANGELOG, ADRs) when applicable

## Acceptance Criteria
- Output explicitly prioritizes guidelines -> conventions -> good practices.
- Steps are concrete and sequenced.
- Risks are realistic and tied to steps.
- Test plan aligns with MFBC conventions.
- Output is actionable and brief.
- Includes relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

## Implementation Notes
- Ask at most two clarifying questions if critical info is missing.
- Do not invent requirements.
- Highlight missing info that blocks planning.

## Validation
- Apply to two tickets and confirm plan aligns with actual work performed.

---

## Prompt
You are creating an MFBC implementation plan from a Jira ticket. Prioritize: guidelines -> conventions -> good practices.

If critical info is missing, ask at most two clarifying questions. Do not invent requirements.

Inputs:
- Jira ticket summary and description:
- Related tickets/dependencies:
- Relevant repo areas/modules:

Produce the output in this order:
1) Ordered implementation steps
2) Risks and mitigations (tied to steps)
3) Test plan summary aligned to MFBC conventions
4) Likely impacted files/modules
5) Required doc updates (CHANGELOG, ADRs) if applicable
6) Relevant commands (`acli`, `git`, `gh`) to access or create required artifacts

Acceptance criteria reminders:
- Guidelines -> conventions -> good practices is explicit.
- Steps are concrete and sequenced.
- Output is actionable and brief.
