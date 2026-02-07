namespace MFBC.Core;

/// <summary>
/// Provides deterministic random values scoped by named streams.
/// </summary>
public interface IRandomSource
{
    /// <summary>
    /// Returns a deterministic random integer in the given range for the specified stream.
    /// </summary>
    /// <param name="streamName">Logical random stream name.</param>
    /// <param name="minInclusive">Inclusive lower bound.</param>
    /// <param name="maxExclusive">Exclusive upper bound.</param>
    int NextInt(string streamName, int minInclusive, int maxExclusive);
}
