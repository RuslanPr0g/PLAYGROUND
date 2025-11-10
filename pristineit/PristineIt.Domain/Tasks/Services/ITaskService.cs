using PristineIt.Domain.Common;
using PristineIt.Domain.Tasks.DTOs;

namespace PristineIt.Domain.Tasks.Services;

public interface ITaskService
{
    Task<ValueOrResult<Guid>> CreateTask(CreateTaskDto dto);

    Task<ValueOrResult> AddTagToTask(AddTagToTaskDto dto);
}
