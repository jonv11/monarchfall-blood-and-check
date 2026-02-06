using System.Collections.ObjectModel;

namespace MFBC.Core;

/// <summary>
/// Represents the result of applying an action.
/// </summary>
public sealed class ApplyResult
{
    private ApplyResult(IReadOnlyList<GameEvent> events, IReadOnlyList<ApplyError> errors)
    {
        Events = events;
        Errors = errors;
    }

    /// <summary>
    /// Gets the emitted events.
    /// </summary>
    public IReadOnlyList<GameEvent> Events { get; }

    /// <summary>
    /// Gets the errors, if any.
    /// </summary>
    public IReadOnlyList<ApplyError> Errors { get; }

    /// <summary>
    /// Gets a value indicating whether the result is successful.
    /// </summary>
    public bool IsSuccess => Errors.Count == 0;

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static ApplyResult Success(params GameEvent[] events)
    {
        return new ApplyResult(new ReadOnlyCollection<GameEvent>(events ?? Array.Empty<GameEvent>()), Array.Empty<ApplyError>());
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static ApplyResult Failure(params ApplyError[] errors)
    {
        return new ApplyResult(Array.Empty<GameEvent>(), new ReadOnlyCollection<ApplyError>(errors ?? Array.Empty<ApplyError>()));
    }
}
