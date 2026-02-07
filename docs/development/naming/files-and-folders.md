# Files and Folders

## Purpose
Define file naming and folder structure conventions for MFBC.

## Scope
Applies to source and test projects in this repository.

## File Naming

- **One class per file:** `ClassName.cs`
- **Match the class name exactly** (case-sensitive)
- Use PascalCase for file names

**Examples:**
```
GameState.cs
MovementValidator.cs
Position.cs
```

## Folder Structure

- Match namespace hierarchy
- Use PascalCase for folder names

**Example structure:**
```
src/
  MFBC.Core/
    Board/
      BoardState.cs
      Position.cs
    Pieces/
      Piece.cs
      PieceType.cs
    Rules/
      RuleEngine.cs
tests/
  MFBC.Core.Tests/
    Board/
      BoardStateTests.cs
      PositionTests.cs
```

## Test Files

- Test files mirror the source structure
- Suffix with `Tests`: `ClassNameTests.cs`
