namespace Spring_Lab2
{
    internal class Program
    {
        public delegate Dictionary<GraphNode<int>, int> DijkstraAlgs(Graph<int> graph, GraphNode<int> source);

        public static Graph<int> GenerateGraph(int Size, double p)
        {
            var graph = new Graph<int>();
            var nodes = new List<GraphNode<int>>();

            var random = new Random(DateTime.Now.Millisecond);

            //Creating graph with N nodes
            for (int i = 1; i <= Size; i++)
            {
                nodes.Add(graph.AddNode(i));
            }

            //Creating connectoins between every node with the probability of p
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    if (random.NextDouble() <= p)
                    {
                        graph.ConnectNodes(nodes[i], nodes[j], random.Next(1, 100));
                    }
                }
            }

            return graph;
        }


        static void Main(string[] args)
        {
            DijkstraAlgs[] dijkstra = new DijkstraAlgs[3];
            dijkstra[0] = Dijkstra<int>.BasicDijkstra;
            dijkstra[1] = Dijkstra<int>.BinaryHeapDijkstra;
            dijkstra[2] = Dijkstra<int>.FiboancciHeapDijkstra;
            int N = 10000;
            int step = 100;
            double[] probabilities = new double[3] { 0.1, 0.4, 0.8 };

            for (int i = 0; i < 3; i++)
            {
                using (StreamWriter sw = new StreamWriter($"test_{i + 1}.txt"))
                {
                    sw.WriteLine($"probablity {probabilities[i]}");
                    sw.WriteLine("nodes basic binary fibonacci");
                    for (int n = 1; n <= N; n += step)
                    {
                        string results = $"{n} ";
                        var graph = GenerateGraph(n, probabilities[i]);
                        for (int a = 0; a < dijkstra.Length; a++)
                        {
                            DateTime start = DateTime.Now;
                            dijkstra[a](graph, graph.graphNodes.First());
                            TimeSpan duration = DateTime.Now - start;
                            results += $"{duration.TotalMilliseconds} ";
                        }
                        sw.WriteLine(results);
                        Console.WriteLine($"Test {i}_{n} done...");
                    }
                }
            }
            Console.WriteLine("Done");
        }
    }
}