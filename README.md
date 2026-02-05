# Monarchfall: Blood & Check

[![codecov](https://codecov.io/gh/jonv11/monarchfall-blood-and-check/branch/main/graph/badge.svg)](https://codecov.io/gh/jonv11/monarchfall-blood-and-check)

A chess roguelite engine with CLI-first architecture. Procedurally generated chessboards, dynamic rules, and permadeath progression—exploring the convergence of chess mechanics and roguelike design.

**Current Status:** Scaffold only. Initial project structure and build infrastructure ready for development.

## Quick Start

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
- Design decisions go in `docs/decisions/` with ADR format
- English-only identifiers and comments

See `.github/copilot-instructions.md` for agent guidance.

## Roadmap

Core infrastructure is in place. Next phases:
- Rule engine and piece movement logic
- Procedural board generation
- Game state management and permadeath tracking
- UI/visualization (beyond CLI)

For architectural context, see [architecture overview](docs/architecture/overview.md) and [decision records](docs/decisions/).

## Contributing

Contributions are welcome! See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

**Jira Board:** https://jonv11.atlassian.net/jira/software/projects/MFBC/boards/1

## License

MIT — See [LICENSE](LICENSE) for details.
