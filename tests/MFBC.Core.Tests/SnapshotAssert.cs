using System.Text;
using Xunit;

namespace MFBC.Core.Tests;

internal static class SnapshotAssert
{
    public static void Matches(string snapshotName, GameState state)
    {
        var actual = NormalizeForComparison(GameStateSnapshotSerializer.Serialize(state));
        var expectedPath = GetSnapshotPath(snapshotName);
        var receivedPath = expectedPath + ".received";

        var expected = NormalizeForComparison(File.ReadAllText(expectedPath, Encoding.UTF8));
        if (string.Equals(expected, actual, StringComparison.Ordinal))
        {
            if (File.Exists(receivedPath))
            {
                File.Delete(receivedPath);
            }

            return;
        }

        File.WriteAllText(receivedPath, actual, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
        Assert.Equal(expected, actual);
    }

    private static string GetSnapshotPath(string snapshotName)
    {
        var snapshotDirectory = Path.Combine(AppContext.BaseDirectory, "snapshots");
        var snapshotPath = Path.Combine(snapshotDirectory, $"{snapshotName}.json");
        if (!File.Exists(snapshotPath))
        {
            throw new FileNotFoundException($"Snapshot file not found: {snapshotPath}");
        }

        return snapshotPath;
    }

    private static string NormalizeForComparison(string value)
    {
        var lf = value.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n');
        return lf.TrimEnd('\n');
    }
}
