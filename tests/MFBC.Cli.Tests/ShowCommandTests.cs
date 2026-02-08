using MFBC.Core;
using Spectre.Console.Cli;
using Xunit;

namespace MFBC.Cli.Tests;

public sealed class ShowCommandTests
{
    [Fact]
    public void ShowCommand_CommandAppInvocation_WithPersistedSession_ReturnsSuccess()
    {
        using var scope = new TemporaryWorkingDirectoryScope();
        SessionStore.Save(DeterministicBoardStateInitializer.CreateMinimal(seed: 7UL));

        var app = new CommandApp();
        app.Configure(config =>
        {
            config.SetApplicationName("mfbc");
            config.AddCommand<ShowCommand>("show");
        });

        var exitCode = app.Run(["show"]);

        Assert.Equal(0, exitCode);
    }

    private sealed class TemporaryWorkingDirectoryScope : IDisposable
    {
        private readonly string _originalDirectory;
        private readonly string _temporaryDirectory;

        public TemporaryWorkingDirectoryScope()
        {
            _originalDirectory = Environment.CurrentDirectory;
            _temporaryDirectory = Path.Combine(Path.GetTempPath(), "mfbc-cli-show-tests-" + Guid.NewGuid().ToString("N"));
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
