# Prompt: Generate Subtasks From Parent Ticket

## Context
You are decomposing a parent Jira ticket for Monarchfall: Blood & Check (MFBC) into actionable subtasks. The output must follow MFBC conventions and be ready to convert into Jira subtasks.

## Goal
Analyze a parent ticket and produce a logical, guideline-first subtask breakdown aligned to MFBC conventions, prioritizing guidelines -> conventions -> good practices.

## Scope (In / Out)
### In
- Produce subtasks with clear scope, dependencies, and suggested sequencing.
- Provide concise acceptance criteria and validation for each subtask.
- Indicate when a tiny item should remain as a checklist note in the parent.
- Include relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

### Out
- Assignment, estimation, or implementation decisions.

## Inputs
- Parent ticket summary and description
- Current sprint context (if relevant)
- Known dependencies or linked issues

## Output / Deliverables
- List of proposed subtasks with short titles
- For each subtask: purpose, scope (In/Out), acceptance criteria, validation
- Suggested ordering and dependency notes
- Notes on items best kept as parent checklist entries

## Acceptance Criteria
- Output explicitly prioritizes guidelines -> conventions -> good practices.
- Subtasks are distinct, non-overlapping, and measurable.
- Each subtask has clear acceptance criteria and validation.
- Dependencies are called out when applicable.
- Output is ready to convert to Jira subtasks.
- Includes relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

## Implementation Notes
- Ask at most two clarifying questions if critical info is missing.
- Do not invent work not implied by the parent.
- Keep the list concise and pragmatic.

## Validation
- Apply to a real parent ticket and confirm subtasks cover 100% of scope without duplication.

---

## Prompt
You are decomposing an MFBC parent Jira ticket into subtasks. Prioritize: guidelines -> conventions -> good practices.

If critical info is missing, ask at most two clarifying questions. Do not invent work not implied by the parent.

Inputs:
- Parent ticket summary and description:
- Sprint context (if relevant):
- Known dependencies/linked issues:

Produce the output in this order:
1) Proposed subtask list (short titles)
2) For each subtask: purpose, scope (In/Out), acceptance criteria, validation
3) Suggested ordering and dependency notes
4) Items that should remain as a checklist in the parent
5) Relevant commands (`acli`, `git`, `gh`) to access or create required artifacts

Acceptance criteria reminders:
- Guidelines -> conventions -> good practices is explicit.
- Subtasks are distinct and measurable.
- Dependencies are explicit when applicable.
