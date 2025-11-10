using PristineIt.Domain.Tasks.Models;

namespace PristineIt.Tests.Domain;

public class TagTests
{
    [Fact]
    public void Create_WithValidNonEmptyValue_ReturnsSuccessWithTrimmedValue()
    {
        // Arrange
        const string input = " work ";

        // Act
        var result = Tag.Create(input);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("work", result.Value!.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_WithEmptyOrNullValue_ReturnsFailure(string? invalidInput)
    {
        // Act
        var result = Tag.Create(invalidInput);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Tag cannot be empty.", result.ErrorMessage);
    }

    [Fact]
    public void Create_WithValueExceedingMaxLength_ReturnsFailure()
    {
        // Arrange
        var longValue = new string('a', 21);

        // Act
        var result = Tag.Create(longValue);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Tag must be 20 characters or less.", result.ErrorMessage);
    }

    [Fact]
    public void Create_WithValueAtMaxLength_ReturnsSuccess()
    {
        // Arrange
        var maxLengthValue = new string('a', 20);

        // Act
        var result = Tag.Create(maxLengthValue);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(maxLengthValue, result.Value!.Value);
    }
}