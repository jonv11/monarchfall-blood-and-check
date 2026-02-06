using MFBC.Core;
using Spectre.Console;

namespace MFBC.Cli;

internal static class BoardRenderer
{
    public static void Render(GameState state)
    {
        var board = state.Board;
        var table = new Table();
        table.Border(TableBorder.Minimal);
        table.AddColumn(" ");
        for (var x = 0; x < 8; x++)
        {
            table.AddColumn(((char)('a' + x)).ToString());
        }

        // TODO: This assumes a fixed 8x8 board for v0 CLI output.
        // Extend to render sparse or non-rectangular boards when Core supports it.
        for (var y = 7; y >= 0; y--)
        {
            var row = new List<string> { (y + 1).ToString() };
            for (var x = 0; x < 8; x++)
            {
                var coord = new Coord(x, y);
                if (!board.HasTile(coord))
                {
                    row.Add(" ");
                    continue;
                }

                var tile = board.GetTile(coord);
                if (!tile.IsOccupied)
                {
                    row.Add(".");
                    continue;
                }

                var piece = state.Pieces[tile.OccupantId!.Value];
                row.Add(RenderPiece(piece));
            }

            table.AddRow(row.ToArray());
        }

        AnsiConsole.Write(table);
    }

    private static string RenderPiece(Piece piece)
    {
        var symbol = piece.Type switch
        {
            PieceType.Rook => "R",
            PieceType.King => "K",
            _ => "?"
        };

        return piece.Side == Side.Black ? symbol.ToLowerInvariant() : symbol;
    }
}
