using PristineIt.Domain.Common;

namespace PristineIt.Domain.Tasks.Models;

public sealed record Tag
{
    public string Value { get; }

    private Tag(string value)
    {
        Value = value.Trim();
    }

    public static ValueOrResult<Tag> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ValueOrResult<Tag>.Failure("Tag cannot be empty.");

        if (value.Length > 20)
            return ValueOrResult<Tag>.Failure("Tag must be 20 characters or less.");

        return ValueOrResult<Tag>.Success(new Tag(value));
    }

    public override string ToString() => Value;
}