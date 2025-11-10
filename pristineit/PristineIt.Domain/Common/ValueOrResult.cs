namespace PristineIt.Domain.Common;

public class ValueOrResult
{
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }

    protected ValueOrResult(bool isSuccess, string? errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static ValueOrResult Success() => new(true, null);
    public static ValueOrResult Failure(string error) => new(false, error);
}

public class ValueOrResult<T> : ValueOrResult
{
    public T? Value { get; }

    private ValueOrResult(T? value, bool isSuccess, string? errorMessage)
        : base(isSuccess, errorMessage)
    {
        Value = value;
    }

    public static ValueOrResult<T> Success(T value) => new(value, true, null);
    public new static ValueOrResult<T> Failure(string error) => new(default, false, error);
}