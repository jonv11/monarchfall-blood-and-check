# Automated Enforcement

## Purpose
Describe the automated tooling that enforces naming and style standards.

## Scope
Applies to all contributors and CI workflows.

## Tooling

This project uses:

- **`.editorconfig`** — Enforces formatting rules (indentation, spacing, etc.)
- **`dotnet format`** — Auto-formats code to match `.editorconfig`
- **`Directory.Build.props`** — Enforces warnings as errors, nullable refs, XML docs

## Before Committing

```bash
# Format code
dotnet format

# Verify build with no warnings
dotnet build

# Run tests
dotnet test
```
