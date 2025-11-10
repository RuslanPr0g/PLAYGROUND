namespace Playground.Runners;

internal sealed class CloneGraphRunner : IRunner
{
    public string Name => nameof(CloneGraphRunner);

    public ValueTask Run()
    {
        return ValueTask.CompletedTask;
    }

    public class Solution
    {
        private readonly Dictionary<int, Node> visited;

        public Solution() => visited = [];

        public Node CloneGraph(Node node)
        {
            if (node is null)
            {
                return null!;
            }

            return DFSClone(node);
        }

        private Node DFSClone(Node node)
        {
            Node clone = new(node.val);
            visited.Add(node.val, clone);

            foreach (Node neighbor in node.neighbors)
            {
                if (!visited.TryGetValue(neighbor.val, out var existingNeighbour))
                {
                    var clonedNeighbour = DFSClone(neighbor);
                    AddToCollectionIfNotContains(clone.neighbors, clonedNeighbour);
                    AddToCollectionIfNotContains(clonedNeighbour.neighbors, clone);
                }
                else
                {
                    AddToCollectionIfNotContains(clone.neighbors, existingNeighbour);
                    AddToCollectionIfNotContains(existingNeighbour.neighbors, clone);
                }
            }

            return clone;
        }

        private void AddToCollectionIfNotContains(IList<Node> collection, Node item)
        {
            if (!collection.Contains(item))
            {
                collection.Add(item);
            }
        }
    }

    public class Node
    {
        public int val;
        public IList<Node> neighbors;

        public Node()
        {
            val = 0;
            neighbors = new List<Node>();
        }

        public Node(int _val)
        {
            val = _val;
            neighbors = new List<Node>();
        }

        public Node(int _val, List<Node> _neighbors)
        {
            val = _val;
            neighbors = _neighbors;
        }
    }
}
