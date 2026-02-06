using System.Collections.ObjectModel;

namespace MFBC.Core;

/// <summary>
/// Represents a sparse board of tiles.
/// </summary>
public sealed class Board
{
    private readonly Dictionary<Coord, Tile> _tiles = new();

    /// <summary>
    /// Gets a read-only view of tiles by coordinate.
    /// </summary>
    public IReadOnlyDictionary<Coord, Tile> Tiles => new ReadOnlyDictionary<Coord, Tile>(_tiles);

    /// <summary>
    /// Adds a tile to the board.
    /// </summary>
    /// <param name="tile">Tile to add.</param>
    public void AddTile(Tile tile)
    {
        if (tile is null)
        {
            throw new ArgumentNullException(nameof(tile));
        }

        if (_tiles.ContainsKey(tile.Coord))
        {
            throw new InvalidOperationException($"Tile already exists at {tile.Coord}.");
        }

        _tiles[tile.Coord] = tile;
    }

    /// <summary>
    /// Removes an existing tile, if unoccupied.
    /// </summary>
    /// <param name="coord">Coordinate of the tile to remove.</param>
    public void RemoveTile(Coord coord)
    {
        if (!_tiles.TryGetValue(coord, out var tile))
        {
            throw new InvalidOperationException($"Tile does not exist at {coord}.");
        }

        if (tile.IsOccupied)
        {
            throw new InvalidOperationException($"Cannot remove occupied tile at {coord}.");
        }

        _tiles.Remove(coord);
    }

    /// <summary>
    /// Returns true if a tile exists at the given coordinate.
    /// </summary>
    public bool HasTile(Coord coord) => _tiles.ContainsKey(coord);

    /// <summary>
    /// Gets a tile at the given coordinate.
    /// </summary>
    public Tile GetTile(Coord coord)
    {
        if (!_tiles.TryGetValue(coord, out var tile))
        {
            throw new InvalidOperationException($"Tile does not exist at {coord}.");
        }

        return tile;
    }

    /// <summary>
    /// Places a piece on a tile, enforcing existence and occupancy rules.
    /// </summary>
    /// <param name="piece">Piece to place.</param>
    /// <param name="allowNonEnterable">Whether placement may ignore enterability.</param>
    public void PlacePiece(Piece piece, bool allowNonEnterable = false)
    {
        if (piece is null)
        {
            throw new ArgumentNullException(nameof(piece));
        }

        var tile = GetTile(piece.Position);

        if (!allowNonEnterable && !tile.Enterable)
        {
            throw new InvalidOperationException($"Tile at {tile.Coord} is not enterable.");
        }

        if (tile.IsOccupied)
        {
            throw new InvalidOperationException($"Tile at {tile.Coord} is already occupied.");
        }

        tile.SetOccupant(piece.Id);
    }

    /// <summary>
    /// Moves a piece to a new coordinate, enforcing tile existence and occupancy rules.
    /// </summary>
    /// <param name="piece">Piece to move.</param>
    /// <param name="to">Destination coordinate.</param>
    public void MovePiece(Piece piece, Coord to)
    {
        if (piece is null)
        {
            throw new ArgumentNullException(nameof(piece));
        }

        var fromTile = GetTile(piece.Position);
        var toTile = GetTile(to);

        // Enterability is enforced by higher-level legality rules, not by low-level mutation.
        if (toTile.IsOccupied)
        {
            throw new InvalidOperationException($"Destination tile at {to} is already occupied.");
        }

        fromTile.SetOccupant(null);
        toTile.SetOccupant(piece.Id);
        piece.SetPosition(to);
    }
}
