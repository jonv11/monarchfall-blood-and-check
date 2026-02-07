using NetArchTest.Rules;
using Xunit;

namespace MFBC.Core.Tests;

public class ArchitectureBoundaryTests
{
    [Fact]
    public void CoreAssembly_ShouldNotDependOnCliLayer()
    {
        var result = Types
            .InAssembly(typeof(GameState).Assembly)
            .Should()
            .NotHaveDependencyOn("MFBC.Cli")
            .GetResult();

        AssertArchitectureRule(result);
    }

    [Theory]
    [InlineData("System.IO")]
    [InlineData("System.Net")]
    [InlineData("System.Console")]
    public void CoreAssembly_ForbiddenDependency_ShouldNotExist(string forbiddenDependency)
    {
        var result = Types
            .InAssembly(typeof(GameState).Assembly)
            .Should()
            .NotHaveDependencyOn(forbiddenDependency)
            .GetResult();

        AssertArchitectureRule(result);
    }

    private static void AssertArchitectureRule(TestResult result)
    {
        var realFailingTypes = (result.FailingTypeNames ?? [])
            .Where(typeName => !typeName.StartsWith("Coverlet.Core.Instrumentation.Tracker.", StringComparison.Ordinal))
            .ToArray();

        Assert.True(
            realFailingTypes.Length == 0,
            $"Architecture rule failed for types: {string.Join(", ", realFailingTypes)}");
    }
}
