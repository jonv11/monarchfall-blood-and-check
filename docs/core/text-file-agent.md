# Guidelines for Writing Agent-Optimized Text Files

This document defines best practices for creating documentation, prompts, and text content intended to be consumed by AI coding agents (e.g., VS Code Copilot Agents, CLI agents, automation agents).

The goal is **predictability, reliability, and deterministic behavior**.

---

# 1. Core Principles

## 1.1 Be Explicit
Agents do not infer structure reliably.

Always:
- State instructions clearly
- Avoid ambiguity
- Avoid implicit assumptions
- Prefer rules over explanations

Good:
- "Use snake_case for file names."

Bad:
- "File names should follow conventions."

---

## 1.2 Keep Files Small

Recommended size limits:

| File Type | Ideal Size | Maximum |
|---|---|---|
| Prompt file | 2–8 KB | 15 KB |
| Core rules | <10 KB | 20 KB |
| Documentation module | 5–30 KB | 50 KB |

If a file exceeds **50 KB**, split it.

Agents may:
- Read only the beginning
- Skip sections
- Ignore rules at the end

---

## 1.3 Modular Structure

Instead of one large file:

```
docs/
  jira/
    conventions.md
    workflow.md
    fields.md
```

Load only what is needed.

---

# 2. Writing Style for Agents

## 2.1 Use Structured Sections

Always structure content:

```
# Purpose
# Scope
# Rules
# Examples
# Exceptions
```

Agents rely on headings to navigate.

---

## 2.2 Prefer Lists Over Paragraphs

Good:
- One rule per bullet

Avoid long paragraphs.

---

## 2.3 Put Critical Rules First

Agents prioritize early content.

Structure:

1. Mandatory rules
2. Constraints
3. Examples
4. Additional information

---

# 3. Deterministic Instruction Patterns

## 3.1 Explicit Loading Instructions

When used in prompts:

```
Before answering:
Read:
- /docs/jira/conventions.md
- /docs/git/workflow.md

Do not rely on linked files.
```

Do not rely on:
- "See also"
- Cross references

---

## 3.2 Control Assumptions

Include:

```
If required information is missing:
- Ask the user
- Do not guess
```

---

## 3.3 Define Priority

When multiple rule sources exist:

```
Priority order:
1. Task prompt
2. Repository rules
3. General best practices
```

---

# 4. Link Handling

Agents do NOT reliably follow links.

Avoid relying on:
- Relative references
- External URLs
- Nested documentation chains

If a file is required, list it explicitly.

---

# 5. Example Structure (Recommended)

```
docs/
  core/
    agent-rules.md
  jira/
    conventions.md
    field-rules.md
  git/
    branching.md
    pr-guidelines.md
```

Workflow prompt:

```
Read:
- /docs/core/agent-rules.md
- /docs/jira/conventions.md
- /docs/git/branching.md
```

---

# 6. Content Design for Reliability

## 6.1 Avoid

- Long narrative text
- Marketing language
- Philosophy or background
- Duplicate information
- Hidden constraints in examples

---

## 6.2 Include

- Clear rules
- Input → Output expectations
- Formatting requirements
- Edge cases
- Validation criteria

---

# 7. Token Efficiency

Agents operate within context limits.

Optimize by:
- Removing redundancy
- Avoiding repetition
- Using concise wording
- Splitting large examples

---

# 8. Agent Safety Controls

Recommended section to include in core rules:

```
If uncertain:
- Ask for clarification

Do not:
- Invent missing data
- Assume unspecified conventions
- Ignore explicit rules
```

---

# 9. Validation Checklist

Before adding a file for agent use:

- File size < 30 KB
- Clear headings
- No long paragraphs
- Critical rules at top
- No dependency on links
- Explicit instructions
- No ambiguity

---

# 10. Anti‑Patterns

Avoid:

### Large Monolithic Docs
100 KB architecture files.

### Deep References
File A → File B → File C.

### Implicit Context
"Follow repository standards" (without defining them).

### Mixed Audiences
Do not mix:
- Human onboarding
- Agent instructions

Create separate files if needed.

---

# 11. Recommended Core File

Create:

```
/docs/core/agent-rules.md
```

Keep it under **10 KB** and include:
- Decision rules
- Priority order
- Assumption control
- Clarification behavior

---

# 12. Key Rule

If a file is **critical for agent behavior**, it must be:
- Small
- Explicit
- Loaded directly in the prompt

Never rely on discovery.

---

# 13. Summary

For reliable agent behavior:

- Small files
- Modular structure
- Explicit loading
- Structured rules
- Minimal ambiguity
- No implicit context
