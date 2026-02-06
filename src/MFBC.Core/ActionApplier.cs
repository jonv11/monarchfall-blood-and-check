namespace MFBC.Core;

/// <summary>
/// Applies actions to a game state.
/// </summary>
public sealed class ActionApplier
{
    /// <summary>
    /// Applies a game action to the provided state.
    /// </summary>
    /// <param name="state">Game state.</param>
    /// <param name="action">Action to apply.</param>
    public ApplyResult ApplyAction(GameState state, GameAction action)
    {
        if (state is null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        return action switch
        {
            MoveAction moveAction => ApplyMove(state, moveAction),
            _ => ApplyResult.Failure(new ApplyError("unsupported_action", $"Unsupported action kind: {action.Kind}."))
        };
    }

    private static ApplyResult ApplyMove(GameState state, MoveAction action)
    {
        if (!state.Pieces.TryGetValue(action.ActorId, out var piece))
        {
            return ApplyResult.Failure(new ApplyError("actor_missing", $"Actor piece not found: {action.ActorId}."));
        }

        if (!state.Board.HasTile(action.From))
        {
            return ApplyResult.Failure(new ApplyError("origin_missing", $"Origin tile does not exist: {action.From}."));
        }

        if (piece.Position != action.From)
        {
            return ApplyResult.Failure(new ApplyError("actor_position_mismatch", "Actor position does not match action origin."));
        }

        if (action.From == action.To)
        {
            return ApplyResult.Failure(new ApplyError("no_op_move", "Action destination matches origin."));
        }

        if (!state.Board.HasTile(action.To))
        {
            return ApplyResult.Failure(new ApplyError("destination_missing", $"Destination tile does not exist: {action.To}."));
        }

        try
        {
            state.Board.MovePiece(piece, action.To);
        }
        catch (InvalidOperationException ex)
        {
            return ApplyResult.Failure(new ApplyError("move_invalid", ex.Message));
        }

        return ApplyResult.Success(new MovedEvent(piece.Id, action.From, action.To));
    }
}
