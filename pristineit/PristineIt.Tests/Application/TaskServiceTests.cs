using Moq;
using PristineIt.Application.Services;
using PristineIt.Domain.Tasks.DTOs;
using PristineIt.Domain.Tasks.Models;
using PristineIt.Domain.Tasks.Repositories;

namespace PristineIt.Tests.Application;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _repositoryMock = new();
    private readonly TaskService _service;

    public TaskServiceTests()
    {
        _service = new TaskService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateTask_WithValidDto_CreatesAndSavesTaskReturningId()
    {
        // Arrange
        var dto = new CreateTaskDto("New Task", "Desc", 2);
        _repositoryMock.Setup(r => r.Add(It.IsAny<TodoTask>())).Returns(
            Task.FromResult(TodoTask.Create(
                Title.Create("New Task").Value!,
                Description.Create("Desc").Value,
                Priority.Create(2).Value!).Value)!);
        _repositoryMock.Setup(r => r.SaveChanges()).Returns(Task.FromResult(true));

        // Act
        var result = await _service.CreateTask(dto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
        _repositoryMock.Verify(r => r.Add(It.Is<TodoTask>(t =>
            t.Title.Value == "New Task" &&
            t.Description!.Value == "Desc" &&
            t.Priority.Level == 2)), Times.Once);
        _repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task CreateTask_WithInvalidTitle_ReturnsFailureWithoutRepositoryCalls()
    {
        // Arrange
        var dto = new CreateTaskDto("", null, 2);

        // Act
        var result = await _service.CreateTask(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Title is required.", result.ErrorMessage);
        _repositoryMock.Verify(r => r.Add(It.IsAny<TodoTask>()), Times.Never);
        _repositoryMock.Verify(r => r.SaveChanges(), Times.Never);
    }

    [Fact]
    public async Task CreateTask_WithInvalidDescription_ReturnsFailureWithoutRepositoryCalls()
    {
        // Arrange
        var dto = new CreateTaskDto("Task", new string('a', 501), 2);

        // Act
        var result = await _service.CreateTask(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Description cannot exceed 500 characters.", result.ErrorMessage);
        _repositoryMock.Verify(r => r.Add(It.IsAny<TodoTask>()), Times.Never);
    }

    [Fact]
    public async Task CreateTask_WithInvalidPriority_ReturnsFailureWithoutRepositoryCalls()
    {
        // Arrange
        var dto = new CreateTaskDto("Task", null, 4);

        // Act
        var result = await _service.CreateTask(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Invalid priority level", result.ErrorMessage!);
        _repositoryMock.Verify(r => r.Add(It.IsAny<TodoTask>()), Times.Never);
    }

    [Fact]
    public async Task AddTagToTask_WithValidDtoAndExistingTask_AddsTagAndSaves()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var dto = new AddTagToTaskDto(taskId, "new");
        var existingTask = TodoTask.Create(Title.Create("Task").Value!, null, Priority.Create(2).Value!).Value!;
        _repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);
        _repositoryMock.Setup(r => r.SaveChanges()).Returns(Task.FromResult(true));

        // Act
        var result = await _service.AddTagToTask(dto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(existingTask.Tags);
        Assert.Equal("new", existingTask.Tags.First().Value);
        _repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task AddTagToTask_WithNonExistingTask_ReturnsFailureWithoutSave()
    {
        // Arrange
        var dto = new AddTagToTaskDto(Guid.NewGuid(), "new");
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((TodoTask?)null);

        // Act
        var result = await _service.AddTagToTask(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Task not found.", result.ErrorMessage);
        _repositoryMock.Verify(r => r.SaveChanges(), Times.Never);
    }

    [Fact]
    public async Task AddTagToTask_WithInvalidTag_ReturnsFailureWithoutSave()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var dto = new AddTagToTaskDto(taskId, "");
        var existingTask = TodoTask.Create(Title.Create("Task").Value!, null, Priority.Create(2).Value!).Value!;
        _repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);

        // Act
        var result = await _service.AddTagToTask(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Tag cannot be empty.", result.ErrorMessage);
        _repositoryMock.Verify(r => r.SaveChanges(), Times.Never);
    }

    [Fact]
    public async Task MarkTaskAsCompleted_WithValidDtoAndExistingTask_MarksAndSaves()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var dto = new MarkTaskAsCompletedDto(taskId);
        var existingTask = TodoTask.Create(Title.Create("Task").Value!, null, Priority.Create(2).Value!).Value!;
        _repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);
        _repositoryMock.Setup(r => r.SaveChanges()).Returns(Task.FromResult(true));

        // Act
        var result = await _service.MarkTaskAsCompleted(dto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(existingTask.IsCompleted);
        Assert.NotNull(existingTask.CompletedAt);
        _repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task MarkTaskAsCompleted_WithAlreadyCompletedTask_ReturnsFailureWithoutSave()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var dto = new MarkTaskAsCompletedDto(taskId);
        var existingTask = TodoTask.Create(Title.Create("Task").Value!, null, Priority.Create(2).Value!).Value!;
        existingTask.MarkAsCompleted();
        _repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);

        // Act
        var result = await _service.MarkTaskAsCompleted(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Task is already completed.", result.ErrorMessage);
        _repositoryMock.Verify(r => r.SaveChanges(), Times.Never);
    }

    [Fact]
    public async Task SnoozeTask_WithValidFutureDateAndExistingTask_SnoozesAndSaves()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var futureDate = DateTime.UtcNow.AddDays(1);
        var dto = new SnoozeTaskDto(taskId, futureDate);
        var existingTask = TodoTask.Create(Title.Create("Task").Value!, null, Priority.Create(2).Value!).Value!;
        _repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);
        _repositoryMock.Setup(r => r.SaveChanges()).Returns(Task.FromResult(true));

        // Act
        var result = await _service.SnoozeTask(dto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(futureDate, existingTask.DueDate);
        _repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task SnoozeTask_WithPastDate_ReturnsFailureWithoutSave()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var pastDate = DateTime.UtcNow.AddDays(-1);
        var dto = new SnoozeTaskDto(taskId, pastDate);
        var existingTask = TodoTask.Create(Title.Create("Task").Value!, null, Priority.Create(2).Value!).Value!;
        _repositoryMock.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(existingTask);

        // Act
        var result = await _service.SnoozeTask(dto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Snooze date must be in the future.", result.ErrorMessage);
        _repositoryMock.Verify(r => r.SaveChanges(), Times.Never);
    }

    [Fact]
    public async Task GetTodaysTasks_CallsRepositoryAndReturnsTasks()
    {
        // Arrange
        var today = DateTime.UtcNow.Date;
        var tasks = new List<TodoTask> { TodoTask.Create(Title.Create("Task").Value!, null, Priority.Create(2).Value!).Value! };
        _repositoryMock.Setup(r => r.GetTasksWithinADay(today)).ReturnsAsync(tasks);

        // Act
        var result = await _service.GetTodaysTasks();

        // Assert
        Assert.Equal(tasks, result);
    }

    [Fact]
    public async Task FilterBy_WithParameters_CallsRepositoryAndReturnsFilteredTasks()
    {
        // Arrange
        var tasks = new List<TodoTask> { TodoTask.Create(Title.Create("Task").Value!, null, Priority.Create(2).Value!).Value! };
        _repositoryMock.Setup(r => r.FilterBy("work", 2)).ReturnsAsync(tasks);

        // Act
        var result = await _service.FilterBy("work", 2);

        // Assert
        Assert.Equal(tasks, result);
    }

    [Fact]
    public async Task FilterBy_WithNullParameters_CallsRepositoryWithNullsAndReturnsTasks()
    {
        // Arrange
        var tasks = new List<TodoTask>();
        _repositoryMock.Setup(r => r.FilterBy(null, null)).ReturnsAsync(tasks);

        // Act
        var result = await _service.FilterBy(null, null);

        // Assert
        Assert.Empty(result);
    }
}