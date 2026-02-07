using MFBC.Core;
using Xunit;

namespace MFBC.Cli.Tests;

public sealed class SessionStoreTests
{
    [Fact]
    public void SaveAndLoad_RoundTripAcrossInvocations_Works()
    {
        using var scope = new TemporaryWorkingDirectoryScope();
        var initial = DeterministicBoardStateInitializer.CreateMinimal(seed: 12UL);

        SessionStore.Save(initial);
        var loaded = LoadOrThrow();

        Assert.Equal(
            GameStateSnapshotSerializer.Serialize(initial),
            GameStateSnapshotSerializer.Serialize(loaded));
    }

    [Fact]
    public void Save_Twice_ReplacesPersistedSession()
    {
        using var scope = new TemporaryWorkingDirectoryScope();
        var first = DeterministicBoardStateInitializer.CreateMinimal(seed: 1UL);
        var second = DeterministicBoardStateInitializer.CreateMinimal(seed: 2UL);

        SessionStore.Save(first);
        SessionStore.Save(second);

        var loaded = LoadOrThrow();
        Assert.Equal(
            GameStateSnapshotSerializer.Serialize(second),
            GameStateSnapshotSerializer.Serialize(loaded));
    }

    [Fact]
    public void TryLoad_MissingSessionFile_ReturnsGuidance()
    {
        using var scope = new TemporaryWorkingDirectoryScope();

        var ok = SessionStore.TryLoad(out var state, out var error);

        Assert.False(ok);
        Assert.Null(state);
        Assert.Equal("No active session. Run 'new' first.", error);
    }

    [Fact]
    public void TryLoad_CorruptSessionFile_ReturnsResetGuidance()
    {
        using var scope = new TemporaryWorkingDirectoryScope();
        var sessionDirectory = Path.Combine(Environment.CurrentDirectory, ".mfbc");
        Directory.CreateDirectory(sessionDirectory);
        File.WriteAllText(Path.Combine(sessionDirectory, "run-state.json"), "{ invalid json }");

        var ok = SessionStore.TryLoad(out var state, out var error);

        Assert.False(ok);
        Assert.Null(state);
        Assert.Equal("Active session data is invalid. Run 'new' to reset.", error);
    }

    private static GameState LoadOrThrow()
    {
        if (!SessionStore.TryLoad(out var loaded, out var errorMessage))
        {
            throw new InvalidOperationException(errorMessage);
        }

        return loaded!;
    }

    private sealed class TemporaryWorkingDirectoryScope : IDisposable
    {
        private readonly string _originalDirectory;
        private readonly string _temporaryDirectory;

        public TemporaryWorkingDirectoryScope()
        {
            _originalDirectory = Environment.CurrentDirectory;
            _temporaryDirectory = Path.Combine(Path.GetTempPath(), "mfbc-cli-tests-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_temporaryDirectory);
            Environment.CurrentDirectory = _temporaryDirectory;
        }

        public void Dispose()
        {
            Environment.CurrentDirectory = _originalDirectory;
            if (Directory.Exists(_temporaryDirectory))
            {
                Directory.Delete(_temporaryDirectory, recursive: true);
            }
        }
    }
}
