using PristineIt.Domain.Common.Repositories;

namespace PristineIt.Domain.Tasks.Repositories;

public interface ITaskRepository : IBaseRepository
{
    Task<TodoTask> GetByIdAsync(Guid id);
    Task<IEnumerable<TodoTask>> GetTasksWithinADay(DateTime date);
    Task<IEnumerable<TodoTask>> FilterBy(string? tag, int? prio);

    Task<bool> Add(TodoTask entity);
}
