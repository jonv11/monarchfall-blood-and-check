using MFBC.Core;

namespace MFBC.Cli;

internal static class SessionStore
{
    private static GameState? _state;

    public static GameState? Current => _state;

    public static void Set(GameState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }
}
