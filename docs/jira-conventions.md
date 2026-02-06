# Jira Conventions & Process Guide

This guide provides a standardized approach for creating, managing, and completing work items in MFBC (Monarchfall: Blood & Check). Following these conventions ensures consistency across the project and reduces friction for both human contributors and AI agents alike.

## Table of Contents

1. [Issue Types & Hierarchy](#issue-types--hierarchy)
2. [Issue Type Reference Table](#issue-type-reference-table)
3. [Good vs. Bad Examples](#good-vs-bad-examples)
4. [Field Requirements by Type](#field-requirements-by-type)
5. [Definition of Ready](#definition-of-ready)
6. [Definition of Done](#definition-of-done)
7. [Workflow & Status Transitions](#workflow--status-transitions)
8. [Parent-Child Decomposition](#parent-child-decomposition)
9. [Common Anti-Patterns](#common-anti-patterns)
10. [Tips for Contributors](#tips-for-contributors)

---

## Issue Types & Hierarchy

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

---

## Good vs. Bad Examples

### Epic Examples

**BAD:**  
```
Title: "Add features"
Description: "We need to add more stuff to the game"
AC: "Features should be added"
```
**Problems:** Too vague, no clear scope, unmeasurable AC  
**Why it fails:** No one knows what work is included or when it's complete

**GOOD:**  
```
Title: "Implement real-time multiplayer game synchronization"
Description: "Enable multiple players to play on the same board with live updates. 
Players see each other's moves instantly and receive notifications of opponent actions."
AC:
- ✓ Multi-player game sessions can be created and joined
- ✓ Board state syncs across all connected clients < 500ms
- ✓ Player moves broadcast to opponents in real-time
- ✓ Disconnect/reconnect handled with state reconstruction
- ✓ Load testing validates 10+ players per session
```
**Why it works:** Clear scope, measurable results, architecture evident

---

### Feature Examples

**BAD:**  
```
Title: "Improve game performance"
Description: "Make the game faster"
AC: "It should be faster"
```
**Problems:** Unmeasurable, no specifics, could mean anything  
**Why it fails:** Acceptance impossible to verify

**GOOD:**  
```
Title: "Implement incremental board state updates with change deltas"
Description: "Reduce network bandwidth and client processing by sending only changed
 board state instead of entire board. Estimated bandwidth reduction: 60-70%"
AC:
- ✓ Board state represented as change deltas (before/after piece positions)
- ✓ Client applies deltas without full board refresh
- ✓ Bandwidth measurement shows 60%+ reduction vs. full state
- ✓ All existing tests pass with delta system
- ✓ Integration tests verify delta->state reconstruction accuracy
```
**Why it works:** Measurable improvement, clear implementation scope, testable

---

### Task Examples

**BAD:**  
```
Title: "Add validation"
Description: "We need validation"
AC: "Validation is added"
```
**Problems:** What validation? Where? To what input?  
**Why it fails:** Ambiguous; impossible to review or test

**GOOD:**  
```
Title: "Implement piece movement validation with move legality rules"
Description: "Add comprehensive validation layer for piece moves based on chess rules
 (e.g., pawns only move forward, knights move in L-shape). Validation runs on both 
client and server for security."
AC:
- ✓ Validate pawn: forward 1 square (or 2 from start), captures diagonal only
- ✓ Validate knight: L-shape (2+1 squares) in any direction
- ✓ Validate bishop: diagonal any distance, no piece blocking
- ✓ Validate rook: horizontal/vertical any distance, no piece blocking
- ✓ Validate queen: combination of bishop + rook rules
- ✓ Validate king: 1 square in any direction, castling when eligible
- ✓ Detect check/checkmate and prevent illegal moves
- ✓ Unit tests: 20+ test cases covering all pieces and edge cases
- ✓ All tests pass, zero build warnings
```
**Why it works:** Clear scope per piece, measurable, testable, specific enough to implement

---

### Subtask Examples

**BAD (creating Subtasks for what should be a single Task):**  
```
Task: "Implement validation"
Subtask 1: "Implement validation"
Subtask 2: "Write tests"
Subtask 3: "Fix bugs"
```
**Problems:** Subtasks repeat Task intent, too abstract  
**Why it fails:** No guidance on what validation or what tests

**GOOD (concrete Subtasks within a clear Task):**  
```
Task: "Implement piece movement validation with move legality rules"
├─ Subtask: "Design and document validation algorithm for all piece types"
├─ Subtask: "Implement core validation logic with helper functions"
├─ Subtask: "Add unit tests for each piece type (pawn, knight, bishop, rook, queen, king)"
├─ Subtask: "Implement move legality checks (pins, checks, castling)"
└─ Subtask: "Integration test with game scenarios (openings, endgame, stalemate)"
```
**Why it works:** Each Subtask is actionable, parallel-able, and together they complete the Task

---

## Field Requirements by Type

### Required Fields by Issue Type

| Field | Epic | Feature | Task | Subtask |
|-------|------|---------|------|---------|
| **Summary** | ✓ Required | ✓ Required | ✓ Required | ✓ Required |
| **Description** | ✓ Required | ✓ Required | ✓ Required | Optional |
| **Acceptance Criteria** | ✓ Required | ✓ Required | ✓ Required | Optional |
| **Dependencies** | If exists | If exists | If exists | Rarely |
| **Priority** | Recommended | Recommended | Recommended | Optional |
| **Assignee** | Optional | Recommended | Recommended | Recommended |

### Field Guidelines

**Summary**
- Be specific and descriptive (not "Fix bug", but "Fix NullReferenceException in board validation")
- Include scope prefix for clarity: "Docs:", "Refactor:", "Fix:", "Feature:", etc.
- Keep under 80 characters; avoid punctuation at end

**Description**
- Start with one-sentence purpose
- Add context: Why is this work needed? What problem does it solve?
- Include relevant background or references (ticket numbers, design docs)
- Break into sections: Context, Goal, Design Notes (if applicable)

**Acceptance Criteria**
- Write criteria as testable statements (can be verified/demonstrated)
- Each criterion is a checkbox: `- [ ] Specific, measurable action`
- Minimum 2-3 criteria per Feature/Task; 1-2 per Subtask
- Include edge cases, error scenarios, and performance targets if relevant
- Reference test coverage (e.g., "Unit tests cover all edge cases")

**Dependencies**
- Link related work: "Depends on MFBC-XX", "Blocks MFBC-YY"
- Cross-reference documentation or external systems
- List prerequisite tasks that must complete first

---

## Definition of Ready

A Task/Feature is **Ready to Start** when:

- ✓ **Acceptance Criteria are clear and measurable** – a reviewer can unambiguously verify completion
- ✓ **Dependencies are documented** – no hidden blockers or prerequisite work
- ✓ **Scope is clearly bounded** – work fits in planned timeframe (1-2 days for Task, 3-5 days for Feature)
- ✓ **Design is approved** – architectural decisions made; no "figure it out during implementation"
- ✓ **Necessary context is linked** – design docs, prior discussions, related issues referenced
- ✓ **No follow-up work ambiguity** – if this Task is done, the Feature is 90%+ complete

**Anti-Pattern:** "Figure it out as you go" – this means the issue isn't ready.

---

## Definition of Done

A Task/Feature is **Done** when:

- ✓ **All Acceptance Criteria are met** – demonstrated and tested
- ✓ **Code changes are implemented** (if applicable)
  - Follows MFBC style guide and naming conventions
  - No build warnings (treat warnings as errors per `Directory.Build.props`)
  - All public APIs have XML documentation
- ✓ **Tests are added** (if logic added)
  - Unit tests for MFBC.Core logic
  - Integration tests for workflows
  - All tests pass locally and in CI
- ✓ **Code review is complete** – PR approved by at least one team member
- ✓ **Branch is merged to main** – code is in production-ready state
- ✓ **Documentation is updated** (if applicable)
  - README, guides, or inline code comments
  - CHANGELOG.md updated with user-facing changes
- ✓ **Jira status transitioned to Done** – when PR is merged

**Anti-Pattern:** Marking as Done when PR is opened; wait until merged.

---

## Workflow & Status Transitions

MFBC uses a four-status workflow optimized for continuous work tracking:

```
To Do → In Progress → In Review → Done
  ↑                                    ↓
  └────────────────────────────────────┘ (only on reject)
```

### Status Definitions

**To Do** (Not Started)
- Work is planned but not yet started
- Acceptance Criteria are defined and ready
- Resources may not be assigned
- **Transition to In Progress** when you begin implementation

**In Progress** (Active)
- Work has begun; developer is actively implementing
- Subtasks may be created and tracked during this phase
- Expected to complete within 1-2 days (Task) or 3-5 days (Feature)
- **Transition to In Review** when implementation is complete and PR is ready

**In Review** (Waiting for Approval)
- Implementation is complete; PR is open for review
- Code is in a reviewable, testable state
- Waiting for approval from reviewer(s)
- **Transition back to In Progress** only if reviewer requests changes (rare; usually done in PR comments)
- **Transition to Done** when PR review is approved and merged

**Done** (Complete)
- PR has been merged to main
- All Acceptance Criteria verified as met
- Code is live in main branch
- No further work on this item (new issues for follow-ups)

### Transition Guidelines

| From | To | When | Notes |
|------|----|----|-------|
| **To Do** | **In Progress** | Start work | Immediate; you began implementation |
| **In Progress** | **In Review** | PR ready | Code complete, PR open for review |
| **In Review** | **Done** | PR merged | Approval given; code in main |
| **In Review** | **In Progress** | Major changes needed | Only if significant rework; minor feedback via PR comments |
| **To Do** | **Done** | Rarely | Only if work determined unnecessary (close without impl.) |

---

## Parent-Child Decomposition

### Decomposition Hierarchy Tree

```
Epic: Implement piece exchange and compensation system
│
├─ Feature: Implement piece exchange UI with live preview
│   ├─ Task: Design UI mockups and get approval
│   ├─ Task: Implement exchange dialog component
│   │   ├─ Subtask: Build form inputs (piece selection, quantity)
│   │   ├─ Subtask: Implement live preview of exchange
│   │   └─ Subtask: Add validation feedback to user
│   └─ Task: Write integration tests for exchange workflow
│
├─ Feature: Implement piece exchange API endpoint
│   ├─ Task: Design REST API contract
│   ├─ Task: Implement exchange logic and rules
│   │   ├─ Subtask: Validate exchange legality (pieces to exchange exist, types match)
│   │   ├─ Subtask: Calculate compensation based on piece values
│   │   └─ Subtask: Update board state with exchanged pieces
│   ├─ Task: Add authentication and authorization checks
│   └─ Task: Write comprehensive API tests
│
└─ Feature: Implement exchange history and analytics
    ├─ Task: Design database schema for exchange logs
    ├─ Task: Record exchange transactions
    └─ Task: Build analytics dashboard
```

### Decomposition Rules

**Rule 1: Epic → Feature**
- Each Feature delivers one user-facing or developer-facing capability
- If a Feature is not shippable independently, it's too small (merge with another Feature)
- Example: "Exchange UI" and "Exchange API" are separate; UI alone is not useful without API

**Rule 2: Feature → Task**
- Each Task is work that can be done in 1-2 days by one developer
- If a Task requires collaboration or will take 3+ days, split it
- Example: "Implement exchange API" splits into "Design API", "Implement logic", "Test", "Deploy" if needed

**Rule 3: Task → Subtask (Optional)**
- Use Subtasks only if a Task has 4+ distinct steps or parallel work
- If Task is straightforward, use Acceptance Criteria itemization instead
- Example: A "Write tests" Task might not need Subtasks; just list test cases in AC

**Rule 4: Keep Depth Shallow**
- Avoid deep nesting (Epic > Feature > Task > Subtask > Sub-subtask) – stay at 3 levels max
- Subtasks should not have any children
- If a Subtask has children, the Task wasn't properly decomposed

**Rule 5: Parent Completion**
- A parent issue is Complete when all child issues are Complete
- Do not mark Feature as Done until all Tasks are merged
- Do not mark Epic as Done until all Features are merged

---

## Common Anti-Patterns

### Anti-Pattern 1: Epic-Sized Tasks

**The Pattern:**
```
Title: "Implement cheating detection system"
AC: "Cheating is detected"
```

**Why It's a Pattern:**
- Takes 3+ weeks of work; spans multiple systems
- Breaks into 5+ distinct Features naturally
- No one knows when it's actually done

**The Fix:**
Create an Epic with 3-5 Features:
```
Epic: "Implement cheating detection system"
├─ Feature: "Detect rapid move sequences indicative of bots"
├─ Feature: "Detect position pre-planning via clustering analysis"
└─ Feature: "Implement admin dashboard to flag suspicious accounts"
```

---

### Anti-Pattern 2: Vague Acceptance Criteria

**The Pattern:**
```
Task: "Improve code quality"
AC:
- ✓ Code quality is improved
- ✓ Refactor as needed
```

**Why It's a Pattern:**
- "Improved" is unmeasurable; how much? by what metric?
- Reviewer has no way to verify completion
- Becomes a constant discussion: "Is this good enough?"

**The Fix:**
Be specific and measurable:
```
Task: "Refactor Board class to reduce cyclomatic complexity"
AC:
- ✓ Cyclomatic complexity per method < 5 (was 8-12)
- ✓ Code coverage remains ≥ 90%
- ✓ All existing tests pass unchanged
- ✓ Performance regression < 5% (measured by benchmark)
```

---

### Anti-Pattern 3: Missing Dependencies

**The Pattern:**
```
Task: "Implement user authentication in CLI"
– (No mention of backend API requirement)
```

**Why It's a Pattern:**
- Developer starts, realizes API doesn't exist yet
- Blocked for days; work is duplicated or conflicting
- No visibility into true critical path

**The Fix:**
Document dependencies explicitly:
```
Task: "Implement user authentication in CLI"
Dependencies: Depends on MFBC-18 (REST API auth endpoint)
Description: "Once MFBC-18 (API auth) is done, implement CLI login flow..."
```

---

### Anti-Pattern 4: Mixing Multiple Concerns

**The Pattern:**
```
Task: "Implement and test piece movement validation and add docs"
```

**Why It's a Pattern:**
- Too many deliverables; no singular focus
- Review becomes unclear: "Is this done? What about tests? Docs?"
- Hard to parallelize work

**The Fix:**
Split into focused Tasks:
```
Task: "Implement piece movement validation"
Task: "Write unit tests for movement validation"
Task: "Document movement rules in user guide"
```

---

### Anti-Pattern 5: Creating Subtasks for Everything

**The Pattern:**
```
Task: "Add new CLI command"
├─ Subtask: "Add new CLI command"
├─ Subtask: "Write help text"
├─ Subtask: "Test it"
```

**Why It's a Pattern:**
- Subtasks mirror the Task; no additional value
- Adds overhead without clarity
- Each Subtask is not truly independent

**The Fix:**
Use Subtasks only when they're independent steps or when Task is complex:
```
Task: "Add board analysis CLI command"

AC:
- ✓ CLI command accepts board position and outputs analysis metrics
- ✓ Metrics include: threats, capture opportunities, position evaluation
- ✓ Help text clearly describes command usage
- ✓ Unit tests cover 10+ board configurations
- ✓ No build warnings
```

Or, if Task is truly complex with parallel work:
```
Task: "Build real-time multiplayer sync system"
├─ Subtask: "Design message protocol for board updates" (design phase)
├─ Subtask: "Implement client-side sync engine" (coding phase)
├─ Subtask: "Implement server-side state reconciliation" (parallel coding)
└─ Subtask: "Write integration tests for sync scenarios" (testing phase)
```

---

### Anti-Pattern 6: No Clear Definition of Ready

**The Pattern:**
```
Task: "Fix performance"
– (No context, no measurement, no AC about what metric improves)
```

**Why It's a Pattern:**
- Developer doesn't know where to start
- "Performance" could mean anything
- Review disagreement: "Is it fast enough now?"

**The Fix:**
Define measurable, specific AC:
```
Task: "Optimize board rendering performance to < 16ms per frame"

Description: "Board currently renders in 40-60ms per frame; goal is 60 FPS (< 16.67ms frame time)"

AC:
- ✓ Board rendering < 16ms per frame (measured at 60 FPS)
- ✓ No memory leaks during 10+ minute sessions
- ✓ GPU utilization remains < 60%
- ✓ Benchmark suite added to CI
```

---

## Tips for Contributors

### For Reporters (Creating Issues)

1. **Use the Right Type**  
   Ask: "Can this be shipped independently?" → Feature  
   Ask: "Can one person complete this in 1-2 days?" → Task  
   Ask: "Does this take multiple weeks?" → Epic

2. **Write for Future You (and AI agents)**  
   Assume the person reading this has context only from the issue
   – link everything relevant

3. **Be Specific with AC**  
   Instead of: "Add comments to code"  
   Use: "Add XML documentation comments to public methods in BoardValidator class (5 methods currently missing)"

4. **Test Your Own Acceptance Criteria**  
   When you finish writing AC, ask: "Could someone else independently verify each point?" If no, rewrite.

### For Implementers (Working on Issues)

1. **Clarify Before Starting**  
   If anything is unclear, don't guess—ask in the issue or Slack
   Status Definition of Ready requires clarity

2. **Transition Status When Appropriate**  
   - Move to **In Progress** immediately when you start
   - Move to **In Review** when PR is opened (not just drafted)
   - Don't move to **Done** until merged

3. **Reference the Issue in Your PR and Commits**  
   Include "Resolves MFBC-XX" in PR body and commit messages
   This links history and helps others understand context

4. **Use Checklists for Complex Tasks**  
   Before marking In Review, print Acceptance Criteria and verify each one is met

### For Reviewers

1. **Review Against AC**  
   Check PR against each Acceptance Criterion
   If AC unclear, ask reporter/implementer to clarify before merging

2. **Check for Quality Standards**  
   - Code follows naming conventions
   - No build warnings
   - Tests included and passing
   - Documentation updated if needed

3. **Approve or Request Changes Clearly**  
   Approval = ready to merge  
   Request Changes = rework needed (give specific guidance)

---

## Additional Resources

- **[Naming Conventions & Style Guide](naming-conventions.md)** – Code and identifier standards
- **[Architecture Overview](architecture/overview.md)** – System design and layering rules
- **[CONTRIBUTING.md](../CONTRIBUTING.md)** – Development workflow and PR guidelines
- **[CLI Cheatsheet](cli-cheatsheet.md)** – Quick reference for MFBC CLI commands

---

## Version History

| Date | Version | Change |
|------|---------|--------|
| 2026-02-06 | 1.0 | Initial documentation for MFBC Jira conventions |

---

**Questions or suggestions?** Update this document with a new PR or raise a discussion in Slack.
