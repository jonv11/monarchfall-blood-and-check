# Jira Work Item Templates

This document provides copy-paste-ready templates for creating work items in MFBC Jira. Each template includes placeholders, inline guidance, linking instructions, and a filled example demonstrating realistic usage.

**How to use these templates:**
1. Choose the appropriate template for your work item type
2. Copy the template text
3. Create a new issue in Jira
4. Paste the template content into the Description field
5. Fill in each section, following the guidance and examples
6. Reference the [Jira Conventions & Process Guide](jira-conventions.md) for detailed guidance

---

## Epic Template

### Template

```
## Context
[Describe the business or strategic motivation. What problem in MFBC are you solving?
Why is this a priority now? What triggered this epic? Who benefits?]

## Goal
[State the high-level objective in 1-2 sentences. What capability or system will exist 
after this epic is complete?]

## Scope

### In Scope
- [Major deliverable or capability area 1]
- [Major deliverable or capability area 2]
- [Major deliverable or capability area 3]
[Add more as needed. Keep at high level—these become Features.]

### Out of Scope
- [What will NOT be included in this epic, and why?]
- [What's deferred for future work?]

## Deliverables
- [Major system or feature component 1]
- [Major system or feature component 2]
- [Document or specification, if applicable]

## Acceptance Criteria
As an Epic, acceptance criteria focus on strategic outcomes. You'll measure success 
via child Features and their acceptance criteria.

- [ ] All Features (child items) are merged to main and tested
- [ ] Performance target: [specific metric, e.g., "board render < 16ms"]
- [ ] Documentation (ADR, architecture doc, or user guide) is complete and linked
- [ ] No critical bugs or warnings in the merged code
- [ ] Stakeholder review/approval obtained (if applicable)

## Dependencies / References
- Related Epic: [MFBC-XX, if part of larger initiative]
- Design Doc: [link]
- Related Tickets: [MFBC-XX, MFBC-YY]
- External: [docs, tools, or external systems]

## Risks / Edge Cases
- **Risk 1:** [Description] → *Mitigation:* [What will you do about it?]
- **Risk 2:** [Description] → *Mitigation:* [What will you do about it?]

## Assumptions
- [We assume property/constraint 1 about the system]
- [We assume property/constraint 2 about the system]
- [We assume team/resource assumption]
```

### Filled Example

```
## Context
MFBC board rendering performance degrades as the board scales beyond 16x16 squares. 
Currently, full board redraws take 40-60ms (at 60 FPS target of 16.67ms per frame). 
This causes frame drops, poor perceived responsiveness, and limits gameplay on lower-end devices. 
Players report lag spikes when moving pieces or updating threats. This is impacting 
gameplay experience and limiting expansion to mobile platforms.

## Goal
Optimize the rendering pipeline to achieve consistent 60 FPS performance across all 
supported board sizes (8x8 to 32x32) and devices, unlocking smooth gameplay and mobile expansion.

## Scope

### In Scope
- Implement incremental tile updates (only re-render changed tiles, not full board)
- Optimize threat calculation to run asynchronously without blocking UI
- Add GPU-accelerated rendering for tile graphics
- Profile and optimize hot-path code (board traversal, threat computation)
- Build performance benchmarking suite for regression testing

### Out of Scope
- WebGL or advanced graphics; stick to 2D canvas for now
- Multiplayer synchronization (separate epic)
- Animation improvements (future phase)

## Deliverables
- Performance-optimized rendering engine (incremental updates)
- Async threat computation system
- Benchmark suite with 10+ board configurations
- Architecture decision record (ADR) documenting optimization strategy
- Performance regression tests in CI

## Acceptance Criteria
- [ ] All performance-related Features are merged and tested on CI
- [ ] Benchmark suite passes; render time < 16ms for 16x16+ boards
- [ ] Frame rate maintains 60 FPS during gameplay on target devices
- [ ] ADR documents optimization decisions and is linked in main README
- [ ] Performance comparison document shows before/after metrics
- [ ] No critical bugs; all existing tests pass

## Dependencies / References
- Design Doc: Performance optimization strategy (to be created in MFBC-26)
- Related: MFBC-20 (board architecture), MFBC-21 (threat calculation)
- Tools: Chrome DevTools for profiling, Lighthouse for benchmarking

## Risks / Edge Cases
- **Risk:** Custom rendering code introduces subtle graphics bugs
  → *Mitigation:* Extensive visual regression tests; comparison against reference renders
- **Risk:** Async threat calculation causes stale data display
  → *Mitigation:* Queue system with invalidation; user can't move pieces during compute

## Assumptions
- We'll continue using Canvas 2D (not migrate to WebGL during this epic)
- Target device is 2020+ specification (not ancient phones)
- Performance baseline will be established first (MFBC-26 profiling task)
```

