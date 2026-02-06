# ADR-0002: CLI Parsing and Rendering Library

**Date:** 2026-02-06  
**Status:** Accepted  
**Deciders:** Core team

## Context

MFBC needs a CLI-first interface that supports subcommands (new/show/play), consistent help output, and readable terminal rendering. The CLI should remain isolated from Core logic and should be ergonomic to extend as commands grow.

## Decision

Use **Spectre.Console.Cli** for command parsing and **Spectre.Console** for rendering.

## Rationale

- Provides a clean command model with subcommands and settings.
- Built-in help and validation.
- Rich console rendering without leaking into Core.
- Mature and widely used in .NET CLI tools.

## Consequences

- CLI project will take a dependency on Spectre.Console.* packages.
- Core remains dependency-free (BCL only).
- Future commands can follow the same Spectre.Console.Cli pattern.

## Alternatives Considered

- **System.CommandLine:** viable, but less ergonomic for rich rendering and evolving UI output.
- **Custom parsing:** rejected due to maintenance overhead.

## Related Decisions

- ADR-0001: Project Scaffold and Architecture
