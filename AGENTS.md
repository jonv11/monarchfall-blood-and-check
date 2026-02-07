# AGENTS.md

Repository-wide guidance entry point for coding agents (Codex, Copilot, Claude, etc.).

## Canonical instructions (read order)

1. **.github/copilot-instructions.md** — primary, detailed repository guidance (workflow, conventions, PR expectations).
2. **docs/** — architecture, development conventions, and any project-specific specs referenced by Copilot instructions.
3. **.github/pull_request_template.md** (or **.github/PULL_REQUEST_TEMPLATE/**) — PR requirements and checklist.
4. **.github/workflows/** — CI expectations (tests, formatting, build steps) that PRs must satisfy.

If any instructions conflict, follow the list above in order (higher wins).

## Non-negotiables (apply to all agents)

- Follow repository guidance in this order: **.github/copilot-instructions.md** → **docs/** → PR templates → CI workflows. If instructions conflict, the earlier source wins.
- Produce **high-quality changes** that follow the repository’s **guidelines, conventions, and good practices** (style, architecture, naming, testing, documentation).
- If requirements are ambiguous or missing, ask targeted questions before implementing; do not invent requirements.
- Keep changes minimal and consistent with existing patterns; avoid unnecessary refactors.
- Do not delete information from documentation; prefer additive edits and preserve intent. If a removal is necessary, explain why and what replaced it.
- Ensure changes satisfy CI expectations and local checks defined in **.github/workflows/** (build/test/lint/format) and fix failures you introduce.
- When making behavior changes, update or add tests and documentation as required by the repo’s standards.

## .github inventory (what to look for)

- **.github/copilot-instructions.md**  
  Repository-wide agent instructions for GitHub Copilot and many Copilot-based workflows.

- **.github/prompts/**  
  Reusable prompt files for consistent agent workflows (e.g., audits, ticket drafting, PR review).

- **.github/workflows/**  
  CI pipelines that define the checks required for merges (build/test/lint/format).

- **.github/PULL_REQUEST_TEMPLATE.md** or **.github/PULL_REQUEST_TEMPLATE/**  
  PR template(s) that define required PR structure and validation steps.

- **.github/ISSUE_TEMPLATE/**  
  GitHub issue templates (if used) for consistent issue creation.

- **.github/CODEOWNERS**  
  Ownership/review routing rules (if present).

## Optional compatibility shims (only if you use these tools)

- **CLAUDE.md** (repo root)  
  If you use Claude Code, keep this file as a redirect to .github/copilot-instructions.md (avoid duplicating rules).

- **.cursorrules** (repo root) or **.cursor/rules/**  
  If you use Cursor, keep rules short and point to .github/copilot-instructions.md.
