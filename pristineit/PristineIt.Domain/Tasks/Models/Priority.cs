using PristineIt.Domain.Common;

namespace PristineIt.Domain.Tasks.Models;

public sealed record Priority
{
    public static readonly Priority Low = new(1, "Low");
    public static readonly Priority Medium = new(2, "Medium");
    public static readonly Priority High = new(3, "High");

    public int Level { get; }
    public string Name { get; }

    private Priority(int level, string name)
    {
        Level = level;
        Name = name;
    }

    public static ValueOrResult<Priority> Create(int level)
    {
        return level switch
        {
            1 => ValueOrResult<Priority>.Success(Low),
            2 => ValueOrResult<Priority>.Success(Medium),
            3 => ValueOrResult<Priority>.Success(High),
            _ => ValueOrResult<Priority>.Failure($"Invalid priority level: {level}. Use 1=Low, 2=Medium, 3=High")
        };
    }

    public override string ToString() => Name;
}