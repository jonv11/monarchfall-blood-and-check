# Test Naming

## Purpose
Define naming patterns for test classes and test methods.

## Scope
Applies to tests under tests/ and any test utilities.

## Test Class Naming

```csharp
// Pattern: <ClassUnderTest>Tests
public class GameStateTests { }
public class MovementValidatorTests { }
```

## Test Method Naming

Use the **MethodName_Scenario_ExpectedBehavior** pattern:

```csharp
[Fact]
public void MovePiece_ValidMove_UpdatesPosition()
{
    // Arrange, Act, Assert
}

[Fact]
public void IsValidPosition_OutOfBounds_ReturnsFalse()
{
    // Arrange, Act, Assert
}

[Fact]
public void ApplyMove_NullMove_ThrowsArgumentNullException()
{
    // Arrange, Act, Assert
}
```

## Pattern Breakdown

```
<MethodName>_<Scenario>_<ExpectedBehavior>
```

- **MethodName:** The method being tested
- **Scenario:** The input condition or state being tested
- **ExpectedBehavior:** What should happen

## Additional Examples

```csharp
[Fact]
public void GetStatus_GameInitialized_ReturnsVersionString() { }

[Fact]
public void Constructor_NullValidator_ThrowsArgumentNullException() { }

[Theory]
[InlineData(0, 0, true)]
[InlineData(8, 8, false)]
public void IsInBounds_VariousPositions_ReturnsExpectedResult(int x, int y, bool expected)
{
    // ...
}
```
