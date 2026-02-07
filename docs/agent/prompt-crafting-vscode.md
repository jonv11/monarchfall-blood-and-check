# VS Code Prompt File Conventions

## Purpose
Define the structure and style rules for VS Code prompt files used in this repository.

## Scope
Applies to `.copilot-instructions.md`, `.github/copilot-instructions.md`, and prompt files under `.github/prompts`.

## File Structure

VS Code prompt files (`.copilot-instructions.md`, `.github/copilot-instructions.md`) have this structure:

```markdown
# Agent Instructions for [Project Name]

## Overview
[1-3 sentence summary of project and agent purpose]

## Architecture Rules
[Key architectural constraints that agents must follow]

## Development Standards
[Code style, testing, documentation requirements]

## Workflow
[Standard steps for implementing features]

## Verification
[Commands agents should run to validate work]

## References
[Links to key documentation]
```

## Front Matter & Variable Interpolation

**VS Code provides variables in prompt file context:**
- `${workspaceFolder}` – Workspace root path
- `${file}` – Current file being edited
- `${selectedText}` – Selected text in editor

**Use in prompts like:**
```
The project is in ${workspaceFolder}. When referencing files, use paths relative 
to the project root (e.g., src/MFBC.Core/Board.cs).

Current file: ${file}. If modifying this file, ensure changes are compatible 
with its existing structure and imports.
```

## Context File Referencing

Reference project files and documentation directly:

```
The architecture rules are in docs/architecture/overview.md. Pay special attention 
to the "Layering" section: MFBC.Core must have no dependencies on MFBC.Cli.

Code style is defined in .editorconfig. Before committing, run: dotnet format
```

## Instruction Style

**Use imperative, clear language:**
- ✓ "Write unit tests for all public methods"
- ✗ "You should probably write tests if you feel like it"

**Avoid assumptions about agent knowledge:**
- ✓ "Use C# XML documentation: /// <summary>...</summary>"
- ✗ "Add docs (agents know how)"

**Include "Why" context:**
```
MUST: Include unit tests in MFBC.Core.Tests for all new logic.
WHY: MFBC enforces comprehensive test coverage; builds fail without 90%+ coverage.
HOW: Create a test class in tests/MFBC.Core.Tests/, use XUnit framework, 
     follow naming: [Feature]Tests.cs
```
