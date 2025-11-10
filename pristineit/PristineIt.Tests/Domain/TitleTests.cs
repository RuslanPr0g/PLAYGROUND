using PristineIt.Domain.Tasks.Models;

namespace PristineIt.Tests.Domain;

public class TitleTests
{
    [Fact]
    public void Create_WithValidNonEmptyValue_ReturnsSuccessWithTrimmedValue()
    {
        // Arrange
        const string input = " Task Title ";

        // Act
        var result = Title.Create(input);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Task Title", result.Value!.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyOrNullValue_ReturnsFailure(string? invalidInput)
    {
        // Act
        var result = Title.Create(invalidInput);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Title is required.", result.ErrorMessage);
    }

    [Fact]
    public void Create_WithValueExceedingMaxLength_ReturnsFailure()
    {
        // Arrange
        var longValue = new string('a', 101);

        // Act
        var result = Title.Create(longValue);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Title cannot exceed 100 characters.", result.ErrorMessage);
    }

    [Fact]
    public void Create_WithValueAtMaxLength_ReturnsSuccess()
    {
        // Arrange
        var maxLengthValue = new string('a', 100);

        // Act
        var result = Title.Create(maxLengthValue);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(maxLengthValue, result.Value!.Value);
    }
}