---

## Feature Template

### Template

```
## Context
[Describe why this feature is needed. What user or developer problem does it solve? 
How does it relate to the parent Epic (if any)?]

## Goal
[State the feature objective in 1-2 sentences. What will users be able to do after this?]

## Scope

### In Scope
- [Specific capability 1]
- [Specific capability 2]
- [UI/UX component or behavioral requirement]

### Out of Scope
- [What's intentionally NOT included]
- [What's deferred]

## Deliverables
- [Shipped code/component]
- [Tests (unit or integration)]
- [Documentation]

## Acceptance Criteria
Format: Use a checklist for testable, itemized criteria.
Each criterion should be verifiable by code review, test execution, or demo.

- [ ] [Specific capability 1 is implemented and tested]
- [ ] [Specific capability 2 is implemented and tested]
- [ ] [Integration point or workflow works end-to-end]
- [ ] [Edge case or error scenario is handled]
- [ ] [Documentation is updated]
- [ ] [No build warnings; all tests pass]

## Dependencies / References
- Parent Epic: [MFBC-XX]
- Depends on: [MFBC-YY (must complete first)]
- Related: [MFBC-ZZ (worth reviewing together)]
- Design: [link to design doc, Figma, or architecture note]

## Risks / Edge Cases
- **Risk 1:** [Description] → *Mitigation:* [Plan to address]
- **Edge Case 1:** [Scenario] → *Handling:* [How will we deal with it?]

## Assumptions
- [System assumption or dependency]
- [Team/resource assumption]
```

### Filled Example

```
## Context
Users want to practice board analysis without playing full games (permadeath can 
be frustrating). AI agent contributors need a way to evaluate board positions 
objectively (materiel, threats, opportunities). Currently, no analysis output exists 
in the CLI. The parent Epic is "Build board analysis and inspection toolkit" (MFBC-22).

## Goal
Build a CLI command that accepts a board position and outputs quantitative analysis 
metrics, enabling users to learn and AI agents to evaluate positions.

## Scope

### In Scope
- CLI command: `mfbc analyze --position <fen> --metrics all`
- Output metrics: material count (piece value sum), threats (pieces under attack), 
  capture opportunities (undefended pieces), position evaluation (basic heuristic)
- Configuration: allow filtering metrics (e.g., `--metrics threats,material`)
- Error handling: validate FEN input; provide helpful error messages

### Out of Scope
- Stockfish integration or engine-level analysis (future)
- Historical comparison ("this is worse than game X")
- Endgame tablebases
- Visualization (CLI output only)

## Deliverables
- CLI command handler in MFBC.Cli
- Board analysis engine (pure logic) in MFBC.Core
- Integration tests for analysis workflows
- Documentation in CLI cheatsheet with examples

## Acceptance Criteria
- [ ] CLI command accepts `--position` (FEN string) and `--metrics` (comma-separated list)
- [ ] Computes and outputs: material count, threats, opportunities, evaluation score
- [ ] Invalid FEN input rejected with helpful error message
- [ ] All Analysis metrics are consistent with manual verification (spot-check 5+ positions)
- [ ] Integration tests cover: valid FEN, invalid FEN, all metrics output, partial metric filtering
- [ ] CLI cheatsheet updated with command syntax and 3+ examples
- [ ] No build warnings; all tests pass

## Dependencies / References
- Parent Epic: MFBC-22 (Board analysis toolkit)
- Depends on: MFBC-17 (Board state model—must be stable)
- Related: MFBC-23 (threat detection)
- Design: Board analysis metrics specification (to be created)

## Risks / Edge Cases
- **Risk:** Performance—analyzing complex positions takes too long
  → *Mitigation:* Benchmark analysis on 10x10+ boards; cache threat calculations
- **Edge Case:** Illegal board position in FEN (e.g., opposing kings adjacent)
  → *Handling:* Detect and reject with error; don't attempt analysis on invalid boards
- **Edge Case:** FEN doesn't include move history; analysis assumes material is only concern
  → *Handling:* Document limitation; note that analysis doesn't account for positional subtlety

## Assumptions
- FEN (Forsyth-Edwards Notation) is the standard input format for board positions
- Team wants quantitative metrics, not narrative analysis ("this position is won")
- CLI users have basic understanding of chess terminology (threats, material)
```

