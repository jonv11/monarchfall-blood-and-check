# Quick Reference and Usage Tips

## Purpose
Provide a compact reference for template selection and best practices.

## Scope
Use for quick selection and checklist reminders when drafting Jira issues.

## Quick Reference

| Type | Duration | Parent | Children | Template | Example |
|------|----------|--------|----------|----------|---------|
| **Epic** | 2-4 weeks | None | Features (3+) | Epic Template | Board Rendering Optimization |
| **Feature** | 3-5 days | Epic | Tasks (2-5) | Feature Template | Board Analysis CLI |
| **Task** | 1-2 days | Feature | Subtasks (optional) | Task Template | Pawn Validation Logic |
| **Subtask** | < 1 day | Task | None | Subtask Template | Pawn Movement Implementation |

## Tips for Using Templates

1. **Fill in every section.** Sections like Dependencies, Risks, and Assumptions are easy to skip, but they prevent surprises later.

2. **Copy-paste exactly once per issue.** Don't reuse the same issue multiple times; create a new one.

3. **Link your design docs/references.** If you reference an external doc or prior discussion, add the link. Future you will thank you.

4. **Acceptance Criteria must be testable.** If you write "UI should be intuitive," rewrite it as "user can perform action X in < 2 clicks."

5. **Use the examples as inspiration.** They're realistic MFBC scenarios; adapt them to your work, don't copy verbatim.

6. **Keep scope sections as boundaries.** "Out of Scope" is just as important as "In Scope"—it prevents scope creep and manages expectations.
