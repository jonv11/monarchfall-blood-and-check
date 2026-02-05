# Naming Conventions & Style Guide

This document defines the naming and style conventions for **Monarchfall: Blood & Check** to ensure consistent, readable, and maintainable code across all contributors.

## Table of Contents

- [General Principles](#general-principles)
- [C# Naming Conventions](#c-naming-conventions)
- [Namespaces](#namespaces)
- [Files and Folders](#files-and-folders)
- [Test Naming](#test-naming)
- [Code Style](#code-style)

---

## General Principles

1. **Clarity over brevity:** Names should be self-explanatory
2. **English only:** All identifiers, comments, and documentation in English
3. **Consistency:** Follow established patterns throughout the codebase
4. **Standard C# conventions:** Align with [Microsoft's C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

---

## C# Naming Conventions

### PascalCase

Use **PascalCase** for:

- **Classes and structs**
  ```csharp
  public class GameState { }
  public struct Position { }
  ```

- **Interfaces** (prefix with `I`)
  ```csharp
  public interface IMovementValidator { }
  public interface IRuleEngine { }
  ```

- **Enums and enum members**
  ```csharp
  public enum PieceType
  {
      Pawn,
      Rook,
      Knight,
      Bishop,
      Queen,
      King
  }
  ```

- **Properties**
  ```csharp
  public string PlayerName { get; set; }
  public int TurnCount { get; private set; }
  ```

- **Methods**
  ```csharp
  public void ApplyMove(Move move) { }
  public bool IsValidPosition(Position pos) { }
  ```

- **Constants**
  ```csharp
  public const int MaxBoardSize = 8;
  private const string DefaultVersion = "1.0.0";
  ```

- **Public fields** (avoid when possible; prefer properties)
  ```csharp
  public readonly int BoardWidth = 8;
  ```

### camelCase

Use **camelCase** for:

- **Private fields** (prefix with underscore `_`)
  ```csharp
  private readonly IMovementValidator _validator;
  private int _turnCount;
  ```

- **Local variables**
  ```csharp
  var currentPlayer = GetActivePlayer();
  int moveCount = 0;
  ```

- **Method parameters**
  ```csharp
  public void MovePiece(Position from, Position to, PieceType pieceType)
  {
      // ...
  }
  ```

### Special Cases

- **Acronyms:** Treat as words, not all-caps
  ```csharp
  // Good
  public class XmlParser { }
  public string GetHtmlContent() { }
  
  // Avoid
  public class XMLParser { }
  public string GetHTMLContent() { }
  ```

- **Two-letter acronyms:** Keep uppercase
  ```csharp
  public class IOHelper { }
  public string UITheme { get; set; }
  ```

---

## Namespaces

### Structure

Namespaces follow the folder structure and use PascalCase:

```
MFBC.Core
MFBC.Core.Board
MFBC.Core.Rules
MFBC.Core.Pieces
MFBC.Cli
MFBC.Cli.Commands
```

### Naming Pattern

```
<Project>.<Module>[.<SubModule>]
```

**Examples:**
```csharp
namespace MFBC.Core;
namespace MFBC.Core.Movement;
namespace MFBC.Core.Pieces;
namespace MFBC.Cli.Commands;
```

### Guidelines

- Keep nesting shallow (max 3-4 levels)
- Use singular nouns unless the namespace represents a collection concept
- Avoid generic names like `Utilities` or `Helpers`

---

## Files and Folders

### File Naming

- **One class per file:** `ClassName.cs`
- **Match the class name exactly** (case-sensitive)
- Use PascalCase for file names

**Examples:**
```
GameState.cs
MovementValidator.cs
Position.cs
```

### Folder Structure

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

### Test Files

- Test files mirror the source structure
- Suffix with `Tests`: `ClassNameTests.cs`

---

## Test Naming

### Test Class Naming

```csharp
// Pattern: <ClassUnderTest>Tests
public class GameStateTests { }
public class MovementValidatorTests { }
```

### Test Method Naming

Use the **MethodName_Scenario_ExpectedBehavior** pattern:

```csharp
[Fact]
public void MovePiece_ValidMove_UpdatesPosition()
{
    // Arrange, Act, Assert
}

[Fact]
public void IsValidPosition_OutOfBounds_ReturnsFalse()
{
    // Arrange, Act, Assert
}

[Fact]
public void ApplyMove_NullMove_ThrowsArgumentNullException()
{
    // Arrange, Act, Assert
}
```

### Pattern Breakdown

```
<MethodName>_<Scenario>_<ExpectedBehavior>
```

- **MethodName:** The method being tested
- **Scenario:** The input condition or state being tested
- **ExpectedBehavior:** What should happen

### Additional Examples

```csharp
[Fact]
public void GetStatus_GameInitialized_ReturnsVersionString() { }

[Fact]
public void Constructor_NullValidator_ThrowsArgumentNullException() { }

[Theory]
[InlineData(0, 0, true)]
[InlineData(8, 8, false)]
public void IsInBounds_VariousPositions_ReturnsExpectedResult(int x, int y, bool expected)
{
    // ...
}
```

---

## Code Style

### Braces and Indentation

- **Always use braces** for control structures (even single-line)
  ```csharp
  // Good
  if (condition)
  {
      DoSomething();
  }
  
  // Avoid
  if (condition) DoSomething();
  ```

- **Opening brace on new line** (K&R style for C#)
  ```csharp
  public void Method()
  {
      // ...
  }
  ```

- **Indentation:** 4 spaces (configured in `.editorconfig`)

### Type Declaration

- **Use `var`** when the type is obvious
  ```csharp
  var player = new Player();
  var count = GetCount();
  ```

- **Explicit types** when clarity is needed
  ```csharp
  IMovementValidator validator = CreateValidator();
  Position position = CalculatePosition();
  ```

### Access Modifiers

- **Always specify explicitly**
  ```csharp
  // Good
  public class GameState { }
  private int _count;
  
  // Avoid implicit
  class GameState { }  // avoid
  int _count;           // avoid
  ```

### Ordering

Class members should follow this order:

1. Constants
2. Static fields
3. Fields (private, then protected, then public)
4. Constructors
5. Properties
6. Methods (public, then protected, then private)
7. Nested types

### Comments and Documentation

- **XML comments** for all public APIs
  ```csharp
  /// <summary>
  /// Validates whether a move is legal according to chess rules.
  /// </summary>
  /// <param name="move">The move to validate.</param>
  /// <returns>True if the move is valid; otherwise, false.</returns>
  public bool IsValidMove(Move move)
  {
      // ...
  }
  ```

- **Inline comments** for complex logic
  ```csharp
  // Check if the king is in check after the move
  if (IsKingInCheck(afterMoveState))
  {
      return false;
  }
  ```

- Avoid obvious comments
  ```csharp
  // Bad: _count++; // Increment count
  // Good: Just write _count++
  ```

### Line Length

- Target **120 characters** maximum
- Break long lines at logical points

---

## Automated Enforcement

This project uses:

- **`.editorconfig`** — Enforces formatting rules (indentation, spacing, etc.)
- **`dotnet format`** — Auto-formats code to match `.editorconfig`
- **`Directory.Build.props`** — Enforces warnings as errors, nullable refs, XML docs

### Before Committing

```bash
# Format code
dotnet format

# Verify build with no warnings
dotnet build

# Run tests
dotnet test
```

---

## Project-Specific Conventions

### Test Arrange-Act-Assert

Use clear AAA pattern with comments:

```csharp
[Fact]
public void Example_Test()
{
    // Arrange
    var sut = new GameState();
    
    // Act
    var result = sut.GetStatus();
    
    // Assert
    Assert.NotNull(result);
}
```

### Nullable Reference Types

Enabled project-wide. Use nullability annotations correctly:

```csharp
// Non-nullable by default
public string Name { get; set; } = string.Empty;

// Nullable when appropriate
public string? OptionalDescription { get; set; }

// Null-forgiving operator when justified
public string GetValue() => _cachedValue!;
```

---

## References

- [Microsoft C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [.NET Framework Design Guidelines](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/)
- [C# Language Specification](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/)

---

## Questions or Exceptions?

If you encounter a naming scenario not covered here, or need to propose an exception:

1. Open an issue on GitHub for discussion
2. Reference this guide in code reviews
3. Update this document via PR if patterns need refinement

For architectural decisions, see [`docs/decisions/`](decisions/) for the ADR process.
