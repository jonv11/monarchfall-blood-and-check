using MFBC.Core;
using System.Text.Json;

namespace MFBC.Cli;

internal static class SessionStore
{
    private const string SessionDirectoryName = ".mfbc";
    private const string SessionFileName = "run-state.json";
    private const int SessionSchemaVersion = 1;

    public static void Save(GameState state)
    {
        if (state is null)
        {
            throw new ArgumentNullException(nameof(state));
        }

        var sessionFilePath = GetSessionFilePath();
        Directory.CreateDirectory(Path.GetDirectoryName(sessionFilePath)!);

        var model = SessionFileModel.FromState(state);
        var json = JsonSerializer.Serialize(
            model,
            new JsonSerializerOptions
            {
                WriteIndented = true,
            });

        File.WriteAllText(sessionFilePath, json);
    }

    public static bool TryLoad(out GameState? state, out string errorMessage)
    {
        state = null;
        var sessionFilePath = GetSessionFilePath();
        if (!File.Exists(sessionFilePath))
        {
            errorMessage = "No active session. Run 'new' first.";
            return false;
        }

        try
        {
            var json = File.ReadAllText(sessionFilePath);
            var model = JsonSerializer.Deserialize<SessionFileModel>(json);
            if (model is null)
            {
                errorMessage = "Active session data is invalid. Run 'new' to reset.";
                return false;
            }

            state = model.ToState();
            errorMessage = string.Empty;
            return true;
        }
        catch (Exception)
        {
            errorMessage = "Active session data is invalid. Run 'new' to reset.";
            return false;
        }
    }

    private static string GetSessionFilePath()
    {
        return Path.Combine(Environment.CurrentDirectory, SessionDirectoryName, SessionFileName);
    }

    private sealed class SessionFileModel
    {
        public int SchemaVersion { get; set; }

        public string Version { get; set; } = string.Empty;

        public List<SessionTileModel> Tiles { get; set; } = [];

        public List<SessionPieceModel> Pieces { get; set; } = [];

        public static SessionFileModel FromState(GameState state)
        {
            return new SessionFileModel
            {
                SchemaVersion = SessionSchemaVersion,
                Version = state.Version,
                Tiles = state.Board.Tiles.Values
                    .OrderBy(tile => tile.Coord.Y)
                    .ThenBy(tile => tile.Coord.X)
                    .Select(tile => new SessionTileModel
                    {
                        X = tile.Coord.X,
                        Y = tile.Coord.Y,
                        Enterable = tile.Enterable,
                    })
                    .ToList(),
                Pieces = state.Pieces.Values
                    .OrderBy(piece => piece.Id)
                    .Select(piece => new SessionPieceModel
                    {
                        Id = piece.Id.ToString("D"),
                        Type = piece.Type.ToString(),
                        Side = piece.Side.ToString(),
                        X = piece.Position.X,
                        Y = piece.Position.Y,
                        IsAlive = piece.IsAlive,
                    })
                    .ToList(),
            };
        }

        public GameState ToState()
        {
            if (SchemaVersion != SessionSchemaVersion)
            {
                throw new InvalidOperationException("Unsupported session schema version.");
            }

            var board = new Board();
            foreach (var tile in Tiles.OrderBy(t => t.Y).ThenBy(t => t.X))
            {
                board.AddTile(new Tile(new Coord(tile.X, tile.Y), tile.Enterable));
            }

            var state = new GameState(board);
            foreach (var piece in Pieces.OrderBy(p => p.Id, StringComparer.Ordinal))
            {
                if (!piece.IsAlive)
                {
                    continue;
                }

                if (!Enum.TryParse<PieceType>(piece.Type, ignoreCase: false, out var pieceType))
                {
                    throw new InvalidOperationException($"Unknown piece type '{piece.Type}'.");
                }

                if (!Enum.TryParse<Side>(piece.Side, ignoreCase: false, out var side))
                {
                    throw new InvalidOperationException($"Unknown side '{piece.Side}'.");
                }

                if (!Guid.TryParse(piece.Id, out var pieceId) || pieceId == Guid.Empty)
                {
                    throw new InvalidOperationException("Invalid piece id.");
                }

                state.AddPiece(new Piece(pieceId, pieceType, side, new Coord(piece.X, piece.Y)));
            }

            return state;
        }
    }

    private sealed class SessionTileModel
    {
        public int X { get; set; }

        public int Y { get; set; }

        public bool Enterable { get; set; }
    }

    private sealed class SessionPieceModel
    {
        public string Id { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Side { get; set; } = string.Empty;

        public int X { get; set; }

        public int Y { get; set; }

        public bool IsAlive { get; set; }
    }
}
