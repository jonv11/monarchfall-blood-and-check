# Dependency Boundaries and Layering Rules

## Context

MFBC uses a layered architecture with Core as the pure domain logic and CLI as the presentation layer. Dependency boundaries must be enforced to maintain testability, portability, and separation of concerns.

## Decisions

### 1. Core Dependencies

MFBC.Core uses BCL only (no NuGet packages), with optional exceptions:

- System.Text.Json (BCL stack) for lightweight serialization types
- Microsoft.Extensions.Logging.Abstractions only if a clean logging interface is required (often unnecessary if relying on events)

Rationale: Core must remain pure, portable, deterministic, and testable. Any IO, serialization, or adapter logic must live outside Core or behind interfaces.

### 2. CLI Dependencies

CLI may depend on ergonomic tooling, including:

- Command parsing: System.CommandLine (or Spectre.Console.Cli)
- Rendering/UI: Spectre.Console (tables, colors, layouts)
- Serialization for save/load: System.Text.Json (or Newtonsoft.Json if required, but prefer one serializer across the solution)

Rule: CLI may do IO; Core must not.

### 3. Layering Enforcement

Use a layered approach with automation:

Project reference rules (baseline):

- MFBC.Core references nothing else in the solution
- MFBC.Cli references MFBC.Core
- Tests reference MFBC.Core (and possibly CLI only for CLI tests)

CI enforcement:

- Build fails if forbidden references or packages are introduced in Core

Static enforcement (choose one):

- Roslyn analyzer (recommended): enforce "no forbidden namespaces" in Core (System.IO, System.Net, Spectre.*, etc.)
- ArchUnitNET or NetArchTest.Rules in tests to assert architecture constraints

Minimal high-value setup:

- Add a small "architecture tests" suite that asserts:
  - Core does not reference banned assemblies
  - Core does not use IO, network, threading, or time sources directly (unless explicitly allowed)
  - CLI is the only layer allowed to call console APIs

Code review remains important but should not be the only guardrail.

## Rationale

- BCL-only keeps Core portable and deterministic.
- Automated enforcement scales and prevents accidental violations.
- CLI is free to use presentation tooling without contaminating Core.

## Alternatives Considered

- Allowing all NuGet packages in Core: rejected due to risk of coupling and non-determinism.
- Code review only for enforcement: rejected as insufficient at scale.
- No static enforcement: rejected because violations are easy to introduce accidentally.

## Consequences / Constraints

- Core must remain dependency-free (except optional BCL stack).
- Any new dependency in Core requires explicit justification and architecture review.
- Architecture tests must be updated if new modules are added.

## Open Questions

- None in this document.
