using PristineIt.Domain.Tasks.Models;

namespace PristineIt.Tests.Domain;

public class PriorityTests
{
    [Fact]
    public void Create_WithValidLowLevel_ReturnsLowPriority()
    {
        // Act
        var result = Priority.Create(1);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value!.Level);
        Assert.Equal("Low", result.Value.Name);
    }

    [Fact]
    public void Create_WithValidMediumLevel_ReturnsMediumPriority()
    {
        // Act
        var result = Priority.Create(2);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value!.Level);
        Assert.Equal("Medium", result.Value.Name);
    }

    [Fact]
    public void Create_WithValidHighLevel_ReturnsHighPriority()
    {
        // Act
        var result = Priority.Create(3);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Value!.Level);
        Assert.Equal("High", result.Value.Name);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(4)]
    [InlineData(-1)]
    public void Create_WithInvalidLevel_ReturnsFailureWithErrorMessage(int invalidLevel)
    {
        // Act
        var result = Priority.Create(invalidLevel);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Invalid priority level", result.ErrorMessage!);
    }
}