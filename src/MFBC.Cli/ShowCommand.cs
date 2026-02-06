using Spectre.Console;
using Spectre.Console.Cli;

namespace MFBC.Cli;

internal sealed class ShowCommand : Command<CommandSettings>
{
    public override int Execute(CommandContext context, CommandSettings settings)
    {
        var state = SessionStore.Current;
        if (state is null)
        {
            AnsiConsole.MarkupLine("[red]No active session. Run 'new' first.[/]");
            return 1;
        }

        BoardRenderer.Render(state);
        return 0;
    }
}
