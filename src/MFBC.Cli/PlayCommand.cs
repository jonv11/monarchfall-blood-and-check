using MFBC.Core;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Linq;

namespace MFBC.Cli;

internal sealed class PlayCommand : Command<PlaySettings>
{
    public override int Execute(CommandContext context, PlaySettings settings)
    {
        if (!SessionStore.TryLoad(out var state, out var errorMessage))
        {
            AnsiConsole.MarkupLine($"[red]{errorMessage}[/]");
            return 1;
        }

        if (state is null)
        {
            AnsiConsole.MarkupLine("[red]Active session data is invalid. Run 'new' to reset.[/]");
            return 1;
        }

        var moveText = settings.Move ?? string.Empty;
        if (string.IsNullOrWhiteSpace(moveText))
        {
            if (!string.IsNullOrWhiteSpace(settings.From) && !string.IsNullOrWhiteSpace(settings.To))
            {
                moveText = $"{settings.From}{settings.To}";
            }
        }

        if (!MoveParser.TryParseMove(moveText, out var from, out var to))
        {
            AnsiConsole.MarkupLine("[red]Invalid move format. Use e2e4 or --from e2 --to e4.[/]");
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

        foreach (var gameEvent in result.Events)
        {
            if (gameEvent is MovedEvent moved)
            {
                AnsiConsole.MarkupLine($"[green]Moved[/] {FormatCoord(moved.From)} -> {FormatCoord(moved.To)}");
            }
        }

        try
        {
            SessionStore.Save(state);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to persist session:[/] {ex.Message}");
            return 1;
        }

        BoardRenderer.Render(state);
        return 0;
    }

    private static string FormatCoord(Coord coord)
    {
        var file = (char)('a' + coord.X);
        var rank = (char)('1' + coord.Y);
        return $"{file}{rank}";
    }
}

internal sealed class PlaySettings : CommandSettings
{
    [CommandArgument(0, "[move]")]
    [Description("Move in algebraic form, e.g. e2e4.")]
    public string? Move { get; init; }

    [CommandOption("--from <FROM>")]
    [Description("Origin coordinate, e.g. e2.")]
    public string? From { get; init; }

    [CommandOption("--to <TO>")]
    [Description("Destination coordinate, e.g. e4.")]
    public string? To { get; init; }
}
