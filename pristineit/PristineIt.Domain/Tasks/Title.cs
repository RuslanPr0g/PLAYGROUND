using PristineIt.Domain.Common;

namespace PristineIt.Domain.Tasks;

public sealed record Title
{
    public string Value { get; }

    private Title(string value) => Value = value.Trim();

    public static ValueOrResult<Title> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ValueOrResult<Title>.Failure("Title is required.");

        if (value.Length > 100)
            return ValueOrResult<Title>.Failure("Title cannot exceed 100 characters.");

        return ValueOrResult<Title>.Success(new Title(value));
    }
}