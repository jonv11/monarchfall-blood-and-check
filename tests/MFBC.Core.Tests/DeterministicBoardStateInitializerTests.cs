using System.Text;
using Xunit;

namespace MFBC.Core.Tests;

public class DeterministicBoardStateInitializerTests
{
    [Fact]
    public void CreateMinimal_SameSeed_ProducesIdenticalSnapshots()
    {
        var left = DeterministicBoardStateInitializer.CreateMinimal(seed: 100UL, width: 8, height: 8);
        var right = DeterministicBoardStateInitializer.CreateMinimal(seed: 100UL, width: 8, height: 8);

        Assert.Equal(CreateSnapshot(left), CreateSnapshot(right));
    }

    [Fact]
    public void CreateMinimal_DifferentSeed_ProducesDifferentSnapshots()
    {
        var left = DeterministicBoardStateInitializer.CreateMinimal(seed: 100UL, width: 8, height: 8);
        var right = DeterministicBoardStateInitializer.CreateMinimal(seed: 101UL, width: 8, height: 8);

        Assert.NotEqual(CreateSnapshot(left), CreateSnapshot(right));
    }

    [Fact]
    public void CreateMinimal_ProvidesStableTileAndPieceOrdering()
    {
        var state = DeterministicBoardStateInitializer.CreateMinimal(seed: 200UL, width: 3, height: 2);

        var orderedTiles = state.Board.Tiles.Keys
            .OrderBy(coord => coord.Y)
            .ThenBy(coord => coord.X)
            .ToArray();

        Assert.Equal(
            [new Coord(0, 0), new Coord(1, 0), new Coord(2, 0), new Coord(0, 1), new Coord(1, 1), new Coord(2, 1)],
            orderedTiles);

        var orderedPieces = state.Pieces.Values
            .OrderBy(piece => piece.Side)
            .ThenBy(piece => piece.Type)
            .Select(piece => piece.Id)
            .ToArray();

        Assert.Equal(
            [Guid.Parse("11111111-1111-1111-1111-111111111111"), Guid.Parse("22222222-2222-2222-2222-222222222222")],
            orderedPieces);
    }

    [Fact]
    public void InitializeMinimal_UsesNamedStreamDeterministically()
    {
        var boardA = CreateThreeByThreeBoard();
        var boardB = CreateThreeByThreeBoard();
        var randomA = new DeterministicRandomSource(42UL);
        var randomB = new DeterministicRandomSource(42UL);

        var stateA = new GameState(boardA, randomA);
        var stateB = new GameState(boardB, randomB);

        DeterministicBoardStateInitializer.InitializeMinimal(stateA, "board.init");
        DeterministicBoardStateInitializer.InitializeMinimal(stateB, "board.init");

        Assert.Equal(CreateSnapshot(stateA), CreateSnapshot(stateB));
    }

    private static Board CreateThreeByThreeBoard()
    {
        var board = new Board();
        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                board.AddTile(new Tile(new Coord(x, y)));
            }
        }

        return board;
    }

    private static string CreateSnapshot(GameState state)
    {
        var builder = new StringBuilder();
        foreach (var coord in state.Board.Tiles.Keys.OrderBy(c => c.Y).ThenBy(c => c.X))
        {
            var tile = state.Board.GetTile(coord);
            builder.Append($"T:{coord.X},{coord.Y}:{tile.Enterable}:{tile.OccupantId};");
        }

        foreach (var piece in state.Pieces.Values.OrderBy(p => p.Side).ThenBy(p => p.Type))
        {
            builder.Append($"P:{piece.Id}:{piece.Side}:{piece.Type}:{piece.Position.X},{piece.Position.Y};");
        }

        return builder.ToString();
    }
}
