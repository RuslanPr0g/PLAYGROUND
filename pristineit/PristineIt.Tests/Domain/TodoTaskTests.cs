using PristineIt.Domain.Tasks.Models;

namespace PristineIt.Tests.Domain;

public class TodoTaskTests
{
    private readonly Title _validTitle = Title.Create("Test Task").Value!;
    private readonly Priority _validPriority = Priority.Create(2).Value!;

    [Fact]
    public void Create_WithValidParameters_SetsPropertiesCorrectly()
    {
        // Act
        var result = TodoTask.Create(_validTitle, null, _validPriority);

        // Assert
        Assert.True(result.IsSuccess);
        var task = result.Value!;
        Assert.NotEqual(Guid.Empty, task.Id);
        Assert.Equal(_validTitle, task.Title);
        Assert.Null(task.Description);
        Assert.Equal(_validPriority, task.Priority);
        Assert.False(task.IsCompleted);
        Assert.Null(task.DueDate);
        Assert.Null(task.CompletedAt);
        Assert.True(DateTime.UtcNow - task.CreatedAt < TimeSpan.FromSeconds(1)); // Approximately now
        Assert.Empty(task.Tags);
    }

    [Fact]
    public void Create_WithNullTitle_ReturnsFailure()
    {
        // Act
        var result = TodoTask.Create(null!, null, _validPriority);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Title is required.", result.ErrorMessage);
    }

    [Fact]
    public void Create_WithNullPriority_ReturnsFailure()
    {
        // Act
        var result = TodoTask.Create(_validTitle, null, null!);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Priority is required.", result.ErrorMessage);
    }

    [Fact]
    public void MarkAsCompleted_WhenNotCompleted_SetsCompletedAndTimestamp()
    {
        // Arrange
        var taskResult = TodoTask.Create(_validTitle, null, _validPriority);
        var task = taskResult.Value!;

        // Act
        var result = task.MarkAsCompleted();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(task.IsCompleted);
        Assert.NotNull(task.CompletedAt);
        Assert.True(DateTime.UtcNow - task.CompletedAt < TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void MarkAsCompleted_WhenAlreadyCompleted_ReturnsFailure()
    {
        // Arrange
        var taskResult = TodoTask.Create(_validTitle, null, _validPriority);
        var task = taskResult.Value!;
        task.MarkAsCompleted(); // First completion

        // Act
        var result = task.MarkAsCompleted();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Task is already completed.", result.ErrorMessage);
    }

    [Fact]
    public void Snooze_WithFutureDate_SetsDueDate()
    {
        // Arrange
        var taskResult = TodoTask.Create(_validTitle, null, _validPriority);
        var task = taskResult.Value!;
        var futureDate = DateTime.UtcNow.AddDays(1);

        // Act
        var result = task.Snooze(futureDate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(futureDate, task.DueDate);
    }

    [Fact]
    public void Snooze_WithPastDate_ReturnsFailure()
    {
        // Arrange
        var taskResult = TodoTask.Create(_validTitle, null, _validPriority);
        var task = taskResult.Value!;
        var pastDate = DateTime.UtcNow.AddDays(-1);

        // Act
        var result = task.Snooze(pastDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Snooze date must be in the future.", result.ErrorMessage);
        Assert.Null(task.DueDate); // Unchanged
    }

    [Fact]
    public void Snooze_WithCurrentDate_ReturnsFailure()
    {
        // Arrange
        var taskResult = TodoTask.Create(_validTitle, null, _validPriority);
        var task = taskResult.Value!;
        var currentDate = DateTime.UtcNow;

        // Act
        var result = task.Snooze(currentDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Snooze date must be in the future.", result.ErrorMessage);
    }

    [Fact]
    public void AddTag_WithValidNewTag_AddsToCollection()
    {
        // Arrange
        var taskResult = TodoTask.Create(_validTitle, null, _validPriority);
        var task = taskResult.Value!;
        var tag = Tag.Create("new").Value!;

        // Act
        var result = task.AddTag(tag);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(task.Tags);
        Assert.Equal("new", task.Tags.First().Value);
    }

    [Fact]
    public void AddTag_WithNullTag_ReturnsFailure()
    {
        // Arrange
        var taskResult = TodoTask.Create(_validTitle, null, _validPriority);
        var task = taskResult.Value!;

        // Act
        var result = task.AddTag(null!);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Tag cannot be null.", result.ErrorMessage);
        Assert.Empty(task.Tags);
    }

    [Fact]
    public void AddTag_WithDuplicateTagCaseInsensitive_ReturnsFailure()
    {
        // Arrange
        var taskResult = TodoTask.Create(_validTitle, null, _validPriority);
        var task = taskResult.Value!;
        var tag1 = Tag.Create("work").Value!;
        task.AddTag(tag1);
        var tag2 = Tag.Create("Work").Value!;

        // Act
        var result = task.AddTag(tag2);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Tag 'Work' already exists.", result.ErrorMessage);
        Assert.Single(task.Tags);
    }
}