namespace MFBC.Core;

/// <summary>
/// Represents a piece on the board.
/// </summary>
public sealed class Piece
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Piece"/> class.
    /// </summary>
    /// <param name="id">Unique identifier for the piece.</param>
    /// <param name="type">Piece type.</param>
    /// <param name="side">Piece side.</param>
    /// <param name="position">Current position.</param>
    public Piece(Guid id, PieceType type, Side side, Coord position)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Piece id must be non-empty.", nameof(id));
        }

        Id = id;
        Type = type;
        Side = side;
        Position = position;
        IsAlive = true;
    }

    /// <summary>
    /// Gets the unique identifier for the piece.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Gets the piece type.
    /// </summary>
    public PieceType Type { get; }

    /// <summary>
    /// Gets the piece side.
    /// </summary>
    public Side Side { get; }

    /// <summary>
    /// Gets the current position of the piece.
    /// </summary>
    public Coord Position { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the piece is alive.
    /// </summary>
    public bool IsAlive { get; private set; }

    /// <summary>
    /// Updates the position of the piece.
    /// </summary>
    /// <param name="position">New position.</param>
    internal void SetPosition(Coord position)
    {
        Position = position;
    }

    /// <summary>
    /// Marks the piece as dead.
    /// </summary>
    internal void Kill()
    {
        IsAlive = false;
    }
}
