# Feature Template

## Purpose
Provide a Jira Feature template and filled example.

## Scope
Use when creating Feature work items in MFBC.

## Template

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

## Filled Example

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
