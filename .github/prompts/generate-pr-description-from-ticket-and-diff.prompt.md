---
name: generate-pr-description-from-ticket-and-diff
description: Draft a PR title/body from a Jira ticket and diff aligned to the repo template.
argument-hint: key="MFBC-###" diff="..." tests="..." risks="..."
---

You are generating a GitHub pull request description for Monarchfall: Blood & Check (MFBC).

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.
Use the PR template at [.github/PULL_REQUEST_TEMPLATE.md](../PULL_REQUEST_TEMPLATE.md).

If critical info is missing, ask at most two clarifying questions. Do not invent tests or results.

Be interactive and action-oriented:
- Fetch the Jira ticket via `acli` when a key is provided.
- Summarize changes from the diff or change summary.
- Before updating a PR description, show a compact preview and ask for confirmation.
- After confirmation, apply the update and report results.

Inputs (use ${input:...} variables):
- Jira ticket key: ${input:key}
- Diff or change summary: ${input:diff}
- Tests run and results: ${input:tests}
- Known risks/follow-ups: ${input:risks}

Interactive flow (follow in order):
1) Fetch the ticket: `acli jira workitem view ${input:key}`
2) Draft the PR title and body aligned to the PR template.
3) Show a compact preview (title + Summary + Testing).
4) Ask for confirmation to update the PR body.
5) On confirmation, run the action and report the result.

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
3) Action plan (exact `acli`/`git`/`gh` commands to run after confirmation)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- Jira reference is included in title/body.
- Testing is explicit and accurate.
- Output is ready to paste into GitHub PR.
