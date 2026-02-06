namespace MFBC.Core;

/// <summary>
/// Represents a basic move action.
/// </summary>
public sealed record MoveAction : GameAction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MoveAction"/> class.
    /// </summary>
    /// <param name="actorId">Acting piece identifier.</param>
    /// <param name="from">Origin coordinate.</param>
    /// <param name="to">Destination coordinate.</param>
    public MoveAction(Guid actorId, Coord from, Coord to)
        : base(GameActionKind.Move, actorId)
    {
        From = from;
        To = to;
    }

    /// <summary>
    /// Gets the origin coordinate.
    /// </summary>
    public Coord From { get; }

    /// <summary>
    /// Gets the destination coordinate.
    /// </summary>
    public Coord To { get; }
}
