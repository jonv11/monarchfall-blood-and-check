# Prompt: Generate Test Plan From Ticket + Code Area

## Context
You are producing a test plan for a Jira ticket in Monarchfall: Blood & Check (MFBC). The plan must align with MFBC guidelines, conventions, and good practices in that order.

## Goal
Create a test plan tied to the ticket scope and code area, prioritizing guidelines -> conventions -> good practices.

## Scope (In / Out)
### In
- Identify unit, integration, and manual test items as applicable.
- Define coverage expectations and key assertions.
- Propose test file/class names aligned to MFBC naming conventions.
- Provide a minimal list of required test commands.
- Include relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

### Out
- Writing tests or running test commands.

## Inputs
- Jira ticket summary and description
- Target code area/modules
- Existing tests or patterns
- Known risks or edge cases

## Output / Deliverables
- Test plan with unit/integration/manual test items
- Coverage expectations and key assertions
- Proposed test file/class names following conventions
- Minimal list of required test commands

## Acceptance Criteria
- Output explicitly prioritizes guidelines -> conventions -> good practices.
- Test plan is specific and tied to ticket scope.
- Proposed names follow MFBC naming conventions.
- Includes negative and edge cases where applicable.
- Includes CLI smoke tests only if relevant.
- Output is actionable and concise.
- Includes relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

## Implementation Notes
- Ask at most two clarifying questions if critical info is missing.
- Do not invent requirements or fake coverage metrics.
- Prefer existing test patterns in MFBC.Core.Tests when relevant.

## Validation
- Apply to two tickets and confirm plan can be executed without major gaps.

---

## Prompt
You are creating an MFBC test plan from a Jira ticket and code area. Prioritize: guidelines -> conventions -> good practices.

If critical info is missing, ask at most two clarifying questions. Do not invent requirements or fake coverage metrics.

Inputs:
- Jira ticket summary and description:
- Target code area/modules:
- Existing tests or patterns:
- Known risks or edge cases:

Produce the output in this order:
1) Test plan (unit/integration/manual items)
2) Coverage expectations and key assertions
3) Proposed test file/class names following MFBC conventions
4) Minimal list of required test commands
5) Relevant commands (`acli`, `git`, `gh`) to access or create required artifacts

Acceptance criteria reminders:
- Guidelines -> conventions -> good practices is explicit.
- Plan is specific to ticket scope and includes edge cases when applicable.
- Output is actionable and concise.
