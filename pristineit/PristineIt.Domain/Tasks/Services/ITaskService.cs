using PristineIt.Domain.Common;
using PristineIt.Domain.Tasks.DTOs;

namespace PristineIt.Domain.Tasks.Services;

public interface ITaskService
{
    // Read Operations
    Task<IEnumerable<TodoTask>> GetTodaysTasks();

    Task<IEnumerable<TodoTask>> FilterBy(string? tag, int? prio);

    // Create & Update Operations
    Task<ValueOrResult<Guid>> CreateTask(CreateTaskDto dto);

    Task<ValueOrResult> AddTagToTask(AddTagToTaskDto dto);

    Task<ValueOrResult> MarkTaskAsCompleted(MarkTaskAsCompletedDto dto);

    Task<ValueOrResult> SnoozeTask(SnoozeTaskDto dto);
}
