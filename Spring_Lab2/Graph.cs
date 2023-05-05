using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab2
{
    public class Graph<T>
    {
        public HashSet<GraphNode<T>> graphNodes { get; }

        public Graph()
        {
            graphNodes = new HashSet<GraphNode<T>>();
        }

        public GraphNode<T> AddNode(T key)
        {
            var newNode = new GraphNode<T>(key);
            graphNodes.Add(newNode);
            return newNode;
        }

        public GraphNode<T> RemoveNode(GraphNode<T> node)
        {
            if(graphNodes.Contains(node))
            {
                graphNodes.Remove(node);
                foreach(var connection in node.AdjacentNodes.Keys)
                {
                    node.DisconnectFrom(connection);
                }
            } else
            {
                throw new InvalidOperationException("removing non-existant node");
            }
            return node;
        }

        public void ConnectNodes(GraphNode<T> firstNode, GraphNode<T> secondNode, int distance)
        {
            firstNode.ConnectTo(secondNode, distance);
        }

        public void DisconnectNodes(GraphNode<T> firstNode, GraphNode<T> secondNode)
        {
            firstNode.DisconnectFrom(secondNode);
        }

        public void Clear()
        {
            graphNodes.Clear();
        }
    }
}
