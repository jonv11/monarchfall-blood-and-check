using MFBC.Core;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Linq;

namespace MFBC.Cli;

internal sealed class NewCommand : Command<NewSettings>
{
    public override int Execute(CommandContext context, NewSettings settings)
    {
        var state = DemoGameFactory.Create();
        SessionStore.Set(state);

        AnsiConsole.MarkupLine("[green]Session created.[/]");
        BoardRenderer.Render(state);

        if (!settings.Interactive)
        {
            return 0;
        }

        AnsiConsole.MarkupLine("[grey]Enter commands: show, play <move>, exit[/]");
        while (true)
        {
            var line = AnsiConsole.Ask<string>("> ");
            if (string.Equals(line, "exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (string.Equals(line, "show", StringComparison.OrdinalIgnoreCase))
            {
                BoardRenderer.Render(state);
                continue;
            }

            if (line.StartsWith("play ", StringComparison.OrdinalIgnoreCase))
            {
                var moveText = line[5..].Trim();
                _ = ApplyMove(state, moveText);
                continue;
            }

            AnsiConsole.MarkupLine("[red]Unknown command.[/]");
        }

        return 0;
    }

    private static int ApplyMove(GameState state, string moveText)
    {
        if (!MoveParser.TryParseMove(moveText, out var from, out var to))
        {
            AnsiConsole.MarkupLine("[red]Invalid move format. Use e2e4.[/]");
            return 1;
        }

        var piece = state.Pieces.Values.FirstOrDefault(p => p.Position == from);
        if (piece is null)
        {
            AnsiConsole.MarkupLine("[red]No piece at origin.[/]");
            return 1;
        }

        var applier = new ActionApplier();
        var result = applier.ApplyAction(state, new MoveAction(piece.Id, from, to));
        if (!result.IsSuccess)
        {
            foreach (var error in result.Errors)
            {
                AnsiConsole.MarkupLine($"[red]{error.Code}[/]: {error.Message}");
            }

            return 1;
        }

        BoardRenderer.Render(state);
        return 0;
    }
}

internal sealed class NewSettings : CommandSettings
{
    [CommandOption("--interactive")]
    public bool Interactive { get; init; } = true;
}
