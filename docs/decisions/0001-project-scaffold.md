# ADR-0001: Project Scaffold and Architecture

**Date:** 2026-02-04  
**Status:** Accepted  
**Deciders:** Core team

## Context

Monarchfall: Blood & Check is a new project at the early scaffold stage. We need to establish a foundation that supports long-term growth, scales with complexity, and enforces clean separation of concerns.

## Decision

### 1. CLI-First Approach

**Decision:** Start with a command-line interface rather than a GUI.

**Rationale:**
- Fast iteration on core logic without UI overhead
- Easier to test and debug game rules
- CLI can be re-used or extended with UI later (the core stays unchanged)
- Supports rapid prototyping and design iteration

### 2. Layered Architecture (Core / CLI Separation)

**Decision:** Split logic into `MFBC.Core` (pure domain) and `MFBC.Cli` (presentation).

**Rationale:**
- Keeps game rules testable and independent of I/O
- Enforces unidirectional dependency: CLI → Core (never reverse)
- Allows future UI modules to consume Core without conflict
- Mirrors industry-standard layering (domain-driven design)

### 3. xUnit Testing Framework

**Decision:** Use xUnit for unit tests in `MFBC.Core.Tests`.

**Rationale:**
- Modern, lean, zero boilerplate
- Community standard in .NET ecosystem
- Good tool support (VS Code, Visual Studio, CI)
- Sufficient for current and foreseeable test needs

### 4. .NET 8 LTS

**Decision:** Target .NET 8 as the minimum platform.

**Rationale:**
- Latest Long-Term Support (LTS) version as of 2026
- Stability and vendor support through 2026
- Rich feature set (records, pattern matching, nullable reference types)
- Widely available in CI/CD and developer environments
- No vendor lock-in; can migrate to newer LTS when needed

### 5. Directory.Build.props for Centralized Config

**Decision:** Use `Directory.Build.props` to enforce consistent settings.

**Rationale:**
- Single source of truth for language version, nullable refs, warnings
- Scales as new projects are added
- Reduces boilerplate in individual .csproj files
- Standard .NET practice

## Consequences

- **Positive:** Clear structure, testable design, easy to onboard contributors
- **Positive:** Minimal overhead—no unused complexity
- **Risk:** Core must never reference CLI or presentation—architecture reviews needed
- **Mitigation:** CI and code review discipline; ADRs for future modules

## Alternatives Considered

- **Monorepo vs. single repo:** Single repo is simpler for a cohesive product
- **Unity/Godot from day 1:** Would delay core logic iteration
- **NUnit instead of xUnit:** Possible but xUnit is more modern and minimal
- **.NET 9 vs. 8:** .NET 8 is LTS; .NET 9 is Current and loses support sooner

## Related Decisions

- Architecture separation: see [Architecture Overview](../architecture/overview.md)
- Future modules will follow the same layering rules
