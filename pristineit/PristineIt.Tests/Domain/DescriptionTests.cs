using PristineIt.Domain.Tasks.Models;

namespace PristineIt.Tests.Domain;

public class DescriptionTests
{
    [Fact]
    public void Create_WithValidValue_ReturnsSuccessWithTrimmedValue()
    {
        // Arrange
        const string input = " Detailed description ";

        // Act
        var result = Description.Create(input);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Detailed description", result.Value!.Value);
    }

    [Fact]
    public void Create_WithNullValue_ReturnsSuccessWithNull()
    {
        // Act
        var result = Description.Create(null);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Value!.Value);
    }

    [Fact]
    public void Create_WithEmptyValue_ReturnsSuccessWithNull()
    {
        // Act
        var result = Description.Create("");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Value!.Value);
    }

    [Fact]
    public void Create_WithValueExceedingMaxLength_ReturnsFailure()
    {
        // Arrange
        var longValue = new string('a', 501);

        // Act
        var result = Description.Create(longValue);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Description cannot exceed 500 characters.", result.ErrorMessage);
    }

    [Fact]
    public void Create_WithValueAtMaxLength_ReturnsSuccess()
    {
        // Arrange
        var maxLengthValue = new string('a', 500);

        // Act
        var result = Description.Create(maxLengthValue);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(maxLengthValue, result.Value!.Value);
    }
}
