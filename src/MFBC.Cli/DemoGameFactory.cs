using MFBC.Core;

namespace MFBC.Cli;

internal static class DemoGameFactory
{
    public static GameState Create()
    {
        var board = new Board();
        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                board.AddTile(new Tile(new Coord(x, y)));
            }
        }

        var state = new GameState(board);
        var whiteRook = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, new Coord(0, 0));
        var blackKing = new Piece(Guid.NewGuid(), PieceType.King, Side.Black, new Coord(7, 7));
        state.AddPiece(whiteRook);
        state.AddPiece(blackKing);

        return state;
    }
}
