
namespace Playground.Runners;

internal sealed class MinHeapRunner : RunnerBase
{
    public override string Description => "Demonstrate MinHeap implementation";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var useExample = prompt.Confirm("Use example data instead of entering manually?");
        
        var minh = new MinHeap<string>();
        
        if (useExample)
        {
            // Example data
            minh.Insert("High priority task", 1);
            minh.Insert("Medium priority task", 5);
            minh.Insert("Low priority task", 10);
            minh.Insert("Urgent task", 2);
            minh.Insert("Normal task", 7);
        }
        else
        {
            var count = prompt.PromptInt("How many items to insert?");
            
            for (int i = 0; i < count; i++)
            {
                var value = prompt.PromptString($"Enter value #{i + 1}:");
                var priority = prompt.PromptInt($"Enter priority for '{value}':");
                minh.Insert(value, priority);
            }
        }
        
        Console.WriteLine("\nMinHeap contents (popping all):");
        while (minh.Count > 0)
        {
            Console.WriteLine($"  Priority: {minh.Peek()}, Value: {minh.Pop()}");
        }
        
        return ValueTask.CompletedTask;
    }

    public sealed class MinHeap<T>
    {
        private class Node<TNodeValue>
        {
            private readonly TNodeValue _value;
            private readonly int _priority;

            public Node(TNodeValue value, int priority)
            {
                _value = value;
                _priority = priority;
            }

            public TNodeValue Value { get { return _value; } }

            public int Priority { get { return _priority; } }
        }

        private readonly List<Node<T>> _values;

        public MinHeap()
        {
            _values = [];
        }

        public MinHeap(T[] values)
        {
            _values = values.Select((x, i) => new Node<T>(x, i)).ToList();
        }

        public int Count => _values.Count;

        public T? Peek()
        {
            return _values[0].Value;
        }

        public T? Pop()
        {
            return RemoveRoot();
        }

        public void Insert(T value, int priority)
        {
            _values.Add(new Node<T>(value, priority));
            SiftUp(_values.Count - 1);
        }

        // BELOW ARE PRIVATE METHODS:

        private T? RemoveRoot()
        {
            if (_values.Count == 0)
            {
                return default;
            }

            var value = Peek();
            RemoveAt();
            return value;
        }

        private void RemoveAt(int rootIndex = 0)
        {
            SwapWithLeaf(rootIndex);
            _values.RemoveAt(Count - 1);
            SiftDown();
        }

        private void SwapWithLeaf(int index)
        {
            Swap(index, Count - 1);
        }

        private void SiftDown(int index = 0)
        {
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            int smallest = index;

            if (leftChild < Count && _values[leftChild].Priority < _values[smallest].Priority)
            {
                smallest = leftChild;
            }

            if (rightChild < Count && _values[rightChild].Priority < _values[smallest].Priority)
            {
                smallest = rightChild;
            }

            if (smallest != index)
            {
                Swap(index, smallest);
                SiftDown(smallest);
            }
        }

        private void SiftUp(int index)
        {
            int parentIndex = (index - 1) / 2;

            while (index > 0 && _values[index].Priority < _values[parentIndex].Priority)
            {
                Swap(index, parentIndex);

                index = parentIndex;

                parentIndex = (index - 1) / 2;
            }
        }

        private void Swap(int i, int j)
        {
            var temp = _values[i];
            _values[i] = _values[j];
            _values[j] = temp;
        }
    }
}
