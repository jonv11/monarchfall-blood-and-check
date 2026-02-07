# Good vs. Bad Jira Examples

## Purpose
Provide concrete examples of good and bad Jira work items.

## Scope
Applies to Epic, Feature, Task, and Subtask issue types.

## Epic Examples

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

## Feature Examples

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

## Task Examples

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

## Subtask Examples

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
