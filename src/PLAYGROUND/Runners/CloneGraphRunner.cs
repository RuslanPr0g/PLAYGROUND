namespace Playground.Runners;

internal sealed class CloneGraphRunner : RunnerBase
{
    public override string Description => "Deep clone an undirected graph using DFS";

    public override ValueTask Run(IUserPrompt prompt)
    {
        var exampleInputs = new[]
        {
            "1:2,4;2:1,3;3:2,4;4:1,3",
            "1:2;2:1",
            "1:"
        };

        var input = prompt.PromptStringOrChoice(
            "Select or enter graph adjacency (node:neighbors;...):",
            exampleInputs);

        var original = BuildGraph(input);

        if (original is null)
        {
            Console.WriteLine("Empty graph — nothing to clone.");
            return ValueTask.CompletedTask;
        }

        var cloned = new Solution().CloneGraph(original);

        Console.WriteLine("Original graph:");
        PrintGraph(original);
        Console.WriteLine("\nCloned graph:");
        PrintGraph(cloned);
        Console.WriteLine($"\nDeep clone verified: {IsDeepClone(original, cloned)}");

        return ValueTask.CompletedTask;
    }

    private static Node? BuildGraph(string adjacency)
    {
        if (string.IsNullOrWhiteSpace(adjacency))
        {
            return null;
        }

        var nodes = new Dictionary<int, Node>();
        var parts = adjacency.Split(';', StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in parts)
        {
            var colonIndex = part.IndexOf(':');
            if (colonIndex < 0)
            {
                continue;
            }

            var nodeVal = int.Parse(part[..colonIndex].Trim());
            if (!nodes.ContainsKey(nodeVal))
            {
                nodes[nodeVal] = new Node(nodeVal);
            }

            var neighborPart = part[(colonIndex + 1)..].Trim();
            if (string.IsNullOrEmpty(neighborPart))
            {
                continue;
            }

            foreach (var neighborStr in neighborPart.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                var neighborVal = int.Parse(neighborStr.Trim());
                if (!nodes.ContainsKey(neighborVal))
                {
                    nodes[neighborVal] = new Node(neighborVal);
                }

                if (!nodes[nodeVal].neighbors.Contains(nodes[neighborVal]))
                {
                    nodes[nodeVal].neighbors.Add(nodes[neighborVal]);
                }
            }
        }

        return nodes.Count == 0 ? null : nodes.Values.MinBy(n => n.val);
    }

    private static void PrintGraph(Node? node)
    {
        if (node is null)
        {
            Console.WriteLine("  (empty)");
            return;
        }

        var visited = new HashSet<int>();
        var queue = new Queue<Node>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!visited.Add(current.val))
            {
                continue;
            }

            var neighborVals = current.neighbors.Select(n => n.val).OrderBy(v => v);
            Console.WriteLine($"  Node {current.val}: [{string.Join(", ", neighborVals)}]");

            foreach (var neighbor in current.neighbors)
            {
                if (!visited.Contains(neighbor.val))
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    private static bool IsDeepClone(Node? original, Node? cloned)
    {
        if (original is null && cloned is null)
        {
            return true;
        }

        if (original is null || cloned is null)
        {
            return false;
        }

        var originalNodes = new Dictionary<int, Node>();
        var clonedNodes = new Dictionary<int, Node>();
        CollectNodes(original, originalNodes);
        CollectNodes(cloned, clonedNodes);

        if (originalNodes.Count != clonedNodes.Count)
        {
            return false;
        }

        foreach (var (val, origNode) in originalNodes)
        {
            if (!clonedNodes.TryGetValue(val, out var cloneNode))
            {
                return false;
            }

            if (ReferenceEquals(origNode, cloneNode))
            {
                return false;
            }

            var origNeighbors = origNode.neighbors.Select(n => n.val).OrderBy(v => v).ToArray();
            var cloneNeighbors = cloneNode.neighbors.Select(n => n.val).OrderBy(v => v).ToArray();
            if (!origNeighbors.SequenceEqual(cloneNeighbors))
            {
                return false;
            }
        }

        return true;
    }

    private static void CollectNodes(Node node, Dictionary<int, Node> nodes)
    {
        if (nodes.ContainsKey(node.val))
        {
            return;
        }

        nodes[node.val] = node;
        foreach (var neighbor in node.neighbors)
        {
            CollectNodes(neighbor, nodes);
        }
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
