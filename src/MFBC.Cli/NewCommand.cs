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
        try
        {
            SessionStore.Save(state);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to persist session:[/] {ex.Message}");
            return 1;
        }

        AnsiConsole.MarkupLine("[green]Session created.[/]");
        BoardRenderer.Render(state);

        if (settings.NoInteractive || !settings.Interactive)
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
                _ = ApplyMove(state, moveText, interactive: true);
                continue;
            }

            AnsiConsole.MarkupLine("[red]Unknown command.[/]");
        }

        return 0;
    }

    private static int ApplyMove(GameState state, string moveText, bool interactive)
    {
        if (!MoveParser.TryParseMove(moveText, out var from, out var to))
        {
            AnsiConsole.MarkupLine("[red]Invalid move format. Use e2e4.[/]");
            return interactive ? 0 : 1;
        }

        var piece = state.Pieces.Values.FirstOrDefault(p => p.Position == from);
        if (piece is null)
        {
            AnsiConsole.MarkupLine("[red]No piece at origin.[/]");
            return interactive ? 0 : 1;
        }

        var applier = new ActionApplier();
        var result = applier.ApplyAction(state, new MoveAction(piece.Id, from, to));
        if (!result.IsSuccess)
        {
            foreach (var error in result.Errors)
            {
                AnsiConsole.MarkupLine($"[red]{error.Code}[/]: {error.Message}");
            }

            return interactive ? 0 : 1;
        }

        foreach (var gameEvent in result.Events)
        {
            if (gameEvent is MovedEvent moved)
            {
                AnsiConsole.MarkupLine($"[green]Moved[/] {FormatCoord(moved.From)} -> {FormatCoord(moved.To)}");
            }
        }

        BoardRenderer.Render(state);
        try
        {
            SessionStore.Save(state);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to persist session:[/] {ex.Message}");
            return interactive ? 0 : 1;
        }

        return 0;
    }

    private static string FormatCoord(Coord coord)
    {
        var file = (char)('a' + coord.X);
        var rank = (char)('1' + coord.Y);
        return $"{file}{rank}";
    }
}

internal sealed class NewSettings : CommandSettings
{
    [CommandOption("--interactive")]
    public bool Interactive { get; init; }

    [CommandOption("--no-interactive")]
    public bool NoInteractive { get; init; }
}
