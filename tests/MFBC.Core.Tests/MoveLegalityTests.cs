using Xunit;

namespace MFBC.Core.Tests;

public class MoveLegalityTests
{
    [Fact]
    public void Rook_AllowsStraightMove_WhenPathClear()
    {
        var state = CreateEmptyBoardState();
        var rook = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, new Coord(0, 0));
        state.AddPiece(rook);

        var error = MoveLegality.Validate(state, rook, rook.Position, new Coord(0, 5));

        Assert.Null(error);
    }

    [Fact]
    public void Rook_RejectsDiagonalMove()
    {
        var state = CreateEmptyBoardState();
        var rook = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, new Coord(0, 0));
        state.AddPiece(rook);

        var error = MoveLegality.Validate(state, rook, rook.Position, new Coord(1, 1));

        Assert.NotNull(error);
        Assert.Equal("rook_invalid", error!.Code);
    }

    [Fact]
    public void Rook_RejectsMove_WhenPathBlocked()
    {
        var state = CreateEmptyBoardState();
        var rook = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, new Coord(0, 0));
        var blocker = new Piece(Guid.NewGuid(), PieceType.King, Side.White, new Coord(0, 2));
        state.AddPiece(rook);
        state.AddPiece(blocker);

        var error = MoveLegality.Validate(state, rook, rook.Position, new Coord(0, 5));

        Assert.NotNull(error);
        Assert.Equal("path_blocked", error!.Code);
    }

    [Fact]
    public void Rook_RejectsMove_WhenPathTileMissing()
    {
        var board = new Board();
        board.AddTile(new Tile(new Coord(0, 0)));
        board.AddTile(new Tile(new Coord(0, 2)));
        board.AddTile(new Tile(new Coord(0, 3)));
        var state = new GameState(board);
        var rook = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, new Coord(0, 0));
        state.AddPiece(rook);

        var error = MoveLegality.Validate(state, rook, rook.Position, new Coord(0, 3));

        Assert.NotNull(error);
        Assert.Equal("path_missing", error!.Code);
    }

    [Fact]
    public void King_AllowsSingleStepMove()
    {
        var state = CreateEmptyBoardState();
        var king = new Piece(Guid.NewGuid(), PieceType.King, Side.White, new Coord(4, 4));
        state.AddPiece(king);

        var error = MoveLegality.Validate(state, king, king.Position, new Coord(5, 5));

        Assert.Null(error);
    }

    [Fact]
    public void King_RejectsMove_MoreThanOneSquare()
    {
        var state = CreateEmptyBoardState();
        var king = new Piece(Guid.NewGuid(), PieceType.King, Side.White, new Coord(4, 4));
        state.AddPiece(king);

        var error = MoveLegality.Validate(state, king, king.Position, new Coord(6, 4));

        Assert.NotNull(error);
        Assert.Equal("king_invalid", error!.Code);
    }

    [Fact]
    public void Destination_Rejects_AllyOccupant()
    {
        var state = CreateEmptyBoardState();
        var rook = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, new Coord(0, 0));
        var ally = new Piece(Guid.NewGuid(), PieceType.King, Side.White, new Coord(0, 3));
        state.AddPiece(rook);
        state.AddPiece(ally);

        var error = MoveLegality.Validate(state, rook, rook.Position, ally.Position);

        Assert.NotNull(error);
        Assert.Equal("destination_ally", error!.Code);
    }

    private static GameState CreateEmptyBoardState()
    {
        var board = new Board();
        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                board.AddTile(new Tile(new Coord(x, y)));
            }
        }

        return new GameState(board);
    }
}
