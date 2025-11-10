using PristineIt.Domain.Common;
using PristineIt.Domain.Tasks;
using PristineIt.Domain.Tasks.DTOs;
using PristineIt.Domain.Tasks.Repositories;
using PristineIt.Domain.Tasks.Services;

namespace PristineIt.Application.Services;

public sealed class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    // Read Operations
    public async Task<IEnumerable<TodoTask>> GetTodaysTasks()
    {
        var today = DateTime.UtcNow.Date;
        return await _repository.GetTasksWithinADay(today);
    }

    public async Task<IEnumerable<TodoTask>> FilterBy(string? tag, int? prio)
    {
        return await _repository.FilterBy(tag, prio);
    }

    // Create & Update Operations
    public async Task<ValueOrResult<Guid>> CreateTask(CreateTaskDto dto)
    {
        var titleResult = Title.Create(dto.Title);
        if (!titleResult.IsSuccess)
        {
            return ValueOrResult<Guid>.Failure(titleResult.ErrorMessage!);
        }

        var descResult = Description.Create(dto.Description);
        if (!descResult.IsSuccess)
        {
            return ValueOrResult<Guid>.Failure(descResult.ErrorMessage!);
        }

        var priorityResult = Priority.Create(dto.PriorityLevel);
        if (!priorityResult.IsSuccess)
        {
            return ValueOrResult<Guid>.Failure(priorityResult.ErrorMessage!);
        }

        var taskResult = TodoTask.Create(titleResult.Value!, descResult.Value, priorityResult.Value!);
        if (!taskResult.IsSuccess)
        {
            return ValueOrResult<Guid>.Failure(taskResult.ErrorMessage!);
        }

        TodoTask? task = taskResult.Value;

        if (task is null)
        {
            return ValueOrResult<Guid>.Failure("Something went wrong.");
        }

        await _repository.Add(task);
        await _repository.SaveChanges();

        return ValueOrResult<Guid>.Success(task.Id);
    }

    public async Task<ValueOrResult> AddTagToTask(AddTagToTaskDto dto)
    {
        var task = await _repository.GetByIdAsync(dto.TaskId);
        if (task is null)
        {
            return ValueOrResult.Failure("Task not found.");
        }

        var tagResult = Tag.Create(dto.Tag);
        if (!tagResult.IsSuccess) return tagResult;

        var result = task.AddTag(tagResult.Value!);
        if (!result.IsSuccess) return result;

        // We do not need to call update because EF core handles tracking itself and
        // knows that the entity has changed, thus creating SQL for it.
        await _repository.SaveChanges();
        return ValueOrResult.Success();
    }

    public async Task<ValueOrResult> MarkTaskAsCompleted(MarkTaskAsCompletedDto dto)
    {
        var task = await _repository.GetByIdAsync(dto.TaskId);
        if (task is null)
        {
            return ValueOrResult.Failure("Task not found.");
        }

        var result = task.MarkAsCompleted();
        if (!result.IsSuccess) return result;

        // We do not need to call update because EF core handles tracking itself and
        // knows that the entity has changed, thus creating SQL for it.
        await _repository.SaveChanges();
        return ValueOrResult.Success();
    }

    public async Task<ValueOrResult> SnoozeTask(SnoozeTaskDto dto)
    {
        var task = await _repository.GetByIdAsync(dto.TaskId);
        if (task is null) return ValueOrResult.Failure("Task not found.");

        var result = task.Snooze(dto.NewDueDate);
        if (!result.IsSuccess) return result;

        // We do not need to call update because EF core handles tracking itself and
        // knows that the entity has changed, thus creating SQL for it.
        await _repository.SaveChanges();
        return ValueOrResult.Success();
    }
}
