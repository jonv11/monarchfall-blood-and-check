namespace MFBC.Core;

/// <summary>
/// Builds deterministic initial board states using seed-derived random streams.
/// </summary>
public static class DeterministicBoardStateInitializer
{
    private static readonly Guid WhiteRookId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid BlackKingId = Guid.Parse("22222222-2222-2222-2222-222222222222");

    /// <summary>
    /// Creates a deterministic minimal game state for a rectangular sparse board.
    /// </summary>
    /// <param name="seed">Run seed used for deterministic initialization.</param>
    /// <param name="width">Board width.</param>
    /// <param name="height">Board height.</param>
    /// <returns>Initialized game state with deterministic piece placement.</returns>
    public static GameState CreateMinimal(ulong seed, int width = 8, int height = 8)
    {
        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than zero.");
        }

        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than zero.");
        }

        var board = new Board();
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                board.AddTile(new Tile(new Coord(x, y)));
            }
        }

        var state = new GameState(board, new DeterministicRandomSource(seed));
        InitializeMinimal(state, "board.init");
        return state;
    }

    /// <summary>
    /// Initializes a minimal deterministic piece set on an existing game state.
    /// </summary>
    /// <param name="state">Target game state.</param>
    /// <param name="streamName">Named stream to use for placement.</param>
    public static void InitializeMinimal(GameState state, string streamName)
    {
        if (state is null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        if (string.IsNullOrWhiteSpace(streamName))
        {
            throw new ArgumentException("Stream name must be provided.", nameof(streamName));
        }

        var eligibleTiles = state.Board.Tiles.Values
            .Where(tile => tile.Enterable && !tile.IsOccupied)
            .OrderBy(tile => tile.Coord.Y)
            .ThenBy(tile => tile.Coord.X)
            .Select(tile => tile.Coord)
            .ToArray();

        if (eligibleTiles.Length < 2)
        {
            throw new InvalidOperationException("At least two enterable, unoccupied tiles are required.");
        }

        var whiteIndex = state.RandomSource.NextInt(streamName, 0, eligibleTiles.Length);
        var blackIndex = state.RandomSource.NextInt(streamName, 0, eligibleTiles.Length - 1);
        if (blackIndex >= whiteIndex)
        {
            blackIndex++;
        }

        var whiteRook = new Piece(WhiteRookId, PieceType.Rook, Side.White, eligibleTiles[whiteIndex]);
        var blackKing = new Piece(BlackKingId, PieceType.King, Side.Black, eligibleTiles[blackIndex]);

        state.AddPiece(whiteRook);
        state.AddPiece(blackKing);
    }
}
