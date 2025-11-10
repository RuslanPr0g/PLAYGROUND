using PristineIt.Domain.Common.Repositories;

namespace PristineIt.Domain.Tasks.Repositories;

public interface ITaskRepository : IBaseRepository
{
    Task<TodoTask> GetByIdAsync(Guid id);
    Task<bool> Add(TodoTask entity);
}
