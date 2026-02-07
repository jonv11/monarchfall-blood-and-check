using Xunit;

namespace MFBC.Core.Tests;

public class GameStateTests
{
    [Fact]
    public void GameState_Throws_WhenBoardNull()
    {
        Assert.Throws<ArgumentNullException>(() => new GameState(null!));
    }

    [Fact]
    public void GameState_Throws_WhenRandomSourceNull()
    {
        var board = new Board();

        Assert.Throws<ArgumentNullException>(() => new GameState(board, null!));
    }

    [Fact]
    public void GameState_UsesProvidedRandomSource()
    {
        var board = new Board();
        var random = new DeterministicRandomSource(7UL);
        var gameState = new GameState(board, random);

        Assert.Same(random, gameState.RandomSource);
    }

    [Fact]
    public void GameState_GetStatus_ReturnsMessageWithVersion()
    {
        var board = new Board();
        var gameState = new GameState(board);

        var status = gameState.GetStatus();

        Assert.NotNull(status);
        Assert.Contains("0.0.0", status);
    }

    [Fact]
    public void AddPiece_PlacesOnBoard()
    {
        var board = new Board();
        var coord = new Coord(0, 0);
        board.AddTile(new Tile(coord));
        var gameState = new GameState(board);
        var piece = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, coord);

        gameState.AddPiece(piece);

        Assert.Equal(piece.Id, board.GetTile(coord).OccupantId);
        Assert.True(gameState.Pieces.ContainsKey(piece.Id));
    }

    [Fact]
    public void AddPiece_Throws_WhenNull()
    {
        var board = new Board();
        var gameState = new GameState(board);

        Assert.Throws<ArgumentNullException>(() => gameState.AddPiece(null!));
    }

    [Fact]
    public void AddPiece_Throws_WhenDuplicateId()
    {
        var board = new Board();
        var coord = new Coord(0, 0);
        var coord2 = new Coord(1, 0);
        board.AddTile(new Tile(coord));
        board.AddTile(new Tile(coord2));
        var gameState = new GameState(board);
        var id = Guid.NewGuid();
        var first = new Piece(id, PieceType.Rook, Side.White, coord);
        var second = new Piece(id, PieceType.King, Side.Black, coord2);

        gameState.AddPiece(first);
        var ex = Assert.Throws<InvalidOperationException>(() => gameState.AddPiece(second));

        Assert.Contains("already exists", ex.Message);
    }

    [Fact]
    public void ValidateInvariants_DoesNotThrow_WhenStateIsConsistent()
    {
        var board = new Board();
        var coord = new Coord(0, 0);
        board.AddTile(new Tile(coord));
        var gameState = new GameState(board);
        var piece = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, coord);

        gameState.AddPiece(piece);

        gameState.ValidateInvariants();
    }
}
