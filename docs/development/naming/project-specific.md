# Project-Specific Conventions

## Purpose
Capture MFBC-specific conventions that go beyond general C# guidance.

## Scope
Applies to MFBC.Core, MFBC.Cli, and test projects.

## Test Arrange-Act-Assert

Use clear AAA pattern with comments:

```csharp
[Fact]
public void Example_Test()
{
    // Arrange
    var sut = new GameState();
    
    // Act
    var result = sut.GetStatus();
    
    // Assert
    Assert.NotNull(result);
}
```

## Nullable Reference Types

Enabled project-wide. Use nullability annotations correctly:

```csharp
// Non-nullable by default
public string Name { get; set; } = string.Empty;

// Nullable when appropriate
public string? OptionalDescription { get; set; }

// Null-forgiving operator when justified
public string GetValue() => _cachedValue!;
```
