using PristineIt.Domain.Common;

namespace PristineIt.Domain.Tasks.Models;

public sealed record Description
{
    public string Value { get; }

    private Description(string value) => Value = value.Trim();

    public static ValueOrResult<Description> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return ValueOrResult<Description>.Failure("Description cannot be empty.");

        if (value.Length > 500)
            return ValueOrResult<Description>.Failure("Description cannot exceed 500 characters.");

        return ValueOrResult<Description>.Success(new Description(value));
    }
}