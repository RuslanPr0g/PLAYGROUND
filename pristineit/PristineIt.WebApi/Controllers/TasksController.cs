using Microsoft.AspNetCore.Mvc;
using PristineIt.Domain.Tasks.DTOs;
using PristineIt.Domain.Tasks.Repositories;
using PristineIt.Domain.Tasks.Services;

namespace PristineIt.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTaskDto cmd,
        [FromServices] ITaskService service)
    {
        var result = await service.CreateTask(cmd);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, null)
            : BadRequest(result.ErrorMessage);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        Guid id,
        [FromServices] ITaskRepository repo)
    {
        var task = await repo.GetByIdAsync(id);
        return task == null ? NotFound() : Ok(task);
    }

    [HttpPost("{id}/tags")]
    public async Task<IActionResult> AddTag(
        Guid id,
        [FromBody] string tag,
        [FromServices] ITaskService service)
    {
        var result = await service.AddTagToTask(new AddTagToTaskDto(id, tag));
        return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(
        Guid id,
        [FromServices] ITaskService service)
    {
        var result = await service.MarkTaskAsCompleted(new MarkTaskAsCompletedDto(id));
        return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
    }

    [HttpPost("snooze")]
    public async Task<IActionResult> Snooze(
        [FromBody] SnoozeTaskDto dto,
        [FromServices] ITaskService service)
    {
        var result = await service.SnoozeTask(dto);
        return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
    }

    [HttpGet("today")]
    public async Task<IActionResult> GetToday(
        [FromServices] ITaskService service)
    {
        var tasks = await service.GetTodaysTasks();
        return Ok(tasks);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> Filter(
        [FromQuery] string? tag,
        [FromQuery] int? priority,
        [FromServices] ITaskService service)
    {
        var tasks = await service.FilterBy(tag, priority);
        return Ok(tasks);
    }
}