---

## Task Template

### Template

```
## Context
[Why is this task necessary? What is being built or fixed? Who requested it?]

## Goal
[Specific outcome in 1-2 sentences. What will be true after this task is done?]

## Scope

### In Scope
- [Specific implementation 1]
- [Specific implementation 2]

### Out of Scope
- [Explicitly out of scope to avoid scope creep]

## Deliverables
- [Code changes/new file]
- [Tests added]
- [Documentation updates]

## Acceptance Criteria
Format: Checklist of specific, testable criteria. Each should answer: 
"How will I know this is done?" Keep criteria focused on behavior/output, not effort.

- [ ] [Specific implementation detail is coded and tested]
- [ ] [Edge case or error is handled]
- [ ] [Code follows MFBC style guide; no build warnings]
- [ ] [Tests added; 90%+ coverage on new code]
- [ ] [Documentation or comments explain usage]

## Dependencies / References
- Parent: [MFBC-XX (Feature this task belongs to, if any)]
- Depends on: [MFBC-YY must complete first]
- Related: [MFBC-ZZ for context]
- Reference: [design doc, prior discussion]

## Risks / Edge Cases
- **Edge Case:** [Scenario] → *Handling:* [Your plan]

## Assumptions
- [Assumption about system state or requirements]
```

### Filled Example

```
## Context
MFBC.Core move validation currently doesn't distinguish between piece types. 
All pieces are treated identically, which is incorrect—pawns move differently than knights. 
Parent Feature (MFBC-19) requires full move validation by piece type. This task 
implements the core validation logic that the CLI will call.

## Goal
Implement piece-specific move validation in MFBC.Core so that each piece type 
(pawn, knight, bishop, rook, queen, king) validates moves according to chess rules.

## Scope

### In Scope
- Core validation logic for all 6 piece types
- Check/checkmate detection (detect attacks on king)
- Illegal move rejection (move validation returns true/false)
- Castling detection (when eligible, permit king+rook coordinated move)
- Pinned piece detection (piece cannot move if it exposes king)

### Out of Scope
- En passant (advanced pawn capture—defer to follow-up)
- Fifty-move draw rule or threefold repetition (outside scope)
- Stalemate (game-end logic; belongs in game state manager)

## Deliverables
- `MoveValidator.cs` class in MFBC.Core with public methods per piece type
- Unit tests: 50+ test cases covering all pieces and edge cases
- XML documentation on all public methods
- Architecture decision (ADR) if new patterns introduced

## Acceptance Criteria
- [ ] Pawn: moves 1 square forward (2 from start), captures diagonally
- [ ] Knight: L-shaped move (2+1 squares), ignores blocking pieces
- [ ] Bishop: diagonal movement any distance, blocked by pieces
- [ ] Rook: horizontal/vertical any distance, blocked by pieces
- [ ] Queen: combination of bishop + rook rules
- [ ] King: 1 square any direction; castling when eligible
- [ ] Check detection: identify if king is under attack after move
- [ ] Pinned piece detection: prevent moves that expose king to check
- [ ] Move validation rejects illegal moves and returns reason
- [ ] Unit tests: 50+ cases; 95%+ code coverage
- [ ] No build warnings; all tests pass; code reviewed

## Dependencies / References
- Parent: MFBC-19 (Move validation feature)
- Related: MFBC-17 (Board state model—already stable)
- Reference: Chess rules documentation (describe in code comment)

## Risks / Edge Cases
- **Edge Case:** Castling when king or rook has moved before
  → *Handling:* Track move history; require both pieces in start positions and path clear
- **Edge Case:** Move leaves king in check (en route during castling)
  → *Handling:* Validate intermediate squares during king movement
- **Risk:** Pinned piece logic is subtle and easy to implement incorrectly
  → *Mitigation:* Extensive tests (10+ scenarios); pair review with chess expert

## Assumptions
- Board state is represented as MFBC.Core.Board (stable API per MFBC-17)
- Move legality (this task) is separate from game rules (other tasks check checkmate, stalemate)
- This is server-side validation; UI may do client-side hints but trusts server
```

---

## Subtask Template

### Template

