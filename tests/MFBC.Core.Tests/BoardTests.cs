using Xunit;

namespace MFBC.Core.Tests;

public class BoardTests
{
    [Fact]
    public void AddTile_Throws_WhenDuplicate()
    {
        var board = new Board();
        var tile = new Tile(new Coord(0, 0));

        board.AddTile(tile);

        var ex = Assert.Throws<InvalidOperationException>(() => board.AddTile(new Tile(new Coord(0, 0))));
        Assert.Contains("already exists", ex.Message);
    }

    [Fact]
    public void AddTile_Throws_WhenNull()
    {
        var board = new Board();

        Assert.Throws<ArgumentNullException>(() => board.AddTile(null!));
    }

    [Fact]
    public void GetTile_Throws_WhenMissing()
    {
        var board = new Board();

        var ex = Assert.Throws<InvalidOperationException>(() => board.GetTile(new Coord(5, -3)));
        Assert.Contains("does not exist", ex.Message);
    }

    [Fact]
    public void RemoveTile_Throws_WhenMissing()
    {
        var board = new Board();

        var ex = Assert.Throws<InvalidOperationException>(() => board.RemoveTile(new Coord(1, 1)));
        Assert.Contains("does not exist", ex.Message);
    }

    [Fact]
    public void RemoveTile_Throws_WhenOccupied()
    {
        var board = new Board();
        board.AddTile(new Tile(new Coord(0, 0)));
        var piece = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, new Coord(0, 0));
        board.PlacePiece(piece);

        var ex = Assert.Throws<InvalidOperationException>(() => board.RemoveTile(new Coord(0, 0)));
        Assert.Contains("occupied", ex.Message);
    }

    [Fact]
    public void PlacePiece_Throws_WhenTileMissing()
    {
        var board = new Board();
        var piece = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, new Coord(1, 1));

        var ex = Assert.Throws<InvalidOperationException>(() => board.PlacePiece(piece));
        Assert.Contains("does not exist", ex.Message);
    }

    [Fact]
    public void PlacePiece_Throws_WhenTileNotEnterable()
    {
        var board = new Board();
        var coord = new Coord(0, 0);
        board.AddTile(new Tile(coord, enterable: false));
        var piece = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, coord);

        var ex = Assert.Throws<InvalidOperationException>(() => board.PlacePiece(piece));
        Assert.Contains("not enterable", ex.Message);
    }

    [Fact]
    public void PlacePiece_Allows_WhenTileNotEnterableAndOverride()
    {
        var board = new Board();
        var coord = new Coord(0, 0);
        board.AddTile(new Tile(coord, enterable: false));
        var piece = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, coord);

        board.PlacePiece(piece, allowNonEnterable: true);

        Assert.Equal(piece.Id, board.GetTile(coord).OccupantId);
    }

    [Fact]
    public void PlacePiece_Throws_WhenTileOccupied()
    {
        var board = new Board();
        var coord = new Coord(0, 0);
        board.AddTile(new Tile(coord));
        var first = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, coord);
        var second = new Piece(Guid.NewGuid(), PieceType.King, Side.Black, coord);

        board.PlacePiece(first);
        var ex = Assert.Throws<InvalidOperationException>(() => board.PlacePiece(second));

        Assert.Contains("already occupied", ex.Message);
    }

    [Fact]
    public void PlacePiece_Throws_WhenPieceNull()
    {
        var board = new Board();
        board.AddTile(new Tile(new Coord(0, 0)));

        Assert.Throws<ArgumentNullException>(() => board.PlacePiece(null!));
    }

    [Fact]
    public void MovePiece_Throws_WhenPieceNull()
    {
        var board = new Board();
        board.AddTile(new Tile(new Coord(0, 0)));
        board.AddTile(new Tile(new Coord(1, 0)));

        Assert.Throws<ArgumentNullException>(() => board.MovePiece(null!, new Coord(1, 0)));
    }

    [Fact]
    public void MovePiece_Throws_WhenDestinationMissing()
    {
        var board = new Board();
        var from = new Coord(0, 0);
        board.AddTile(new Tile(from));
        var piece = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, from);
        board.PlacePiece(piece);

        var ex = Assert.Throws<InvalidOperationException>(() => board.MovePiece(piece, new Coord(9, 9)));
        Assert.Contains("does not exist", ex.Message);
    }

    [Fact]
    public void MovePiece_Throws_WhenDestinationOccupied()
    {
        var board = new Board();
        var from = new Coord(0, 0);
        var to = new Coord(1, 0);
        board.AddTile(new Tile(from));
        board.AddTile(new Tile(to));
        var pieceA = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, from);
        var pieceB = new Piece(Guid.NewGuid(), PieceType.King, Side.Black, to);
        board.PlacePiece(pieceA);
        board.PlacePiece(pieceB);

        var ex = Assert.Throws<InvalidOperationException>(() => board.MovePiece(pieceA, to));
        Assert.Contains("already occupied", ex.Message);
    }

    [Fact]
    public void MovePiece_UpdatesOccupancy()
    {
        var board = new Board();
        var from = new Coord(0, 0);
        var to = new Coord(1, 0);
        board.AddTile(new Tile(from));
        board.AddTile(new Tile(to));
        var piece = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, from);
        board.PlacePiece(piece);

        board.MovePiece(piece, to);

        Assert.Null(board.GetTile(from).OccupantId);
        Assert.Equal(piece.Id, board.GetTile(to).OccupantId);
        Assert.Equal(to, piece.Position);
    }

    [Fact]
    public void HasTile_ReturnsExpected()
    {
        var board = new Board();
        var coord = new Coord(2, 2);

        Assert.False(board.HasTile(coord));
        board.AddTile(new Tile(coord));
        Assert.True(board.HasTile(coord));
    }
}
