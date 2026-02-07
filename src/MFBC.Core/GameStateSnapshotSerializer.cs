using System.Text;
using System.Text.Json;
using System.Buffers;

namespace MFBC.Core;

/// <summary>
/// Serializes <see cref="GameState"/> into a deterministic JSON snapshot for tests.
/// </summary>
/// <remarks>
/// Determinism contract:
/// - UTF-8 encoded JSON.
/// - LF line endings produced by <see cref="Utf8JsonWriter"/>.
/// - Stable property order and collection ordering.
/// - Invariant formatting through JSON numeric/string writers.
/// - Includes only gameplay-relevant state used by determinism assertions.
/// </remarks>
public static class GameStateSnapshotSerializer
{
    /// <summary>
    /// Serializes a game state to deterministic UTF-8 JSON bytes.
    /// </summary>
    /// <param name="state">Game state to serialize.</param>
    /// <returns>Canonical snapshot bytes.</returns>
    public static byte[] SerializeToUtf8(GameState state)
    {
        if (state is null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        var snapshot = SnapshotBuilder.Build(state);
        var buffer = new ArrayBufferWriter<byte>();
        using var writer = new Utf8JsonWriter(
            buffer,
            new JsonWriterOptions
            {
                Indented = true,
            });

        writer.WriteStartObject();
        writer.WriteString("version", snapshot.Version);

        writer.WritePropertyName("tiles");
        writer.WriteStartArray();
        foreach (var tile in snapshot.Tiles)
        {
            writer.WriteStartObject();
            writer.WriteNumber("x", tile.X);
            writer.WriteNumber("y", tile.Y);
            writer.WriteBoolean("enterable", tile.Enterable);
            if (tile.OccupantId is null)
            {
                writer.WriteNull("occupantId");
            }
            else
            {
                writer.WriteString("occupantId", tile.OccupantId);
            }

            writer.WriteEndObject();
        }

        writer.WriteEndArray();

        writer.WritePropertyName("pieces");
        writer.WriteStartArray();
        foreach (var piece in snapshot.Pieces)
        {
            writer.WriteStartObject();
            writer.WriteString("id", piece.Id);
            writer.WriteString("type", piece.Type);
            writer.WriteString("side", piece.Side);
            writer.WriteNumber("x", piece.X);
            writer.WriteNumber("y", piece.Y);
            writer.WriteBoolean("isAlive", piece.IsAlive);
            writer.WriteEndObject();
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Flush();
        var json = Encoding.UTF8.GetString(buffer.WrittenSpan);
        var normalized = NormalizeLineEndings(json);
        return Encoding.UTF8.GetBytes(normalized);
    }

    /// <summary>
    /// Serializes a game state to deterministic JSON text.
    /// </summary>
    /// <param name="state">Game state to serialize.</param>
    /// <returns>Canonical snapshot JSON.</returns>
    public static string Serialize(GameState state)
    {
        return NormalizeLineEndings(Encoding.UTF8.GetString(SerializeToUtf8(state)));
    }

    private static class SnapshotBuilder
    {
        public static GameStateSnapshot Build(GameState state)
        {
            var tiles = state.Board.Tiles.Values
                .OrderBy(tile => tile.Coord.Y)
                .ThenBy(tile => tile.Coord.X)
                .Select(tile => new TileSnapshot(
                    tile.Coord.X,
                    tile.Coord.Y,
                    tile.Enterable,
                    tile.OccupantId?.ToString("D")))
                .ToArray();

            var pieces = state.Pieces.Values
                .OrderBy(piece => piece.Id)
                .Select(piece => new PieceSnapshot(
                    piece.Id.ToString("D"),
                    piece.Type.ToString(),
                    piece.Side.ToString(),
                    piece.Position.X,
                    piece.Position.Y,
                    piece.IsAlive))
                .ToArray();

            return new GameStateSnapshot(NormalizeString(state.Version), tiles, pieces);
        }

        private static string NormalizeString(string value)
        {
            return value.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');
        }
    }

    private sealed record GameStateSnapshot(string Version, TileSnapshot[] Tiles, PieceSnapshot[] Pieces);

    private sealed record TileSnapshot(int X, int Y, bool Enterable, string? OccupantId);

    private sealed record PieceSnapshot(string Id, string Type, string Side, int X, int Y, bool IsAlive);

    private static string NormalizeLineEndings(string value)
    {
        return value.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');
    }
}
