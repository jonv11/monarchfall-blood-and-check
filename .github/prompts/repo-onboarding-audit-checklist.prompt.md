---
name: repo-onboarding-audit-checklist
description: Produce an MFBC onboarding audit checklist aligned to repo docs.
argument-hint: role="..." os="..." env="..."
---

You are producing a repo onboarding audit checklist for Monarchfall: Blood & Check (MFBC).

Follow MFBC standards in this priority order: guidelines -> conventions -> good practices.
Prefer existing docs as source of truth: [CONTRIBUTING.md](../../CONTRIBUTING.md), [docs/local-setup.md](../../docs/local-setup.md), [docs/architecture/overview.md](../../docs/architecture/overview.md).

If critical info is missing, ask at most two clarifying questions. Do not invent tools or versions not documented.

Be interactive and action-oriented:
- Review the current repo structure and docs.
- Draft a checklist that references canonical docs.
- Before posting or updating documentation, show a compact preview and ask for confirmation.
- After confirmation, apply updates or post a comment and report results.

Inputs (use ${input:...} variables):
- Contributor role (if known): ${input:role}
- OS/dev environment assumptions: ${input:os}
- Environment notes or constraints: ${input:env}

Interactive flow (follow in order):
1) Review onboarding-related docs in the repo.
2) Draft the onboarding checklist and conventions summary.
3) Show a compact preview (top setup steps + key conventions).
4) Ask for confirmation to post or update documentation.
5) On confirmation, apply the action and report results.

Produce the output in this order:
1) Onboarding checklist with setup steps
2) Key conventions summary
3) Required tools and versions (from documented sources only)
4) Common pitfalls and how to avoid them
5) Optional first-PR walkthrough (if it improves clarity)
6) Action plan (exact `git`/`gh` commands to access or create required artifacts)

Acceptance criteria reminders:
- Explicitly state the guidelines -> conventions -> good practices priority.
- Checklist is complete, concise, and actionable.
- References canonical repo docs where applicable.
- Output is usable as a quick-start guide.
