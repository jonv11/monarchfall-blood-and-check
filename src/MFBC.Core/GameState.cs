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
    /// Attempts to get a piece at the given coordinate.
    /// </summary>
    /// <param name="coord">Coordinate to inspect.</param>
    /// <param name="piece">Piece at the coordinate, if found.</param>
    public bool TryGetPieceAt(Coord coord, out Piece? piece)
    {
        if (!Board.HasTile(coord))
        {
            piece = null;
            return false;
        }

        var tile = Board.GetTile(coord);
        if (!tile.IsOccupied)
        {
            piece = null;
            return false;
        }

        return _pieces.TryGetValue(tile.OccupantId!.Value, out piece);
    }

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
    /// Removes a piece from the game state.
    /// </summary>
    /// <param name="pieceId">Piece identifier.</param>
    public void RemovePiece(Guid pieceId)
    {
        if (!_pieces.TryGetValue(pieceId, out var piece))
        {
            throw new InvalidOperationException($"Piece with id {pieceId} does not exist.");
        }

        if (Board.HasTile(piece.Position))
        {
            var tile = Board.GetTile(piece.Position);
            if (tile.OccupantId == piece.Id)
            {
                tile.SetOccupant(null);
            }
        }

        piece.Kill();
        _pieces.Remove(piece.Id);
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
