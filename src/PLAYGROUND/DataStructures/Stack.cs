namespace Playground.DataStructures;

public sealed class Stack<T>
{
    private const int DEFAULT_SIZE = 10;
    private T[] _stack = [];
    private int _head = -1;
    private int _size = DEFAULT_SIZE;

    public int Head => _head;

    private Stack(T[] values)
    {
        _stack = values;
    }

    public static Stack<T> Create(T[]? values = null)
    {
        return new Stack<T>(values ?? new T[DEFAULT_SIZE]);
    }

    public void Add(T value)
    {
        var newHead = _head + 1;
        if (newHead >= _size)
        {
            var newSize = _size + (_size + _size / 2);
            Array.Resize(ref _stack, newSize);
            _size = newSize;
        }

        _stack[newHead] = value;

        _head++;
    }

    public T? Pop()
    {
        if (_head < 0) return default;

        var popped = _stack[_head];

        _head--;

        return popped;
    }

    public T? Peek()
    {
        if (_head < 0) return default;

        return _stack[_head];
    }
}