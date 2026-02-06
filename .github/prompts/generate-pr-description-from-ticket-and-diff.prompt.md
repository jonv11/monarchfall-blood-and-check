# Prompt: Generate PR Description From Ticket + Diff

## Context
You are generating a GitHub pull request description for Monarchfall: Blood & Check (MFBC). The output must align to the repository PR template and be ready to paste into GitHub.

## Goal
Create a PR description and suggested title from a Jira ticket and diff, prioritizing guidelines -> conventions -> good practices.

## Scope (In / Out)
### In
- Summarize changes and rationale.
- Link the Jira ticket and include a Resolves line.
- Highlight risks and follow-ups when relevant.
- List validation/testing performed.
- Include checklist completion guidance based on actual work performed.

### Out
- Creating or merging the PR.

## Inputs
- Jira ticket key, summary, and description
- Diff or change summary
- Tests run and results
- Known risks or follow-ups

## Output / Deliverables
- Suggested PR title
- PR description matching .github/PULL_REQUEST_TEMPLATE.md sections
- Checklist completion guidance based on actual work performed

## Acceptance Criteria
- Output explicitly prioritizes guidelines -> conventions -> good practices.
- Includes Jira reference in title/body (Resolves MFBC-###).
- Clearly states what changed and why.
- Testing section is explicit and accurate.
- Output is ready to paste into GitHub PR.

## Implementation Notes
- Do not invent tests or results.
- Keep output concise and review-friendly.
- Ask at most two clarifying questions if critical info is missing.

## Validation
- Apply to two real PRs and confirm reviewers can understand scope in under two minutes.

---

## Prompt
You are generating an MFBC PR description from a Jira ticket and a diff. Prioritize: guidelines -> conventions -> good practices.

If critical info is missing, ask at most two clarifying questions. Do not invent tests or results.

Inputs:
- Jira ticket key, summary, description:
- Diff or change summary:
- Tests run and results:
- Known risks/follow-ups:

Produce the output in this order:
1) Suggested PR title (include MFBC-###)
2) PR description that matches the repo template sections:
   - Summary
   - Type of Change
   - Related Jira Issue (Resolves: MFBC-###)
   - Changes
   - Testing
   - Checklist (guidance on which items to check based on actual work)
   - Breaking Changes
   - Additional Notes
   - Screenshots or Output (if applicable)
3) Any minimal follow-up questions (if needed)

Acceptance criteria reminders:
- Guidelines -> conventions -> good practices is explicit.
- Jira reference is included in title/body.
- Testing is explicit and accurate.
