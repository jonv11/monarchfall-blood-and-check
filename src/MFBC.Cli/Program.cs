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

        var gameState = new GameState();
        Console.WriteLine(gameState.GetStatus());
    }
}
