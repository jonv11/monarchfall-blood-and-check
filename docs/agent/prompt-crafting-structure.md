# Prompt Structure

## Purpose
Provide a standard prompt section order that improves clarity and reliability.

## Scope
Use for any prompt that directs an agent to perform multi-step work in MFBC.

## Recommended Structure

A well-structured prompt has these sections in order:

### 1. **Role** (1-2 sentences)
Define what the agent is doing and its responsibilities.

```
You are an AI coding agent responsible for implementing features in the MFBC 
(Monarchfall: Blood & Check) C# game engine. Your task is to write production-ready code 
following all MFBC architecture rules and coding standards.
```

### 2. **Constraints** (Bullet list)
List what the agent **must follow** and what it **must avoid**.

```
MUST:
- Write code in C# targeting .NET 8.0
- Follow all rules in .editorconfig (indentation, naming, formatting)
- Include unit tests for all logic in MFBC.Core
- Treat all warnings as errors; build must have zero warnings

MUST NOT:
- Introduce dependencies from MFBC.Cli to MFBC.Core (layering violation)
- Use reflection or dynamic code generation
- Commit directly; always push to feature branch first
- Skip writing XML documentation for public methods
```

### 3. **Inputs** (What you're providing to the agent)
State what context, files, or data the agent is working with.

```
Input:
- Ticket MFBC-24 (Jira work item with acceptance criteria)
- Codebase structure: src/MFBC.Core, src/MFBC.Cli, tests/MFBC.Core.Tests
- Reference: .editorconfig for formatting rules
- Reference: docs/architecture/overview.md for layering rules
```

### 4. **Steps** (Numbered instructions)
Break the task into sequential steps with verification points.

```
Steps:
1. Read and understand ticket acceptance criteria
2. Review existing MFBC.Core architecture (docs/architecture/overview.md)
3. Design the feature with reference to existing patterns
4. Implement in MFBC.Core, add unit tests
5. Run: dotnet build --configuration Release (verify no warnings)
6. Run: dotnet test --configuration Release (verify all tests pass)
7. Commit with message: "feat(core): <description> (MFBC-24)"
8. Push branch: git push -u origin feature/MFBC-24-<title>
9. Create PR with acceptance criteria checklist
```

### 5. **Outputs** (What the agent should produce/report)
Describe what deliverables, outputs, or reports the agent should provide.

```
Output:
- Code implementing the feature (MFBC.Core only, no Cli changes unless specified)
- Unit tests (minimum 90% coverage on new code)
- Commit message referencing MFBC-24
- PR description with acceptance criteria checklist (all items ✓ checked)
- Brief summary of what was implemented and any challenges encountered
```
