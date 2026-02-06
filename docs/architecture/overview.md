# Architecture Overview

## Layering

The project follows a clean layering model:

```
┌─────────────────────────────┐
│   MFBC.Cli (Presentation)   │
│   - CLI commands and output │
└──────────────┬──────────────┘
               │
               ▼
┌─────────────────────────────┐
│   MFBC.Core (Logic)         │
│   - Domain models           │
│   - Game rules and state    │
│   - Pure, testable logic    │
└─────────────────────────────┘

┌─────────────────────────────┐
│   MFBC.Core.Tests (Validation)
│   - xUnit test suite        │
└─────────────────────────────┘
```

**Constraint:** `MFBC.Core` is dependency-free and never imports from `MFBC.Cli`.

## Core Module (`MFBC.Core`)

Holds all domain logic:
- Board representation and state
- Piece definitions and movement rules
- Game state machine and permadeath tracking
- Procedural generation algorithms
- Rule engine for roguelike mutations

## CLI Module (`MFBC.Cli`)

Consumes `MFBC.Core` to:
- Accept commands from the user
- Format and display output
- Orchestrate game flows

## Test Strategy

- **Unit tests** exercise `MFBC.Core` classes in isolation
- **Integration tests** verify layering and dependency wiring
- Tests are fast and deterministic (no I/O or external state)

## Non-Goals (For Now)

- No UI framework (GUI/web) until core mechanics are stable
- No engine integration (Godot, Unity, Unreal) at this stage
- No multiplayer or networking
- No asset pipeline

## Technical Design Documents

Detailed technical decisions guiding implementation:

- [Core Domain Model](../core-domain-model.md) — Sparse board, tiles, pieces, and core invariants
- [Core ↔ CLI Contract](../core-cli-contract.md) — API surface, structured data contract
- [Move Representation](../move-representation.md) — Action model, validation, and special moves
- [Rule Engine Design](../rule-engine-design.md) — Phased pipeline, rule categories, deterministic execution
- [Events, Effects, and Mutation](../events-effects-mutation.md) — Event-driven state changes
- [RNG and Determinism](../rng-determinism-replay.md) — Named RNG streams, replay strategy
- [Serialization](../serialization-save-load.md) — Save format, versioning policy
- [Testing Strategy](../testing-strategy.md) — Golden runs, replay tests, coverage requirements
- [Dependency Boundaries](../dependency-boundaries.md) — BCL-only Core, layering enforcement

## Future Modules (Placeholder)

As the project grows:
- `MFBC.RuleSet` — Serializable rule definitions and mutation engine
- `MFBC.ProcGen` — Procedural content generation (boards, encounters)
- `MFBC.Ai` — AI opponent implementation (heuristics, ML-ready structure)
- `MFBC.UI` — Graphics/UI layer (if moving beyond CLI)

Each would follow the same layering constraint: they depend on `MFBC.Core` but not on each other (unless architecturally justified).

## Build and Test

- Solution-level `dotnet build` ensures all projects compile
- Solution-level `dotnet test` runs the full test suite
- CI validates build integrity on every push and PR

See [Project Scaffold ADR](../decisions/0001-project-scaffold.md) for rationale.
