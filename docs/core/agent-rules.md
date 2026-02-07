# Agent Rules

## Purpose
Define mandatory rules and priority order for AI agents working in this repository.

## Scope
Applies to all agent-driven tasks affecting code or documentation in MFBC.

## Priority Order

1. Task prompt instructions
2. Repository instructions (.github/copilot-instructions.md and prompt files)
3. Documentation under /docs
4. General best practices

## Mandatory Rules

- **Layering:** MFBC.Core is pure logic with no dependencies on CLI or presentation.
- **Direction:** MFBC.Cli and test projects depend on MFBC.Core, never the reverse.
- **New modules:** Depend on Core; remain independent of sibling modules unless justified.
- **Tests required:** New logic in MFBC.Core must include unit tests in MFBC.Core.Tests.
- **Language:** English only for identifiers, comments, and documentation.
- **Style:** Follow `.editorconfig`; use `dotnet format` to auto-correct.
- **Warnings:** Treat warnings as errors; suppress only with documented justification.
- **ADRs:** Document design decisions in docs/decisions/ using 0001 format.
- **Changelog:** Update CHANGELOG.md for user-facing changes.
- **Avoid churn:** Do not add unnecessary abstractions, temporary files, or speculative features.

## Clarification and Assumptions

- If required information is missing or ambiguous, ask the user.
- Do not invent requirements, data, or behaviors not supported by repo context.
- If instructions conflict, follow the priority order and ask if unresolved.

## Verification

Before committing:

```bash
dotnet build         # Must succeed with no warnings
dotnet test          # Must pass
dotnet format --verify-no-changes  # Must be compliant (if format tool available)
```
