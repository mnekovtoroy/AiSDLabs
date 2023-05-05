namespace Spring_Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //full graph
            int N = 5000;
            var random = new Random(DateTime.Now.Millisecond);
            using (StreamWriter sw = new StreamWriter("fullgraph.txt"))
            {
                sw.WriteLine("number_of_nodes_full_graph dijkstra_time");
                var graph = new Graph<int>();
                var nodes = new HashSet<GraphNode<int>>();
                for(int i = 0; i <= N; i++)
                {
                    var newNode = graph.AddNode(i);
                    foreach(var node in nodes)
                    {
                        graph.ConnectNodes(node, newNode, random.Next(100));
                    }
                    nodes.Add(newNode);

                    if (i % 50 == 0)
                    {
                        DateTime start = DateTime.Now;
                        Dijkstra<int>.GetDistances(graph, graph.graphNodes.First());
                        TimeSpan span = DateTime.Now - start;
                        sw.WriteLine($"{i} {span.TotalMilliseconds}");
                        Console.WriteLine($"Full graph {i + 1}/{N} done");
                    }
                }
                Console.WriteLine("Full graph done!");
            }

            //BinaryHeap
            using(StreamWriter sw = new StreamWriter("binaryheap.txt"))
            {
                sw.WriteLine("number_of_nodes_bin_heap dijkstra_time including_ToGraph");
                var heap = new BinaryHeap<int>();
                for (int i = 0; i <= N; i++)
                {
                    heap.Push(random.Next());

                    if (i % 50 == 0)
                    {
                        DateTime start1 = DateTime.Now;
                        GraphNode<int> source;
                        var graph = heap.ToGraph(out source);
                        DateTime start2 = DateTime.Now;
                        Dijkstra<int>.GetDistances(graph, source);
                        TimeSpan span2 = DateTime.Now - start2;
                        TimeSpan span1 = DateTime.Now - start1;
                        sw.WriteLine($"{i} {span2.TotalMilliseconds} {span1.TotalMilliseconds}");
                        Console.WriteLine($"Binary heap graph {i + 1}/{N} done");
                    }
                }
                Console.WriteLine("Binary heap done!");
            }

            //FibonacciHeap
            using (StreamWriter sw = new StreamWriter("fiboncciheap.txt"))
            {
                sw.WriteLine("number_of_nodes_fib_heap dijkstra_time including_ToGraph");
                var heap = new FibonacciHeap<int>();
                heap.Insert(random.Next());
                for (int i = 0; i <= N; i++)
                {
                    heap.Insert(random.Next());
                    if(i % 10 == 0)
                    {
                        heap.ExtractMin();
                    }

                    if (i % 50 == 0)
                    {
                        DateTime start1 = DateTime.Now;
                        GraphNode<Node<int>> source;
                        var graph = heap.ToGraph(out source);
                        DateTime start2 = DateTime.Now;
                        Dijkstra<Node<int>>.GetDistances(graph, source);
                        TimeSpan span2 = DateTime.Now - start2;
                        TimeSpan span1 = DateTime.Now - start1;
                        sw.WriteLine($"{i} {span2.TotalMilliseconds} {span1.TotalMilliseconds}");
                        Console.WriteLine($"Binary heap graph {i + 1}/{N} done");
                    }
                }
                Console.WriteLine("Fibonacci heap done!");
            }
        }
    }
}