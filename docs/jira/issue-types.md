# Issue Types and Hierarchy

## Purpose
Define Jira issue types used in MFBC and how they relate in the hierarchy.

## Scope
Applies to Epic, Feature, Task, and Subtask issue types.

## Issue Types and Hierarchy

MFBC uses a four-level hierarchy to organize work:

```
Epic (Large initiative, 2-4 weeks)
  └── Feature (Deliverable component, 3-5 days)
       └── Task (Implementation unit, 1-2 days)
            └── Subtask (Work item, < 1 day)
```

### Epic

A large initiative spanning multiple weeks, representing a significant feature area or system capability.

**Purpose:** Organize major work streams and track long-term progress.

**When to use:**
- Major feature (e.g., "Piece exchange system")
- Large refactor affecting multiple modules
- Multi-week initiative

**Examples:**
- **GOOD:** "Implement piece exchange and compensation system"  
- **GOOD:** "Redesign game state architecture for performance"  
- **NOT EPIC:** "Add undo button" (too small, use Feature)

**Characteristics:**
- Spans 2-4 weeks of work
- Contains 3+ Features
- High-level, business-oriented description
- Requires acceptance criteria at strategic level

---

### Feature

A tangible, user-facing or developer-facing capability that can be shipped independently.

**Purpose:** Break epics into shippable components that deliver clear value.

**When to use:**
- User-facing capability (e.g., "Implement piece exchange UI")
- Developer tool or improvement (e.g., "Add performance profiling to CLI")
- Component or module (e.g., "Create move validation engine")

**Examples:**
- **GOOD:** "Implement move validation with piece-specific rules"
- **GOOD:** "Build Jira integration CLI commands"
- **NOT FEATURE:** "Refactor Board class" (too internal, use Task if needed)
- **NOT FEATURE:** "Fix typo in docs" (too small, use Task or inline PR)

**Characteristics:**
- Spans 3-5 days of focused work
- Contains 2-5 Tasks
- Deliver one clear capability
- Product-oriented language

---

### Task

A specific unit of implementation work, typically completed in 1-2 days.

**Purpose:** Define concrete implementation work with clear acceptance criteria.

**When to use:**
- Implementation of a feature (e.g., "Implement UI for piece exchange dialog")
- Code review/testing work
- Documentation or tooling support
- Configuration or deployment work

**Examples:**
- **GOOD:** "Add TypeScript definitions for Game State API"
- **GOOD:** "Create CLI command for board analysis"
- **GOOD:** "Docs: Write deployment guide for staging environment"
- **NOT TASK:** "Implement multiplayer networking" (too large, use Feature)
- **NOT TASK:** "Think about architecture" (too vague, be specific)

**Characteristics:**
- Span 1-2 days
- Singular focus (one implementation concern)
- Acceptance criteria are measurable and specific
- No child Tasks (Subtasks only if needed)

---

### Subtask

A small, specific work item that can be completed in under one day.

**Purpose:** Break down complex Tasks into trackable micro-steps.

**When to use:**
- Complex Task with multiple validation steps
- Parallel work (e.g., add tests, update docs, implement feature)
- When one Task has 5+ distinct steps

**Examples:**
- **GOOD (as Subtasks of "Implement piece exchange UI"):**
  - "Design UI mockup and get approval"
  - "Implement form validation logic"
  - "Add integration tests for piece exchange"
  - "Update user documentation"
- **NOT SUBTASK:** "Add logging" (should be part of Task AC, not a separate item)
- **NOT SUBTASK:** "Fix bug in unrelated component" (create separate Task instead)

**Characteristics:**
- Span < 1 day
- Part of a larger Task
- Specific and actionable
- Often parallel or sequential within a Task

---

## Issue Type Reference Table

| Type | Hierarchy | Duration | Purpose | AC Required? | Example Summary |
|------|-----------|----------|---------|--------------|-----------------|
| **Epic** | Top | 2-4 weeks | Large initiative, business objective | Yes | Implement piece exchange and compensation system |
| **Feature** | Mid-high | 3-5 days | Shippable capability, user/dev value | Yes | Build piece exchange dialog UI |
| **Task** | Mid-low | 1-2 days | Concrete implementation unit | Yes | Implement piece exchange API endpoint |
| **Subtask** | Bottom | < 1 day | Micro-step within Task | Optional | Write unit tests for exchange validation |
