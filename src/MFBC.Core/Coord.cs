namespace MFBC.Core;

/// <summary>
/// Represents an integer coordinate on a sparse board. Coordinates may be negative.
/// </summary>
public readonly record struct Coord(int X, int Y)
{
    /// <summary>
    /// Returns a compact string representation like (x,y).
    /// </summary>
    public override string ToString() => $"({X},{Y})";
}
