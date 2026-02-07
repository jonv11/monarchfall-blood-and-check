# Anti-Hallucination Patterns

## Purpose
Provide concrete patterns that keep agent output grounded in real sources.

## Scope
Use when prompts risk ambiguous instructions, missing sources, or unverified output.

## Pattern 1: Anchor to Canonical Sources

**Bad (Hallucination Risk):**
```
Follow MFBC naming conventions. Method names should be... [describe from memory]
```

**Good (Grounded):**
```
Follow MFBC naming conventions as defined in docs/development/naming-conventions.md. 
For methods specifically, see the "Method Names" section which states:
- Public methods: PascalCase (e.g., GetThreats)
- Private methods: camelCase (e.g., validateMove)
```

## Pattern 2: Show Counter-Examples

**Bad (Ambiguous):**
```
Write good, clean code.
```

**Good (Concrete):**
```
Write code following .editorconfig rules. Examples of what NOT to do:
- ✗ Using tabs (use spaces as per .editorconfig)
- ✗ Method names like getThreats (use PascalCase)
- ✗ Unused variables (remove cruft)
- ✗ Missing XML comments on public methods

Examples of what TO do:
- ✓ Method: public List<Threat> GetThreats() { ... }
- ✓ Documented: /// <summary>Returns threats to this piece.</summary>
```

## Pattern 3: Stop and Ask Checkpoints

**Bad (Assumes agent will guess right):**
```
Implement move validation.
```

**Good (Checkpoints prevent bad assumptions):**
```
STOP AND ASK IF:
1. "Are pawns the only piece type I should implement, or all 6 pieces?"
2. "Should en passant (special pawn capture) be included, or just basic moves?"
3. "Is castling (king+rook move) part of this task, or a separate task?"

Once you clarify these questions with the user, proceed with implementation.
```

## Pattern 4: Intermediate Validation

**Bad (Agent claims success without evidence):**
```
Implement tests and make sure they pass.
```

**Good (Validates at each step):**
```
Steps:
1. Write test file: tests/MFBC.Core.Tests/MoveValidatorTests.cs
2. Add 20 test cases (pawn, knight, bishop, rook, queen, king coverage)
3. Run locally: dotnet test MFBC.Core.Tests
4. CHECK: All tests pass (green checkmarks)
5. Run: dotnet build Release (check for warnings)
6. CHECK: Build output shows "0 warnings"
7. Commit with message: "test(core): add move validator tests (MFBC-24)"
```

## Pattern 5: Explicit Verification Before Completion

**Bad (No verification):**
```
Implement feature X.
```

**Good (Explicitly checks work):**
```
After implementing, verify completion:

VERIFY each acceptance criterion:
- [ ] Pawn: forward 1, forward 2 from start, diagonal captures—all working
- [ ] Knight: L-shaped move in any direction—test 5+ positions
- [ ] All 6 pieces: manually verify each against chess rules

VERIFY quality:
- Command: dotnet build --configuration Release → "0 projects with warnings"
- Command: dotnet test --configuration Release → "X tests passed, 0 failed"
- Command: cd src/MFBC.Core && check comments on every public method

If ANY verification fails, STOP and report the issue before marking task complete.
```
