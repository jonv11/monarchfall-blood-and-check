namespace MFBC.Core;

/// <summary>
/// Base type for player actions.
/// </summary>
public abstract record GameAction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameAction"/> class.
    /// </summary>
    /// <param name="kind">Action kind.</param>
    /// <param name="actorId">Acting piece identifier.</param>
    protected GameAction(GameActionKind kind, Guid actorId)
    {
        if (actorId == Guid.Empty)
        {
            throw new ArgumentException("Actor id must be non-empty.", nameof(actorId));
        }

        Kind = kind;
        ActorId = actorId;
    }

    /// <summary>
    /// Gets the action kind.
    /// </summary>
    public GameActionKind Kind { get; }

    /// <summary>
    /// Gets the acting piece identifier.
    /// </summary>
    public Guid ActorId { get; }
}
