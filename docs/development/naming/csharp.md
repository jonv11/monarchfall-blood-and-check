# C# Naming Conventions

## Purpose
Define naming rules for C# identifiers used in MFBC.

## Scope
Applies to classes, methods, properties, fields, parameters, and enums.

## PascalCase

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

## camelCase

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

## Special Cases

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
