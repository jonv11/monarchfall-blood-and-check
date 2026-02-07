using Spectre.Console;
using Spectre.Console.Cli;

namespace MFBC.Cli;

internal sealed class ShowCommand : Command<CommandSettings>
{
    public override int Execute(CommandContext context, CommandSettings settings)
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

        BoardRenderer.Render(state);
        return 0;
    }
}
