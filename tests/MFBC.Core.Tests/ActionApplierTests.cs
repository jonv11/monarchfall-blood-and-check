using Xunit;

namespace MFBC.Core.Tests;

public class ActionApplierTests
{
    [Fact]
    public void ApplyAction_Throws_WhenStateNull()
    {
        var applier = new ActionApplier();
        var action = new MoveAction(Guid.NewGuid(), new Coord(0, 0), new Coord(1, 0));

        Assert.Throws<ArgumentNullException>(() => applier.ApplyAction(null!, action));
    }

    [Fact]
    public void ApplyAction_Throws_WhenActionNull()
    {
        var applier = new ActionApplier();
        var state = CreateStateWithSinglePiece(out _);

        Assert.Throws<ArgumentNullException>(() => applier.ApplyAction(state, null!));
    }

    [Fact]
    public void ApplyAction_Throws_WhenActorIdEmpty()
    {
        Assert.Throws<ArgumentException>(() => new MoveAction(Guid.Empty, new Coord(0, 0), new Coord(1, 0)));
    }

    [Fact]
    public void ApplyAction_ReturnsError_WhenActorMissing()
    {
        var applier = new ActionApplier();
        var state = CreateStateWithSinglePiece(out _);
        var action = new MoveAction(Guid.NewGuid(), new Coord(0, 0), new Coord(1, 0));

        var result = applier.ApplyAction(state, action);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "actor_missing");
    }

    [Fact]
    public void ApplyAction_ReturnsError_WhenOriginMissing()
    {
        var applier = new ActionApplier();
        var state = CreateStateWithSinglePiece(out var piece);
        var action = new MoveAction(piece.Id, new Coord(9, 9), new Coord(1, 0));

        var result = applier.ApplyAction(state, action);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "origin_missing");
    }

    [Fact]
    public void ApplyAction_ReturnsError_WhenActorPositionMismatch()
    {
        var applier = new ActionApplier();
        var state = CreateStateWithSinglePiece(out var piece);
        var action = new MoveAction(piece.Id, new Coord(1, 0), new Coord(0, 0));

        var result = applier.ApplyAction(state, action);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "actor_position_mismatch");
    }

    [Fact]
    public void ApplyAction_ReturnsError_WhenNoOpMove()
    {
        var applier = new ActionApplier();
        var state = CreateStateWithSinglePiece(out var piece);
        var action = new MoveAction(piece.Id, piece.Position, piece.Position);

        var result = applier.ApplyAction(state, action);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "no_op_move");
    }

    [Fact]
    public void ApplyAction_ReturnsError_WhenDestinationMissing()
    {
        var applier = new ActionApplier();
        var state = CreateStateWithSinglePiece(out var piece);
        var action = new MoveAction(piece.Id, piece.Position, new Coord(9, 9));

        var result = applier.ApplyAction(state, action);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "destination_missing");
    }

    [Fact]
    public void ApplyAction_ReturnsError_WhenDestinationOccupied()
    {
        var applier = new ActionApplier();
        var board = new Board();
        var from = new Coord(0, 0);
        var to = new Coord(0, 2);
        board.AddTile(new Tile(from));
        board.AddTile(new Tile(new Coord(0, 1)));
        board.AddTile(new Tile(to));
        var state = new GameState(board);
        var mover = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, from);
        var blocker = new Piece(Guid.NewGuid(), PieceType.King, Side.White, to);
        state.AddPiece(mover);
        state.AddPiece(blocker);
        var action = new MoveAction(mover.Id, from, to);

        var result = applier.ApplyAction(state, action);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "destination_ally");
    }

    [Fact]
    public void ApplyAction_CapturesEnemy_WhenDestinationOccupied()
    {
        var applier = new ActionApplier();
        var board = new Board();
        var from = new Coord(0, 0);
        var to = new Coord(0, 3);
        board.AddTile(new Tile(from));
        board.AddTile(new Tile(new Coord(0, 1)));
        board.AddTile(new Tile(new Coord(0, 2)));
        board.AddTile(new Tile(to));
        var state = new GameState(board);
        var mover = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, from);
        var target = new Piece(Guid.NewGuid(), PieceType.King, Side.Black, to);
        state.AddPiece(mover);
        state.AddPiece(target);

        var result = applier.ApplyAction(state, new MoveAction(mover.Id, from, to));

        Assert.True(result.IsSuccess);
        Assert.False(state.Pieces.ContainsKey(target.Id));
        Assert.Equal(to, mover.Position);
    }

    [Fact]
    public void ApplyAction_Rejects_WhenDestinationOccupiedByAlly()
    {
        var applier = new ActionApplier();
        var board = new Board();
        var from = new Coord(0, 0);
        var to = new Coord(0, 2);
        board.AddTile(new Tile(from));
        board.AddTile(new Tile(new Coord(0, 1)));
        board.AddTile(new Tile(to));
        var state = new GameState(board);
        var mover = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, from);
        var ally = new Piece(Guid.NewGuid(), PieceType.King, Side.White, to);
        state.AddPiece(mover);
        state.AddPiece(ally);

        var result = applier.ApplyAction(state, new MoveAction(mover.Id, from, to));

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "destination_ally");
    }

    [Fact]
    public void ApplyAction_MovesPieceAndEmitsEvent()
    {
        var applier = new ActionApplier();
        var state = CreateStateWithSinglePiece(out var piece);
        var from = piece.Position;
        var to = new Coord(1, 0);
        var action = new MoveAction(piece.Id, from, to);

        var result = applier.ApplyAction(state, action);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
        Assert.Single(result.Events);
        var moved = Assert.IsType<MovedEvent>(result.Events[0]);
        Assert.Equal(piece.Id, moved.PieceId);
        Assert.Equal(from, moved.From);
        Assert.Equal(to, moved.To);
        Assert.Equal(to, piece.Position);
    }

    [Fact]
    public void ApplyAction_ReturnsError_WhenUnsupportedAction()
    {
        var applier = new ActionApplier();
        var state = CreateStateWithSinglePiece(out var piece);
        var action = new TestAction(piece.Id);

        var result = applier.ApplyAction(state, action);

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Code == "unsupported_action");
        Assert.Equal(piece.Position, state.Pieces[piece.Id].Position);
    }

    private static GameState CreateStateWithSinglePiece(out Piece piece)
    {
        var board = new Board();
        var from = new Coord(0, 0);
        var to = new Coord(1, 0);
        board.AddTile(new Tile(from));
        board.AddTile(new Tile(to));
        var state = new GameState(board);
        piece = new Piece(Guid.NewGuid(), PieceType.Rook, Side.White, from);
        state.AddPiece(piece);
        return state;
    }

    private sealed record TestAction(Guid ActorId) : GameAction(GameActionKind.Unknown, ActorId);
}
