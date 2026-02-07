using System.Text;

namespace MFBC.Core;

/// <summary>
/// Deterministic random source that derives independent named streams from a run seed.
/// </summary>
public sealed class DeterministicRandomSource : IRandomSource
{
    private readonly ulong _runSeed;
    private readonly Dictionary<string, ulong> _streamStates = new(StringComparer.Ordinal);

    /// <summary>
    /// Initializes a new instance of the <see cref="DeterministicRandomSource"/> class.
    /// </summary>
    /// <param name="runSeed">Seed used to derive all named stream states.</param>
    public DeterministicRandomSource(ulong runSeed)
    {
        _runSeed = runSeed;
    }

    /// <inheritdoc />
    public int NextInt(string streamName, int minInclusive, int maxExclusive)
    {
        if (string.IsNullOrWhiteSpace(streamName))
        {
            throw new ArgumentException("Stream name must be provided.", nameof(streamName));
        }

        if (minInclusive >= maxExclusive)
        {
            throw new ArgumentOutOfRangeException(nameof(maxExclusive), "maxExclusive must be greater than minInclusive.");
        }

        var range = (uint)(maxExclusive - minInclusive);
        var value = NextUInt32(streamName);
        return minInclusive + (int)(value % range);
    }

    private uint NextUInt32(string streamName)
    {
        var state = GetOrCreateStreamState(streamName);

        // xorshift64*
        state ^= state >> 12;
        state ^= state << 25;
        state ^= state >> 27;
        state *= 2685821657736338717UL;

        _streamStates[streamName] = state;
        return (uint)(state >> 32);
    }

    private ulong GetOrCreateStreamState(string streamName)
    {
        if (_streamStates.TryGetValue(streamName, out var state))
        {
            return state;
        }

        // Derive stream seed from run seed and stream name, then mix with splitmix64.
        var streamHash = ComputeFnv1A64(streamName);
        state = Mix64(_runSeed ^ streamHash);
        if (state == 0)
        {
            state = 0x9E3779B97F4A7C15UL;
        }

        _streamStates[streamName] = state;
        return state;
    }

    private static ulong ComputeFnv1A64(string value)
    {
        const ulong offsetBasis = 14695981039346656037UL;
        const ulong prime = 1099511628211UL;

        var hash = offsetBasis;
        foreach (var b in Encoding.UTF8.GetBytes(value))
        {
            hash ^= b;
            hash *= prime;
        }

        return hash;
    }

    private static ulong Mix64(ulong value)
    {
        // splitmix64 finalizer
        value += 0x9E3779B97F4A7C15UL;
        value = (value ^ (value >> 30)) * 0xBF58476D1CE4E5B9UL;
        value = (value ^ (value >> 27)) * 0x94D049BB133111EBUL;
        return value ^ (value >> 31);
    }
}
