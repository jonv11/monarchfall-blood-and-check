---
name: draft-release-notes-entry-from-merged-prs
description: Create release note entries from merged PRs aligned to CHANGELOG.md.
argument-hint: prs="..." tickets="..." changes="..."
---

You are drafting release notes for Monarchfall: Blood & Check (MFBC) from merged PRs.

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.
Align output to [CHANGELOG.md](../../CHANGELOG.md) sections and tone.

If critical info is missing, ask at most two clarifying questions. Do not invent changes beyond PR summaries.

Be interactive and action-oriented:
- Gather merged PR details via `gh` when PR numbers are provided.
- Draft concise, user-facing entries grouped by CHANGELOG sections.
- Before updating CHANGELOG.md, show a compact preview and ask for confirmation.
- After confirmation, update the file and report results.

Inputs (use ${input:...} variables):
- Merged PR list (numbers or URLs): ${input:prs}
- Associated Jira tickets (if any): ${input:tickets}
- Notable changes (features/fixes/breaking changes): ${input:changes}

Interactive flow (follow in order):
1) If PR numbers are provided, fetch summaries:
   - `gh pr view <PR> --json title,body,mergeCommit`
2) Draft release note entries grouped by CHANGELOG sections.
3) Highlight any breaking changes.
4) Show a compact preview (section headings + entries).
5) Ask for confirmation to update CHANGELOG.md.
6) On confirmation, apply updates and report the result.

Produce the output in this order:
1) Release note entries grouped by CHANGELOG sections (Added/Fixed/Infrastructure/etc.)
2) Jira ticket references if required
3) Breaking change warnings (if any)
4) Action plan (exact `gh`/`git` commands and files to update after confirmation)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- Entries are concise, user-facing, and accurate.
- Breaking changes are highlighted.
- Output is ready for a release note section.
