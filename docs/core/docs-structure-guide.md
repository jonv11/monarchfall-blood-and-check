# Guide for Structuring Documentation under ./docs

This guide defines a scalable, agent-friendly, and developer-friendly structure for organizing Markdown documentation inside the `./docs` directory.

The objective is to ensure:
- Fast navigation for humans
- Deterministic consumption by AI agents
- Clear separation of concerns
- Long-term maintainability

---

# 1. Design Principles

## 1.1 One Topic per File
Each file must cover a single responsibility.

Good:
- `jira-workflow.md`
- `branching-strategy.md`

Avoid:
- `project-guide.md` (large mixed content)

---

## 1.2 Keep Files Small

Recommended limits:

| Type | Ideal Size | Maximum |
|---|---|---|
| Core rules | <10 KB | 20 KB |
| Standard doc | 5–30 KB | 50 KB |

If a file exceeds 50 KB, split it.

---

## 1.3 Flat Where Possible

Avoid deep nesting.

Good:
```
docs/
  jira/
  git/
  architecture/
```

Avoid:
```
docs/process/project/management/jira/
```

---

# 2. Recommended docs Structure

```
docs/
  README.md

  core/
    agent-rules.md
    documentation-conventions.md

  architecture/
    overview.md
    system-design.md
    decisions.md

  jira/
    workflow.md
    ticket-guidelines.md
    field-rules.md

  git/
    branching-strategy.md
    commit-guidelines.md
    pr-guidelines.md

  development/
    setup.md
    coding-standards.md
    testing-strategy.md

  operations/
    release-process.md
    ci-cd.md

  examples/
    ticket-examples.md
    pr-examples.md
```

---

# 3. docs/README.md (Required)

The root README must act as an index.

Example structure:

```
# Documentation Index

## Core
- Agent Rules
- Documentation Conventions

## Jira
- Workflow
- Ticket Guidelines

## Git
- Branching Strategy
- PR Guidelines
```

This file must remain under 10 KB.

---

# 4. Naming Conventions

## 4.1 File Names
Use:
- lowercase
- hyphen-separated

Examples:
- `jira-workflow.md`
- `ci-cd.md`

Avoid:
- Spaces
- CamelCase
- Version numbers

---

## 4.2 Folder Names
Use domain-based grouping:
- `jira`
- `git`
- `architecture`
- `development`
- `operations`

---

# 5. Writing Structure (Per File)

Recommended template:

```
# Title

## Purpose
## Scope
## When to Use
## Rules / Process
## Examples
## Edge Cases
## References (optional)
```

Keep critical rules near the top.

---

# 6. Agent Optimization Rules

## 6.1 Avoid Cross-Dependency Chains

Do not rely on:
- “See also other file”
- Deep reference trees

If a workflow needs multiple files, the prompt must list them explicitly.

---

## 6.2 Keep Core Rules Separate

Create:

```
docs/core/agent-rules.md
```

This file should include:
- Priority order
- Assumption rules
- Clarification behavior

Keep under 10 KB.

---

## 6.3 Avoid Mixed Audience Files

Separate:
- Human onboarding
- Agent instructions
- Examples
- Reference material

---

# 7. Documentation Layers

## Layer 1 – Core
Stable, rarely changing.

Examples:
- agent rules
- documentation standards

## Layer 2 – Process
How work is done.

Examples:
- Jira workflow
- Git process
- Release flow

## Layer 3 – Reference
Detailed rules and specifications.

Examples:
- Field definitions
- Naming rules

## Layer 4 – Examples
Concrete samples.

---

# 8. Scalability Rules

When adding new documentation:

1. Check if topic fits an existing domain
2. Create a new file if content >5 KB
3. Update `docs/README.md`
4. Avoid duplicate information
5. Link only for human navigation (not required for agents)

---

# 9. Anti-Patterns

Avoid:

### Monolithic Documentation
Large “everything” files.

### Deep Folder Trees
More than 2 levels.

### Duplicate Rules
Same content in multiple places.

### Hidden Critical Information
Important rules only inside examples or appendices.

---

# 10. Versioning and Maintenance

## 10.1 Update Strategy
When process changes:
- Update the relevant file
- Update docs/README.md if structure changed

---

## 10.2 Review Frequency
Recommended:
- Documentation review every major release
- Agent-critical files reviewed more frequently

---

# 11. Minimal Required Structure (Starter)

For small projects:

```
docs/
  README.md
  core/
    agent-rules.md
  architecture/
    overview.md
  jira/
    workflow.md
  git/
    pr-guidelines.md
```

---

# 12. Key Principles Summary

- One topic per file
- Keep files small
- Flat structure
- Clear naming
- Index everything
- Separate core/process/reference/examples
- Optimize for explicit loading by agents
