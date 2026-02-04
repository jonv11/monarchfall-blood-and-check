using Xunit;

namespace MFBC.Core.Tests;

public class GameStateTests
{
    [Fact]
    public void GameState_GetStatus_ReturnsMessageWithVersion()
    {
        // Arrange
        var gameState = new GameState { Version = "0.0.0" };

        // Act
        var status = gameState.GetStatus();

        // Assert
        Assert.NotNull(status);
        Assert.Contains("0.0.0", status);
    }
}
