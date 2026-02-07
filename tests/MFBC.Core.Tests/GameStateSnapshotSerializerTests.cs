using Xunit;

namespace MFBC.Core.Tests;

public class GameStateSnapshotSerializerTests
{
    [Fact]
    public void Serialize_SameStateRepeated_ProducesIdenticalOutput()
    {
        var state = CreateEquivalentStateA();

        var first = GameStateSnapshotSerializer.Serialize(state);
        var second = GameStateSnapshotSerializer.Serialize(state);

        Assert.Equal(first, second);
    }

    [Fact]
    public void Serialize_EquivalentStatesWithDifferentInsertionOrder_ProducesIdenticalOutput()
    {
        var left = CreateEquivalentStateA();
        var right = CreateEquivalentStateB();

        var leftJson = GameStateSnapshotSerializer.Serialize(left);
        var rightJson = GameStateSnapshotSerializer.Serialize(right);

        Assert.Equal(leftJson, rightJson);
    }

    [Fact]
    public void Serialize_MatchesGoldenSnapshot()
    {
        var state = CreateEquivalentStateA();
        SnapshotAssert.Matches("basic-game-state", state);
    }

    private static GameState CreateEquivalentStateA()
    {
        var board = new Board();
        board.AddTile(new Tile(new Coord(0, 0)));
        board.AddTile(new Tile(new Coord(1, 0)));
        board.AddTile(new Tile(new Coord(2, 0), enterable: false));

        var state = new GameState(board, new DeterministicRandomSource(5UL));
        state.AddPiece(new Piece(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), PieceType.Rook, Side.White, new Coord(0, 0)));
        state.AddPiece(new Piece(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), PieceType.King, Side.Black, new Coord(1, 0)));
        return state;
    }

    private static GameState CreateEquivalentStateB()
    {
        var board = new Board();
        board.AddTile(new Tile(new Coord(2, 0), enterable: false));
        board.AddTile(new Tile(new Coord(1, 0)));
        board.AddTile(new Tile(new Coord(0, 0)));

        var state = new GameState(board, new DeterministicRandomSource(99UL));
        state.AddPiece(new Piece(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), PieceType.King, Side.Black, new Coord(1, 0)));
        state.AddPiece(new Piece(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), PieceType.Rook, Side.White, new Coord(0, 0)));
        return state;
    }
}
