namespace MFBC.Core;

/// <summary>
/// Base type for game events.
/// </summary>
public abstract record GameEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameEvent"/> class.
    /// </summary>
    /// <param name="kind">Event kind.</param>
    protected GameEvent(GameEventKind kind)
    {
        Kind = kind;
    }

    /// <summary>
    /// Gets the event kind.
    /// </summary>
    public GameEventKind Kind { get; }
}
