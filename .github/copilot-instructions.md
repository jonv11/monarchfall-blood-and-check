# Agent Instructions for Monarchfall: Blood & Check

This document guides AI agents working on this repository.

## Architecture Rules

- **Layering:** `MFBC.Core` is pure logic with no dependencies on CLI or presentation.
- **Direction:** `MFBC.Cli` and test projects depend on `MFBC.Core`, never the reverse.
- **New modules:** Follow the same pattern—depend on Core but remain independent of sibling modules unless architecturally justified.

## Development Standards

- **Tests required:** All new logic in `MFBC.Core` must include unit tests in `MFBC.Core.Tests`.
- **Language:** English only for identifiers, comments, and documentation.
- **Style:** Follow `.editorconfig` rules; `dotnet format` to auto-correct.
- **Warnings:** Treat warnings as errors; suppress only with documented justification in code.

## Workflow

- Use small, focused commits aligned to single features or fixes.
- Document design decisions in `docs/decisions/` as ADRs using the 0001 format.
- Update `CHANGELOG.md` for user-facing changes.
- Avoid unnecessary abstractions, temporary files, or speculative features.

## Verification

Before committing:

```bash
dotnet build         # Must succeed with no warnings
dotnet test          # Must pass
dotnet format --verify-no-changes  # Must be compliant (if format tool available)
```

## References

- Architecture: [docs/architecture/overview.md](../../docs/architecture/overview.md)
- Initial decisions: [docs/decisions/0001-project-scaffold.md](../../docs/decisions/0001-project-scaffold.md)
