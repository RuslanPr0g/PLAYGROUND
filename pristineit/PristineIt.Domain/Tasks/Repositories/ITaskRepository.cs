using PristineIt.Domain.Common.Repositories;
using PristineIt.Domain.Tasks.Models;

namespace PristineIt.Domain.Tasks.Repositories;

public interface ITaskRepository : IBaseRepository
{
    Task<TodoTask?> GetByIdAsync(Guid id);
    Task<IEnumerable<TodoTask>> GetTasksWithinADay(DateTime date);
    Task<IEnumerable<TodoTask>> FilterBy(string? tag, int? prio);

    Task<TodoTask> Add(TodoTask entity);
}
