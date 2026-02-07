using Xunit;

namespace MFBC.Core.Tests;

public class DeterministicRandomSourceTests
{
    [Fact]
    public void NextInt_SameSeedAndStreams_ProducesSameSequence()
    {
        var left = new DeterministicRandomSource(123456789UL);
        var right = new DeterministicRandomSource(123456789UL);

        var leftValues = new[]
        {
            left.NextInt("boardGen", 0, 1000),
            left.NextInt("boardGen", 0, 1000),
            left.NextInt("mutations", 0, 1000),
            left.NextInt("boardGen", 0, 1000),
            left.NextInt("mutations", 0, 1000),
        };

        var rightValues = new[]
        {
            right.NextInt("boardGen", 0, 1000),
            right.NextInt("boardGen", 0, 1000),
            right.NextInt("mutations", 0, 1000),
            right.NextInt("boardGen", 0, 1000),
            right.NextInt("mutations", 0, 1000),
        };

        Assert.Equal(leftValues, rightValues);
    }

    [Fact]
    public void NextInt_DifferentSeed_ProducesDifferentSequence()
    {
        var left = new DeterministicRandomSource(1UL);
        var right = new DeterministicRandomSource(2UL);

        var leftValues = new[]
        {
            left.NextInt("boardGen", 0, 1000),
            left.NextInt("boardGen", 0, 1000),
            left.NextInt("boardGen", 0, 1000),
        };

        var rightValues = new[]
        {
            right.NextInt("boardGen", 0, 1000),
            right.NextInt("boardGen", 0, 1000),
            right.NextInt("boardGen", 0, 1000),
        };

        Assert.NotEqual(leftValues, rightValues);
    }

    [Fact]
    public void NextInt_UsesIndependentNamedStreams()
    {
        var random = new DeterministicRandomSource(42UL);

        var boardGenFirst = random.NextInt("boardGen", 0, 1000);
        var combatFirst = random.NextInt("combat", 0, 1000);
        var boardGenSecond = random.NextInt("boardGen", 0, 1000);

        Assert.NotEqual(boardGenFirst, combatFirst);
        Assert.NotEqual(boardGenFirst, boardGenSecond);
    }
}
