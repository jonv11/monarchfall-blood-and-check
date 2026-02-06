using Spectre.Console.Cli;

namespace MFBC.Cli;

internal static class Program
{
    private static int Main(string[] args)
    {
        var app = new CommandApp();
        app.Configure(config =>
        {
            config.SetApplicationName("mfbc");
            config.ValidateExamples();
            config.AddCommand<NewCommand>("new")
                .WithDescription("Start a new session.")
                .WithExample(["new"]);
            config.AddCommand<ShowCommand>("show")
                .WithDescription("Show the current board.")
                .WithExample(["show"]);
            config.AddCommand<PlayCommand>("play")
                .WithDescription("Apply a move like e2e4 or --from e2 --to e4.")
                .WithExample(["play", "e2e4"]);
        });

        return app.Run(args);
    }
}
