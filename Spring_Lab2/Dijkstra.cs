using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab2
{
    public static class Dijkstra<T>
    {
        public static Dictionary<GraphNode<T>, int> GetDistances(Graph<T> graph, GraphNode<T> source)
        {
            var settledNodes = new Dictionary<GraphNode<T>, int>();
            var unsettledNodes = new Dictionary<GraphNode<T>, int>();

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
            //settledNodes.Remove(source);
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
    }
}
