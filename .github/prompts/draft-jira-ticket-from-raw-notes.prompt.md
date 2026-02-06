# Prompt: Draft Jira Ticket From Raw Notes

## Context
You are drafting a Jira ticket for Monarchfall: Blood & Check (MFBC) from raw notes. The output must follow MFBC conventions and be ready to paste into Jira.

## Goal
Convert raw notes into a complete Jira ticket draft with a recommended issue type and proposed hierarchy, prioritizing guidelines first, conventions second, and good practices third.

## Scope (In / Out)
### In
- Transform notes into a Jira ticket draft with sections: Context, Goal, Scope (In/Out), Acceptance Criteria, Implementation Notes, Validation, Dependencies, Risks, Open Questions.
- Recommend a best-fit issue type and justify it.
- Suggest a logical parent/child/subtask structure, including reuse of an existing parent when appropriate.
- Include relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

### Out
- Final prioritization, estimation, or assignment decisions.

## Inputs
- Raw notes (bullets, chat logs, scratchpad)
- Current sprint context (goal, timeframe, active epics/features)
- Target component/module (if known)
- Deadlines or dependencies (if any)

## Output / Deliverables
- Suggested summary line
- Recommended issue type with justification
- Suggested hierarchy (parent/child/subtask) with rationale
- Jira ticket draft text with required sections

## Acceptance Criteria
- Output explicitly prioritizes guidelines -> conventions -> good practices.
- Issue type selection is explicit and justified.
- Hierarchy proposal is logical and improves organization.
- All required sections are present with concrete, testable content.
- Acceptance criteria are measurable.
- Open questions are explicit and minimal.
- Output is ready to paste into Jira (or ADF-ready).
- Includes relevant `acli`, `git`, or `gh` commands to access or create required artifacts.

## Implementation Notes
- Ask at most two clarifying questions if critical info is missing.
- If sprint context is missing, ask for it explicitly.
- Do not invent requirements; surface unknowns as questions.
- Keep output concise but complete.

## Validation
- Run the prompt on two real note sets and verify the resulting tickets pass Definition of Ready and align with sprint structure.

---

## Prompt
You are drafting a Jira ticket for MFBC from raw notes. Prioritize: guidelines -> conventions -> good practices.

If critical info is missing, ask at most two clarifying questions. If sprint context is missing, ask for it explicitly. Do not invent requirements; surface unknowns as questions.

Inputs:
- Raw notes:
- Sprint context:
- Target component/module:
- Deadlines/dependencies:

Produce the output in this order:
1) Suggested summary line
2) Recommended issue type with justification
3) Suggested hierarchy (parent/child/subtask) with rationale
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
5) Relevant commands (`acli`, `git`, `gh`) to access or create required artifacts

Acceptance criteria reminders:
- Guidelines -> conventions -> good practices is explicit.
- Acceptance criteria are measurable.
- Open questions are explicit and minimal.
- Output is ready to paste into Jira (ADF-ready).
