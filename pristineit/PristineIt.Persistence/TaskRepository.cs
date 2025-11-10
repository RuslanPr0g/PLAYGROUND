using Microsoft.EntityFrameworkCore;
using PristineIt.Domain.Tasks.Models;
using PristineIt.Domain.Tasks.Repositories;

namespace PristineIt.Persistence;

public class TaskRepository : ITaskRepository
{
    private readonly PristineItDbContext _context;

    public TaskRepository(PristineItDbContext context)
    {
        _context = context;
    }

    public async Task<TodoTask?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TodoTask> Add(TodoTask task)
    {
        var result = await _context.Tasks.AddAsync(task);
        return result.Entity;
    }

    public async Task<IEnumerable<TodoTask>> GetTasksWithinADay(DateTime date)
    {
        return await _context.Tasks
            .Include(t => t.Tags)
            .Where(t => !t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value.Date == date)
            .ToListAsync();
    }

    public async Task<IEnumerable<TodoTask>> FilterBy(string? tag, int? priorityLevel)
    {
        var query = _context.Tasks.Include(t => t.Tags).AsQueryable();

        if (!string.IsNullOrEmpty(tag))
            query = query.Where(t => t.Tags.Any(tg => tg.Value == tag));

        if (priorityLevel.HasValue)
            query = query.Where(t => t.Priority.Level == priorityLevel.Value);

        return await query.ToListAsync();
    }

    public async Task<bool> SaveChanges()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}