```
## Context
[Brief explanation of why this subtask exists. How does it fit into the parent Task?]

## Goal
[Specific micro-outcome. What single thing will be done?]

## Acceptance Criteria
Format: 2-4 criteria, focused and specific. These should be < 30 minutes to verify.

- [ ] [Specific criterion]
- [ ] [Verification method, if non-obvious]

## Notes / Guidance
[Optional guidance for implementer: edge cases, tips, or gotchas specific to this subtask]
```

### Filled Example

```
## Context
Parent Task: "Implement piece validation" (MFBC-30) has multiple workstreams. 
This subtask handles one piece type (pawn) validation logic in isolation, allowing 
parallel work while the knight/bishop subtasks are done by others.

## Goal
Code pawn movement validation for all cases: forward 1 square, forward 2 from start, 
diagonal captures. Include test cases.

## Acceptance Criteria
- [ ] `ValidatePawnMove()` method is implemented in MoveValidator.cs
- [ ] Forward 1 square, 2 from start, and diagonal capture cases all work correctly
- [ ] 8 test cases added covering: single move, double move from rank 2, blockade, capture, invalid moves
- [ ] Method has XML documentation comment
- [ ] All tests pass locally

## Notes / Guidance
- Pawn moves are direction-aware (white pawns up, black pawns down)—watch the sign
- Test both white and black; don't assume single-direction logic covers both
- Rank numbering: rank 1 = white start, rank 8 = black start (case sensitivity matters!)
- Consider blocked square: pawn can't move forward if occupied by any piece
```

---

## Linking Related Items

### Parent-Child Links

When you create child items (Feature under Epic, Task under Feature), Jira will link them automatically. No manual action needed.

**To link:**
1. Open the parent issue (Epic or Feature)
2. Click "Link child" or in Description, note child issues: `- [ ] MFBC-XX (Feature: ...)`
3. Create new issues as children of the parent
4. Verify parent-child relationship is set correctly

### Dependencies

In the "Depends on" section, list tickets that **must complete before this one can start**.

```
## Dependencies / References
Depends on: MFBC-17 (Board model must be stable)
Blocks: MFBC-20, MFBC-21 (these features depend on this task)
```

### Related Links

In the "Related" section, list tickets that are **relevant but not blocking**:
- Design documents
- Related features working in parallel
- Prior work this task extends

### References in Description

Reference other work inline:
```
This implements the strategy outlined in MFBC-18. See also the design doc at [link].
Complements MFBC-22 (board analysis) and MFBC-23 (threat detection).
```

### PR Links

When creating a PR, include "Resolves MFBC-XX" in the PR description:
```
## Description
Implements pawn movement validation for MFBC-30.

## Resolves
Resolves MFBC-30
```

Jira will automatically link the PR to the ticket once merged.

---

## Quick Reference

| Type | Duration | Parent | Children | Template | Example |
|------|----------|--------|----------|----------|---------|
| **Epic** | 2-4 weeks | None | Features (3+) | [Epic Template](#epic-template) | [Board Rendering Optimization](#filled-example) |
| **Feature** | 3-5 days | Epic | Tasks (2-5) | [Feature Template](#feature-template) | [Board Analysis CLI](#filled-example-1) |
| **Task** | 1-2 days | Feature | Subtasks (optional) | [Task Template](#task-template) | [Pawn Validation Logic](#filled-example-2) |
| **Subtask** | < 1 day | Task | None | [Subtask Template](#subtask-template) | [Pawn Movement Implementation](#filled-example-3) |

---

## Tips for Using Templates

1. **Fill in every section.** Sections like Dependencies, Risks, and Assumptions are easy to skip, but they prevent surprises later.

2. **Copy-paste exactly once per issue.** Don't reuse the same issue multiple times; create a new one.

3. **Link your design docs/references.**  If you reference an external doc or prior discussion, add the link. Future you will thank you.

4. **Acceptance Criteria must be testable.** If you write "UI should be intuitive," rewrite it as "user can perform action X in < 2 clicks."

5. **Use the examples as inspiration.** They're realistic MFBC scenarios; adapt them to your work, don't copy verbatim.

6. **Keep scope sections as boundaries.** "Out of Scope" is just as important as "In Scope"—it prevents scope creep and manages expectations.

---

## Additional Resources

- **[Jira Conventions & Process Guide](jira-conventions.md)** – Full guide on issue types, workflow, and decomposition
- **[CONTRIBUTING.md](../CONTRIBUTING.md)** – Development workflow for implementing work
- **[Naming Conventions](naming-conventions.md)** – Code and identifier standards
