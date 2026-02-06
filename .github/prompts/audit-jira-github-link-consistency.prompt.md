---
name: audit-jira-github-link-consistency
description: Audit and propose fixes for Jira <-> GitHub link consistency.
argument-hint: key="MFBC-###" prs="..." commits="..."
---

You are auditing Jira <-> GitHub link consistency for Monarchfall: Blood & Check (MFBC).

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.
Follow MFBC linking conventions (e.g., use Resolves MFBC-### in PR bodies and commit messages).

If critical info is missing, ask at most two clarifying questions. Do not invent links; require explicit IDs/URLs.

Be interactive and action-oriented:
- Collect ticket and PR context via `acli` and `gh` when keys are provided.
- Produce a concise link audit report and proposed fixes.
- Do not apply changes automatically; provide an action plan for review and confirmation.

Inputs (use ${input:...} variables):
- Jira ticket key: ${input:key}
- Related PRs/issues (numbers or URLs): ${input:prs}
- Branch/commit references (if any): ${input:commits}

Interactive flow (follow in order):
1) Fetch ticket context: `acli jira workitem view ${input:key}`
2) Fetch PR context as needed: `gh pr view <PR> --json title,body,url`
3) Audit missing or inconsistent links.
4) Show a compact preview (missing links + proposed fixes).
5) Provide a step-by-step action plan for review.

Produce the output in this order:
1) Link audit report (missing/incorrect links)
2) Proposed updates to Jira, PR descriptions, and commit messages
3) Guidance for consistent linking going forward
4) Action plan (exact `acli`/`gh`/`git` commands to run after confirmation)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- All suggested links are valid and traceable.
- Report is concise and actionable.
- Output can be pasted as a Jira/PR comment.
