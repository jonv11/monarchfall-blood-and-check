using MFBC.Core;

namespace MFBC.Cli;

class Program
{
    static void Main()
    {
        Console.WriteLine("=================================");
        Console.WriteLine("  Monarchfall: Blood & Check");
        Console.WriteLine("=================================");
        Console.WriteLine();

        // TODO: Replace with proper run initialization once the action pipeline exists.
        var board = new Board();
        board.AddTile(new Tile(new Coord(0, 0)));
        var gameState = new GameState(board);
        Console.WriteLine(gameState.GetStatus());
    }
}
