# Monarchfall: Blood & Check

[![codecov](https://codecov.io/github/jonv11/monarchfall-blood-and-check/graph/badge.svg?token=OL2BRLKZZ8)](https://codecov.io/github/jonv11/monarchfall-blood-and-check)

A chess roguelite engine with CLI-first architecture. Procedurally generated chessboards, dynamic rules, and permadeath progression—exploring the convergence of chess mechanics and roguelike design.

**Current Status:** Scaffold only. Initial project structure and build infrastructure ready for development.

## Quick Start

For detailed setup instructions, see [Local Development Setup Guide](docs/development/local-setup.md).

```bash
# Build the project
dotnet build

# Run the CLI
dotnet run --project src/MFBC.Cli

# Run tests
dotnet test
```

## Repository Structure

- **`src/MFBC.Core/`** — Core game logic and domain models (pure, testable)
- **`src/MFBC.Cli/`** — CLI interface and presentation layer
- **`tests/MFBC.Core.Tests/`** — Unit tests for core logic
- **`docs/architecture/`** — Architecture decisions and system design
- **`docs/decisions/`** — Architectural decision records (ADRs)

## Development

- New logic requires tests in `MFBC.Core.Tests`
- Layering rule: `MFBC.Core` must remain independent; `MFBC.Cli` depends on `MFBC.Core`
- Architectural decisions go in [`docs/decisions/`](docs/decisions/) — see [ADR guide](docs/decisions/README.md)
- Design decisions use the [ADR process](docs/decisions/README.md) and [template](docs/decisions/TEMPLATE.md)
- English-only identifiers and comments

See `.github/copilot-instructions.md` for agent guidance.

For AI agents working on MFBC, see [AI Prompt Crafting Guidelines](docs/agent/prompt-crafting-guide.md) for best practices on crafting high-quality prompts with grounding, verification checkpoints, and hallucination prevention.

## Roadmap

Core infrastructure is in place. Next phases:
- Rule engine and piece movement logic
- Procedural board generation
- Game state management and permadeath tracking
- UI/visualization (beyond CLI)

For architectural context, see [architecture overview](docs/architecture/overview.md) and [decision records](docs/decisions/).

## Technical Foundation

Before implementation, comprehensive technical decisions have been documented:

- [Core Domain Model](docs/architecture/core-domain-model.md) — Board, tiles, pieces, and invariants
- [Core ↔ CLI Contract](docs/architecture/core-cli-contract.md) — API surface and responsibilities
- [Move Representation](docs/architecture/move-representation.md) — Action model and validation flow
- [Rule Engine Design](docs/architecture/rule-engine-design.md) — Phased execution and rule categories
- [Events, Effects, and Mutation](docs/architecture/events-effects-mutation.md) — State change model
- [RNG and Determinism](docs/architecture/rng-determinism-replay.md) — Seed strategy and replay
- [Serialization](docs/architecture/serialization-save-load.md) — Save/load and versioning
- [Testing Strategy](docs/development/testing-strategy.md) — Test categories and determinism guarantees
- [Dependency Boundaries](docs/architecture/dependency-boundaries.md) — Layering enforcement

## Contributing

Contributions are welcome! See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

**Jira Conventions:** See [Jira Conventions & Process Guide](docs/jira/jira-conventions.md) for issue types, workflow, and best practices for creating and managing work items.

**Jira Templates:** See [Jira Work Item Templates](docs/jira/jira-templates.md) for copy-paste-ready templates (Epic, Feature, Task, Subtask) with filled examples.

**Jira Board:** https://jonv11.atlassian.net/jira/software/projects/MFBC/boards/1

### Security

See [Dependency Updates](docs/operations/dependency-updates.md) for information about dependency updates and vulnerability handling.

## License

MIT — See [LICENSE](LICENSE) for details.
