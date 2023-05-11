namespace Spring_Lab2
{
    public static class Dijkstra<T>
    {
        public static Dictionary<GraphNode<T>, int> BasicDijkstra(Graph<T> graph, GraphNode<T> source)
        {
            var settledNodes = new Dictionary<GraphNode<T>, int>();
            var unsettledNodes = new Dictionary<GraphNode<T>, int>();

            PriorityQueue<int, int> test;

            unsettledNodes.Add(source, 0);
            while(unsettledNodes.Count > 0)
            {
                GraphNode<T> current = GetMinimumNode(unsettledNodes);
                int curr_distance = unsettledNodes[current];
                unsettledNodes.Remove(current);
                foreach(var adjNode in current.AdjacentNodes.Keys)
                {
                    if(unsettledNodes.ContainsKey(adjNode))
                    {
                        unsettledNodes[adjNode] = Math.Min(curr_distance + current.AdjacentNodes[adjNode], unsettledNodes[adjNode]);
                    } else if (!settledNodes.ContainsKey(adjNode))
                    {
                        unsettledNodes.Add(adjNode, curr_distance + current.AdjacentNodes[adjNode]);
                    }
                }
                settledNodes.Add(current, curr_distance);
            }
            return settledNodes;
        }

        private static GraphNode<T> GetMinimumNode(Dictionary<GraphNode<T>, int> nodes)
        {
            GraphNode<T> minNode = nodes.Keys.ElementAt(0);
            foreach (var node in nodes.Keys)
            {
                if (nodes[node] < nodes[minNode])
                {
                    minNode = node;
                }
            }
            return minNode;
        }

        public static Dictionary<GraphNode<T>, int> BinaryHeapDijkstra(Graph<T> graph, GraphNode<T> source)
        {
            return HeapDijkstra(graph, source, new BinaryHeap<GraphNode<T>>());
        }

        public static Dictionary<GraphNode<T>, int> FiboancciHeapDijkstra(Graph<T> graph, GraphNode<T> source)
        {
            return HeapDijkstra(graph, source, new FibonacciHeap<GraphNode<T>>());
        }

        public static Dictionary<GraphNode<T>, int> HeapDijkstra(Graph<T> graph, GraphNode<T> source, IHeap<GraphNode<T>> heap)
        {
            var settledNodes = new Dictionary<GraphNode<T>, int>();
            var unsettledNodes = new Dictionary<GraphNode<T>, int>();

            unsettledNodes.Add(source, 0);
            heap.Insert(source, 0);
            while (heap.Count > 0)
            {
                var current = heap.ExtractMin();

                foreach (var adjNode in current.Key.AdjacentNodes.Keys)
                {
                    if (settledNodes.ContainsKey(adjNode)) { continue; }
                    int temp_distance = current.Value + current.Key.AdjacentNodes[adjNode];
                    if (unsettledNodes.ContainsKey(adjNode))
                    {
                        if(temp_distance < unsettledNodes[adjNode])
                        {
                            unsettledNodes[adjNode] = temp_distance;
                            try
                            {
                                heap.DecreaseKey(adjNode, temp_distance);
                            } catch
                            {
                                Console.WriteLine("error");
                            }
                        }
                    }
                    else if (!settledNodes.ContainsKey(adjNode))
                    {
                        unsettledNodes.Add(adjNode, temp_distance);
                        heap.Insert(adjNode, temp_distance);
                    }
                }
                settledNodes.Add(current.Key, current.Value);
            }
            return settledNodes;
        }
    }
}
