# Contributing to Monarchfall: Blood & Check

Thank you for your interest in contributing to **Monarchfall: Blood & Check**! This document provides guidelines for contributing to the project.

## Getting Started

1. **Fork and clone** the repository
2. **See [Local Development Setup Guide](docs/local-setup.md)** for platform-specific setup
3. **Build the project:** `dotnet build`
4. **Run tests:** `dotnet test`
5. **Explore the docs:** See [`docs/architecture/`](docs/architecture/) and [`docs/decisions/`](docs/decisions/)

## Project Management

We use Jira to track work, features, and bugs:

**Jira Board:** https://jonv11.atlassian.net/jira/software/projects/MFBC/boards/1

- Check the board for available issues
- New contributors: look for issues tagged as "good first issue" or "help wanted"
- Before starting work, comment on the issue to avoid duplicate efforts

## Development Workflow

### 1. Create a Branch

Branch from `main` using descriptive names:

```bash
git checkout -b feature/MFBC-123-add-board-generation
git checkout -b fix/MFBC-456-movement-bug
```

**Branch naming convention:** `type/MFBC-###-short-description`

- `feature/` — New functionality
- `fix/` — Bug fixes
- `refactor/` — Code restructuring
- `docs/` — Documentation updates
- `chore/` — Maintenance tasks

### 2. Make Changes

- Follow the `.editorconfig` style rules
- **Format code before committing:** Run `dotnet format` to auto-correct formatting
  - The CI pipeline will verify formatting with `dotnet format --verify-no-changes`
  - If formatting check fails in CI, run `dotnet format` locally and commit the changes
- Keep commits small and focused
- Write descriptive commit messages:
  - `feat(core): add piece movement validation (MFBC-123)`
  - `fix(cli): correct board rendering glitch (MFBC-456)`
  - `docs: update architecture decision for rules engine`

### 3. Write Tests

- **All new logic in `MFBC.Core` requires unit tests**
- Tests go in `MFBC.Core.Tests`
- Ensure `dotnet test` passes before submitting

### 4. Submit a Pull Request

- Push your branch to your fork
- Open a PR against `main`
- Reference the Jira issue: `Resolves MFBC-123` or `Fixes MFBC-456`
- Provide a clear description of changes
- Ensure all CI checks pass:
  - **Code formatting:** `dotnet format --verify-no-changes`
  - **Build:** `dotnet build --configuration Release` with no warnings
  - **Tests:** `dotnet test --configuration Release` passes
- If formatting check fails, run `dotnet format` locally and push the changes

## Architecture Rules

**Critical:** Respect the layering model:

- `MFBC.Core` is **pure logic** — no dependencies on CLI or presentation
- `MFBC.Cli` depends on `MFBC.Core`, never the reverse
- Test projects depend on `MFBC.Core`

See [Architecture Overview](docs/architecture/overview.md) for details.

## Code Standards

- **Naming & Style:** Follow the [Naming Conventions & Style Guide](docs/naming-conventions.md)
- **Language:** English only for identifiers, comments, and documentation
- **Warnings:** Treat warnings as errors (enforced by `Directory.Build.props`)
- **Nullable:** Nullable reference types are enabled project-wide
- **Documentation:** Public APIs should have XML doc comments
- **Formatting:** Run `dotnet format` before committing

## Coverage Policy

- **Minimum coverage target:** 80% overall coverage for the default branch
- **PR gating:** Coverage status checks are **hard-fail** for PRs targeting `main`
- Coverage is reported via Codecov in CI and must pass before merge

## Decision Making

For architectural or design decisions:

1. Read [Architectural Decision Records guide](docs/decisions/README.md) to understand when and how to create ADRs
2. Use the [ADR template](docs/decisions/TEMPLATE.md)
3. File as `docs/decisions/NNNN-kebab-case-title.md` (increment the sequence number)
4. Include: context, decision, rationale, consequences, alternatives considered
5. Set status to `Proposed` and submit for review
6. Update the [decision index](docs/decisions/README.md) once accepted

**ADR Status Values:**
- `Proposed` — Draft, open for discussion
- `Accepted` — Approved and guides current development
- `Deprecated` — No longer valid, but kept for context
- `Superseded` — Replaced by another ADR (reference the new one)

## Questions or Help?

- **Issues:** Open an issue on GitHub for bugs or feature requests
- **Jira:** Check the board for planned work and backlog
- **Agent guidance:** See [`.github/copilot-instructions.md`](.github/copilot-instructions.md) for AI agent conventions

## License

By contributing, you agree that your contributions will be licensed under the MIT License.
