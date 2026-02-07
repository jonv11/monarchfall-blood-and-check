# Field Requirements by Type

## Purpose
Define required fields and field-level guidance for Jira issues.

## Scope
Applies to Epic, Feature, Task, and Subtask issue types.

## Required Fields by Issue Type

| Field | Epic | Feature | Task | Subtask |
|-------|------|---------|------|---------|
| **Summary** | ✓ Required | ✓ Required | ✓ Required | ✓ Required |
| **Description** | ✓ Required | ✓ Required | ✓ Required | Optional |
| **Acceptance Criteria** | ✓ Required | ✓ Required | ✓ Required | Optional |
| **Dependencies** | If exists | If exists | If exists | Rarely |
| **Priority** | Recommended | Recommended | Recommended | Optional |
| **Assignee** | Optional | Recommended | Recommended | Recommended |

## Field Guidelines

**Summary**
- Be specific and descriptive (not "Fix bug", but "Fix NullReferenceException in board validation")
- Include scope prefix for clarity: "Docs:", "Refactor:", "Fix:", "Feature:", etc.
- Keep under 80 characters; avoid punctuation at end

**Description**
- Start with one-sentence purpose
- Add context: Why is this work needed? What problem does it solve?
- Include relevant background or references (ticket numbers, design docs)
- Break into sections: Context, Goal, Design Notes (if applicable)

**Acceptance Criteria**
- Write criteria as testable statements (can be verified/demonstrated)
- Each criterion is a checkbox: `- [ ] Specific, measurable action`
- Minimum 2-3 criteria per Feature/Task; 1-2 per Subtask
- Include edge cases, error scenarios, and performance targets if relevant
- Reference test coverage (e.g., "Unit tests cover all edge cases")

**Dependencies**
- Link related work: "Depends on MFBC-XX", "Blocks MFBC-YY"
- Cross-reference documentation or external systems
- List prerequisite tasks that must complete first
