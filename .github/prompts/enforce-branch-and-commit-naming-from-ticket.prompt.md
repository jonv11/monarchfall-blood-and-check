---
name: enforce-branch-and-commit-naming-from-ticket
description: Derive branch and commit naming from a Jira ticket using MFBC conventions.
argument-hint: key="MFBC-###" workType="docs|feat|fix|chore" 
---

You are deriving branch and commit naming guidance from a Jira ticket for Monarchfall: Blood & Check (MFBC).

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.
Use the naming guide at [docs/naming-conventions.md](../../docs/naming-conventions.md).

If key data is missing, ask at most one clarifying question. Do not invent scope beyond the ticket summary.

Be interactive and action-oriented:
- Fetch the ticket via `acli` when a key is provided.
- Propose the branch name and commit template.
- Before creating a branch, show a compact preview and ask for confirmation.
- After confirmation, run the git commands and report results.

Inputs (use ${input:...} variables):
- Jira ticket key: ${input:key}
- Work type (feature/bug/chore/docs/etc.): ${input:workType}

Interactive flow (follow in order):
1) Fetch the ticket: `acli jira workitem view ${input:key}`
2) Draft branch name and commit template.
3) Show a compact preview (branch name + commit template).
4) Ask for confirmation to create the branch.
5) On confirmation, run the git commands and report the result.

Produce the output in this order:
1) Recommended branch name
2) Commit message template
3) Commit granularity guidance tied to ticket scope
4) Action plan (exact `acli`/`git` commands to run after confirmation)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- Ticket key is included in branch and commit guidance.
- Naming is consistent with repo conventions.
- Output is immediately usable.
