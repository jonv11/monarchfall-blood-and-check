# Task Template

## Purpose
Provide a Jira Task template and filled example.

## Scope
Use when creating Task work items in MFBC.

## Template

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

## Filled Example

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
