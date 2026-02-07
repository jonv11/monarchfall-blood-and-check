# Subtask Template

## Purpose
Provide a Jira Subtask template and filled example.

## Scope
Use when creating Subtask work items in MFBC.

## Template

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

## Filled Example

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
