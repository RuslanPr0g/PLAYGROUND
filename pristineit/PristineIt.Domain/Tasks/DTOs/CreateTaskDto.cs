namespace PristineIt.Domain.Tasks.DTOs;

public record CreateTaskDto(
    string Title,
    string? Description,
    int PriorityLevel
);