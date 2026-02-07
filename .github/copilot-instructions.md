# Repository Agent Instructions for Monarchfall: Blood & Check (Central)

This is the primary repository guidance for all agents working in MFBC, not only Copilot-specific workflows.

## Instruction Precedence

Use this order and resolve conflicts by taking the higher item:

1. `.github/copilot-instructions.md` (this file)
2. `docs/`
3. `.github/PULL_REQUEST_TEMPLATE.md` (or `.github/PULL_REQUEST_TEMPLATE/`)
4. `.github/workflows/`

This order is intentionally aligned with `AGENTS.md`.

## Professional Standards (Non-Negotiable)

- Follow repository conventions and good practices for every task.
- Deliver production-quality changes with clear intent, minimal scope, and no speculative additions.
- Do not invent requirements when unclear; ask targeted questions.
- Keep architecture boundaries, naming, style, testing, and docs quality consistent with existing standards.
- Preserve documentation intent; avoid destructive rewrites unless required and justified.

## Architecture Rules

- `MFBC.Core` is pure domain logic and must not depend on CLI/presentation layers.
- Dependency direction is inward to Core: `MFBC.Cli` and tests may depend on `MFBC.Core`; never the reverse.
- New modules must respect the same boundary model unless an explicit architecture decision says otherwise.

See:
- `docs/architecture/overview.md`
- `docs/architecture/dependency-boundaries.md`
- `docs/architecture/core-cli-contract.md`

## Engineering and Documentation Standards

- English only for identifiers, comments, docs, branch names, and commit messages.
- Follow `.editorconfig` and repository naming/style guides.
- Keep warnings at zero; suppressions require explicit justification.
- All behavior changes must include corresponding tests and documentation updates when applicable.
- All new logic in `MFBC.Core` must have tests in `tests/MFBC.Core.Tests`.
- Record significant architectural decisions as ADRs in `docs/decisions/` when scope requires it.

See:
- `docs/development/naming-conventions.md`
- `docs/development/testing-strategy.md`
- `docs/core/docs-structure-guide.md`

## Workflow Expectations

- Start from current `main` for new work unless instructed otherwise.
- Use focused branches and focused commits tied to a single ticket/scope.
- Use commit format: `<type>(<scope>): <summary> (MFBC-###)` when a Jira key exists.
- Keep pull requests concise, evidence-based, and complete against acceptance criteria.

For Jira/Git/GitHub flow:
- `docs/cli/workflows.md`
- `docs/cli/acli-commands.md`
- `docs/cli/git-commands.md`
- `docs/cli/gh-commands.md`

## PR and CI Quality Gates

Before opening or merging a PR, satisfy repository checks defined in workflows:

```bash
dotnet restore MFBC.sln
dotnet format MFBC.sln --verify-no-changes --verbosity diagnostic
dotnet build MFBC.sln --no-restore --configuration Release
dotnet test MFBC.sln --no-build --configuration Release
```

PRs must comply with:
- `.github/PULL_REQUEST_TEMPLATE.md`
- `.github/workflows/ci.yml`

## Practical Guardrails

- Prefer minimal, additive changes over broad refactors.
- Do not add temporary artifacts to tracked files unless explicitly requested.
- Do not change unrelated files while addressing a ticket.
- If local behavior conflicts with documented behavior, document the gap and align changes to the approved contract or ask for direction.

## Quick References

- Architecture index: `docs/architecture/overview.md`
- Jira conventions: `docs/jira/jira-conventions.md`
- Contribution rules: `CONTRIBUTING.md`
