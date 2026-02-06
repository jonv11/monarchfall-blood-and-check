---
name: ci-failure-triage-summary-next-actions
description: Summarize CI failures and propose next actions aligned to MFBC conventions.
argument-hint: runId="..." pr="..." logs="..." changes="..."
---

You are triaging a CI failure for Monarchfall: Blood & Check (MFBC).

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.

If critical info is missing, ask at most two clarifying questions. Do not invent log details.

Be interactive and action-oriented:
- Pull CI details using `gh` when a run ID or PR is provided.
- Summarize failures, root causes, and next actions.
- Before posting a Jira/PR comment, show a compact preview and ask for confirmation.
- After confirmation, execute the update and report results.

Inputs (use ${input:...} variables):
- CI run ID or URL: ${input:runId}
- PR number or URL: ${input:pr}
- Failure logs or summary (if already available): ${input:logs}
- Recent changes list (if available): ${input:changes}

Interactive flow (follow in order):
1) If run ID/PR is provided, fetch details:
   - `gh run view ${input:runId} --log`
   - `gh pr view ${input:pr} --json title,headRefName,url`
2) Extract the top failure signals and error lines.
3) Draft the triage summary and next actions.
4) Show a compact preview (summary + top root causes + top actions).
5) Ask for confirmation to post the comment.
6) On confirmation, run the action and report the result.

Produce the output in this order:
1) Failure summary (plain language)
2) Probable root causes (ranked, tied to evidence)
3) Next action checklist
4) Re-run vs fix decision guideline
5) Suggested ticket/PR updates (if needed)
6) Action plan (exact `gh`/`acli` commands to run after confirmation)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- Summary is concise and accurate.
- Root causes are plausible and tied to evidence.
- Next actions are specific and actionable.
- Output can be posted as a Jira/PR comment.
