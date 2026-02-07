using System.Buffers;
using System.Text;
using System.Text.Json;
using Xunit;

namespace MFBC.Core.Tests;

public sealed class GoldenRunTests
{
    private static readonly Guid WhiteRookId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid BlackKingId = Guid.Parse("22222222-2222-2222-2222-222222222222");

    [Fact]
    public void FixedSeedRun_RepeatedExecutions_ProduceIdenticalSnapshotAndEventTrace()
    {
        var first = ExecuteGoldenRun();
        var second = ExecuteGoldenRun();

        Assert.Equal(
            GameStateSnapshotSerializer.Serialize(first.State),
            GameStateSnapshotSerializer.Serialize(second.State));

        Assert.Equal(
            SerializeEventTrace(first.Events),
            SerializeEventTrace(second.Events));
    }

    [Fact]
    public void FixedSeedRun_MatchesGoldenSnapshotAndEventTrace()
    {
        var result = ExecuteGoldenRun();

        SnapshotAssert.Matches("golden-run-seed-9-final-state", result.State);
        AssertGoldenJson("golden-run-seed-9-events", SerializeEventTrace(result.Events));
    }

    [Fact]
    public void FixedSeedRun_EventOrder_MatchesExpectedSequence()
    {
        var result = ExecuteGoldenRun();

        Assert.Collection(
            result.Events,
            e =>
            {
                var moved = Assert.IsType<MovedEvent>(e);
                Assert.Equal(BlackKingId, moved.PieceId);
                Assert.Equal(new Coord(7, 5), moved.From);
                Assert.Equal(new Coord(6, 5), moved.To);
            },
            e =>
            {
                var moved = Assert.IsType<MovedEvent>(e);
                Assert.Equal(WhiteRookId, moved.PieceId);
                Assert.Equal(new Coord(7, 7), moved.From);
                Assert.Equal(new Coord(7, 5), moved.To);
            },
            e =>
            {
                var moved = Assert.IsType<MovedEvent>(e);
                Assert.Equal(WhiteRookId, moved.PieceId);
                Assert.Equal(new Coord(7, 5), moved.From);
                Assert.Equal(new Coord(6, 5), moved.To);
            });
    }

    private static (GameState State, IReadOnlyList<GameEvent> Events) ExecuteGoldenRun()
    {
        var state = DeterministicBoardStateInitializer.CreateMinimal(seed: 9UL, width: 8, height: 8);
        var applier = new ActionApplier();
        var events = new List<GameEvent>();

        var actions = new[]
        {
            new MoveAction(BlackKingId, new Coord(7, 5), new Coord(6, 5)),
            new MoveAction(WhiteRookId, new Coord(7, 7), new Coord(7, 5)),
            new MoveAction(WhiteRookId, new Coord(7, 5), new Coord(6, 5)),
        };

        foreach (var action in actions)
        {
            var result = applier.ApplyAction(state, action);
            Assert.True(
                result.IsSuccess,
                $"Expected successful golden action {action.From} -> {action.To} but got: {string.Join(", ", result.Errors.Select(e => $"{e.Code}:{e.Message}"))}");
            events.AddRange(result.Events);
        }

        return (state, events);
    }

    private static string SerializeEventTrace(IReadOnlyList<GameEvent> events)
    {
        var buffer = new ArrayBufferWriter<byte>();
        using var writer = new Utf8JsonWriter(buffer, new JsonWriterOptions { Indented = true });
        writer.WriteStartArray();
        foreach (var gameEvent in events)
        {
            writer.WriteStartObject();
            writer.WriteString("kind", gameEvent.Kind.ToString());
            if (gameEvent is MovedEvent moved)
            {
                writer.WriteString("pieceId", moved.PieceId.ToString("D"));
                writer.WriteNumber("fromX", moved.From.X);
                writer.WriteNumber("fromY", moved.From.Y);
                writer.WriteNumber("toX", moved.To.X);
                writer.WriteNumber("toY", moved.To.Y);
            }

            writer.WriteEndObject();
        }

        writer.WriteEndArray();
        writer.Flush();
        var text = Encoding.UTF8.GetString(buffer.WrittenSpan);
        return text.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');
    }

    private static void AssertGoldenJson(string snapshotName, string actual)
    {
        var expectedPath = Path.Combine(AppContext.BaseDirectory, "snapshots", $"{snapshotName}.json");
        var receivedPath = expectedPath + ".received";
        var expected = NormalizeForComparison(File.ReadAllText(expectedPath, Encoding.UTF8));
        var normalizedActual = NormalizeForComparison(actual);

        if (string.Equals(expected, normalizedActual, StringComparison.Ordinal))
        {
            if (File.Exists(receivedPath))
            {
                File.Delete(receivedPath);
            }

            return;
        }

        File.WriteAllText(receivedPath, normalizedActual, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
        Assert.Equal(expected, normalizedActual);
    }

    private static string NormalizeForComparison(string value)
    {
        var lf = value.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');
        return lf.TrimEnd('\n');
    }
}
