using PristineIt.Domain.Common;

namespace PristineIt.Domain.Tasks;

public class Task
{
    // EF needs parameterless constructor
    private Task() { }

    public Guid Id { get; private set; }
    public Title Title { get; private set; } = null!;
    public Description? Description { get; private set; }
    public Priority Priority { get; private set; } = Priority.Medium;
    public bool IsCompleted { get; private set; }
    public DateTime? DueDate { get; private set; } // null = no due date
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    // Collection of tags - EF will map to join table
    private readonly List<Tag> _tags = new();
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

    // Factory method - only way to create valid Task
    public static ValueOrResult<Task> Create(Title title, Description? description, Priority priority)
    {
        if (title is null) return ValueOrResult<Task>.Failure("Title is required.");
        if (priority is null) return ValueOrResult<Task>.Failure("Priority is required.");

        var task = new Task
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Priority = priority,
            CreatedAt = DateTime.UtcNow,
            IsCompleted = false
        };

        return ValueOrResult<Task>.Success(task);
    }

    public ValueOrResult MarkAsCompleted()
    {
        if (IsCompleted)
            return ValueOrResult.Failure("Task is already completed.");

        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
        return ValueOrResult.Success();
    }

    public ValueOrResult Snooze(DateTime newDueDate)
    {
        if (newDueDate <= DateTime.UtcNow)
            return ValueOrResult.Failure("Snooze date must be in the future.");

        DueDate = newDueDate;
        return ValueOrResult.Success();
    }

    public ValueOrResult AddTag(Tag tag)
    {
        if (tag is null) return ValueOrResult.Failure("Tag cannot be null.");
        if (_tags.Any(t => t.Value.Equals(tag.Value, StringComparison.OrdinalIgnoreCase)))
            return ValueOrResult.Failure($"Tag '{tag}' already exists.");

        _tags.Add(tag);
        return ValueOrResult.Success();
    }

    internal void SetDueDateForTesting(DateTime? dueDate) => DueDate = dueDate;
}