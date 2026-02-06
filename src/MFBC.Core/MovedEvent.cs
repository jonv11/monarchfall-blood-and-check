namespace MFBC.Core;

/// <summary>
/// Event emitted when a piece is moved.
/// </summary>
public sealed record MovedEvent(Guid PieceId, Coord From, Coord To) : GameEvent(GameEventKind.Moved);
