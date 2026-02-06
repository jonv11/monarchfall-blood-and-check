namespace MFBC.Core;

/// <summary>
/// Represents a tile on the board.
/// </summary>
public sealed class Tile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tile"/> class.
    /// </summary>
    /// <param name="coord">Tile coordinate.</param>
    /// <param name="enterable">Whether a standard move may end on this tile.</param>
    public Tile(Coord coord, bool enterable = true)
    {
        Coord = coord;
        Enterable = enterable;
    }

    /// <summary>
    /// Gets the coordinate of this tile.
    /// </summary>
    public Coord Coord { get; }

    /// <summary>
    /// Gets a value indicating whether the tile is enterable by a standard move.
    /// </summary>
    public bool Enterable { get; }

    /// <summary>
    /// Gets the occupying piece identifier, if any.
    /// </summary>
    public Guid? OccupantId { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the tile is occupied.
    /// </summary>
    public bool IsOccupied => OccupantId.HasValue;

    internal void SetOccupant(Guid? pieceId)
    {
        OccupantId = pieceId;
    }
}
