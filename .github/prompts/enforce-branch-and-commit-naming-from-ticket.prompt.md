# Prompt: Enforce Branch and Commit Naming From Ticket

## Context
You are deriving branch and commit naming guidance from a Jira ticket for Monarchfall: Blood & Check (MFBC). The output must follow MFBC naming conventions and be immediately usable.

## Goal
Generate branch name and commit message templates tied to a ticket key and scope, prioritizing guidelines -> conventions -> good practices.

## Scope (In / Out)
### In
- Recommend a branch name following repo conventions.
- Provide a commit message template aligned to repo conventions.
- Map ticket scope to commit granularity guidance.
- Include relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

### Out
- Performing git operations.

## Inputs
- Jira ticket key and summary
- Type of work (feature/bug/chore/docs/etc.)
- MFBC naming conventions (see repo docs)

## Output / Deliverables
- Recommended branch name (type/MFBC-###-short-description)
- Commit message template (<type>(<scope>): <summary> (MFBC-###))
- Mapping of ticket scope to commit granularity

## Acceptance Criteria
- Output explicitly prioritizes guidelines -> conventions -> good practices.
- Includes ticket key in branch and commit guidance.
- Naming is consistent with repo conventions.
- Output can be used immediately by a developer.
- Includes relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

## Implementation Notes
- Ask at most one clarifying question if key data is missing.
- Do not invent scope beyond the ticket summary.
- Keep output short and actionable.

## Validation
- Apply to three tickets and confirm names are consistent and unique.

---

## Prompt
You are deriving branch and commit naming guidance from an MFBC Jira ticket. Prioritize: guidelines -> conventions -> good practices.

If key data is missing, ask at most one clarifying question. Do not invent scope beyond the ticket summary.

Inputs:
- Ticket key and summary:
- Work type (feature/bug/chore/docs/etc.):
- Naming conventions reference (link or excerpt):

Produce the output in this order:
1) Recommended branch name
2) Commit message template
3) Commit granularity guidance tied to ticket scope
4) Relevant commands (`acli`, `git`, `gh`) to access or create required artifacts

Acceptance criteria reminders:
- Guidelines -> conventions -> good practices is explicit.
- Ticket key is included in branch and commit guidance.
- Output is immediately usable.
