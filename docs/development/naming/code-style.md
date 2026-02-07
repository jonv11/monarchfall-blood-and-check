# Code Style

## Purpose
Define code style rules that improve readability and consistency.

## Scope
Applies to C# code in Core, CLI, and test projects.

## Braces and Indentation

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

## Type Declaration

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

## Access Modifiers

- **Always specify explicitly**
  ```csharp
  // Good
  public class GameState { }
  private int _count;
  
  // Avoid implicit
  class GameState { }  // avoid
  int _count;           // avoid
  ```

## Ordering

Class members should follow this order:

1. Constants
2. Static fields
3. Fields (private, then protected, then public)
4. Constructors
5. Properties
6. Methods (public, then protected, then private)
7. Nested types

## Comments and Documentation

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

## Line Length

- Target **120 characters** maximum
- Break long lines at logical points
