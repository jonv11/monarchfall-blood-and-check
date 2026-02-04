namespace MFBC.Core;

/// <summary>
/// Placeholder type to prove minimal project wiring.
/// </summary>
public class GameState
{
    /// <summary>
    /// Gets or sets the game version identifier.
    /// </summary>
    public string Version { get; set; } = "0.0.0";

    /// <summary>
    /// Gets a simple status message.
    /// </summary>
    public string GetStatus()
    {
        return $"Game initialized at version {Version}";
    }
}
