namespace MFBC.Core;

/// <summary>
/// Validates move legality for supported piece types.
/// </summary>
public static class MoveLegality
{
    public static ApplyError? Validate(GameState state, Piece piece, Coord from, Coord to)
    {
        return piece.Type switch
        {
            PieceType.Rook => ValidateRook(state, piece, from, to),
            PieceType.King => ValidateKing(state, piece, from, to),
            _ => new ApplyError("unsupported_piece", $"Unsupported piece type: {piece.Type}.")
        };
    }

    private static ApplyError? ValidateRook(GameState state, Piece piece, Coord from, Coord to)
    {
        if (from.X != to.X && from.Y != to.Y)
        {
            return new ApplyError("rook_invalid", "Rook must move along ranks or files.");
        }

        var stepX = Math.Sign(to.X - from.X);
        var stepY = Math.Sign(to.Y - from.Y);
        var current = new Coord(from.X + stepX, from.Y + stepY);

        while (current != to)
        {
            if (!state.Board.HasTile(current))
            {
                return new ApplyError("path_missing", $"Path tile does not exist: {current}.");
            }

            var tile = state.Board.GetTile(current);
            if (tile.IsOccupied)
            {
                return new ApplyError("path_blocked", $"Path is blocked at {current}.");
            }

            current = new Coord(current.X + stepX, current.Y + stepY);
        }

        return ValidateDestination(state, piece, to);
    }

    private static ApplyError? ValidateKing(GameState state, Piece piece, Coord from, Coord to)
    {
        var dx = Math.Abs(to.X - from.X);
        var dy = Math.Abs(to.Y - from.Y);
        if (dx > 1 || dy > 1)
        {
            return new ApplyError("king_invalid", "King may move one square in any direction.");
        }

        return ValidateDestination(state, piece, to);
    }

    private static ApplyError? ValidateDestination(GameState state, Piece piece, Coord to)
    {
        // Destination existence is validated earlier in ApplyAction.
        if (state.TryGetPieceAt(to, out var occupant) && occupant is not null && occupant.Side == piece.Side)
        {
            return new ApplyError("destination_ally", "Destination is occupied by an allied piece.");
        }

        return null;
    }
}
