namespace MFBC.Core;

/// <summary>
/// Represents the current game state.
/// </summary>
public sealed class GameState
{
    private readonly Dictionary<Guid, Piece> _pieces = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="GameState"/> class.
    /// </summary>
    /// <param name="board">Board instance.</param>
    public GameState(Board board)
    {
        Board = board ?? throw new ArgumentNullException(nameof(board));
        Version = "0.0.0";
    }

    /// <summary>
    /// Gets the board for this game state.
    /// </summary>
    public Board Board { get; }

    /// <summary>
    /// Gets the game version identifier.
    /// </summary>
    public string Version { get; }

    /// <summary>
    /// Gets a read-only view of pieces by id.
    /// </summary>
    public IReadOnlyDictionary<Guid, Piece> Pieces => _pieces;

    /// <summary>
    /// Adds a piece to the game state and places it on the board.
    /// </summary>
    /// <param name="piece">Piece to add.</param>
    /// <param name="allowNonEnterable">Whether placement may ignore enterability.</param>
    public void AddPiece(Piece piece, bool allowNonEnterable = false)
    {
        if (piece is null)
        {
            throw new ArgumentNullException(nameof(piece));
        }

        if (_pieces.ContainsKey(piece.Id))
        {
            throw new InvalidOperationException($"Piece with id {piece.Id} already exists.");
        }

        Board.PlacePiece(piece, allowNonEnterable);
        _pieces[piece.Id] = piece;
    }

    /// <summary>
    /// Validates core invariants between board and piece state.
    /// </summary>
    public void ValidateInvariants()
    {
        // If you need to test failure paths, consider a DEBUG-only test helper
        // that can create intentionally inconsistent state.
        foreach (var piece in _pieces.Values)
        {
            if (!Board.HasTile(piece.Position))
            {
                throw new InvalidOperationException($"Piece {piece.Id} is on a non-existent tile {piece.Position}.");
            }

            var tile = Board.GetTile(piece.Position);
            if (tile.OccupantId != piece.Id)
            {
                throw new InvalidOperationException($"Piece {piece.Id} is not registered as occupant of {piece.Position}.");
            }
        }
    }

    /// <summary>
    /// Gets a simple status message.
    /// </summary>
    public string GetStatus()
    {
        return $"Game initialized at version {Version}";
    }
}
