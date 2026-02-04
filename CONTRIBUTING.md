# Contributing to Monarchfall: Blood & Check

Thank you for your interest in contributing to **Monarchfall: Blood & Check**! This document provides guidelines for contributing to the project.

## Getting Started

1. **Fork and clone** the repository
2. **Build the project:** `dotnet build`
3. **Run tests:** `dotnet test`
4. **Explore the docs:** See [`docs/architecture/`](docs/architecture/) and [`docs/decisions/`](docs/decisions/)

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
- Run `dotnet format` before committing (if available)
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
- Ensure CI checks pass (build + test)

## Architecture Rules

**Critical:** Respect the layering model:

- `MFBC.Core` is **pure logic** — no dependencies on CLI or presentation
- `MFBC.Cli` depends on `MFBC.Core`, never the reverse
- Test projects depend on `MFBC.Core`

See [Architecture Overview](docs/architecture/overview.md) for details.

## Code Standards

- **Language:** English only for identifiers, comments, and documentation
- **Warnings:** Treat warnings as errors (enforced by `Directory.Build.props`)
- **Nullable:** Nullable reference types are enabled project-wide
- **Documentation:** Public APIs should have XML doc comments

## Decision Making

For architectural or design decisions:

1. Create an ADR (Architectural Decision Record) in [`docs/decisions/`](docs/decisions/)
2. Use the format from [`0001-project-scaffold.md`](docs/decisions/0001-project-scaffold.md)
3. Include context, decision, rationale, and consequences

## Questions or Help?

- **Issues:** Open an issue on GitHub for bugs or feature requests
- **Jira:** Check the board for planned work and backlog
- **Agent guidance:** See [`.github/copilot-instructions.md`](.github/copilot-instructions.md) for AI agent conventions

## License

By contributing, you agree that your contributions will be licensed under the MIT License.
