using MFBC.Core;

namespace MFBC.Cli;

internal static class DemoGameFactory
{
    public static GameState Create()
    {
        return DeterministicBoardStateInitializer.CreateMinimal(seed: 0);
    }
}
