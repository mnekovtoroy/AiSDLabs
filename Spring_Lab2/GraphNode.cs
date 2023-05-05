using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab2
{
    public class GraphNode<T>
    {
        public T Key { get; set; }
        public Dictionary<GraphNode<T>, int> AdjacentNodes { get; }
        public int Degree { get; set; }

        public GraphNode(T key)
        {
            Key = key;
            AdjacentNodes = new Dictionary<GraphNode<T>, int>();
        }

        public void ConnectTo(GraphNode<T> connectTo, int distance)
        {
            if(!AdjacentNodes.ContainsKey(connectTo))
            {
                AdjacentNodes.Add(connectTo, distance);
                connectTo.AdjacentNodes.Add(this, distance);
                Degree++;
                connectTo.Degree++;
            }
        }

        public void DisconnectFrom(GraphNode<T> disconnectFrom)
        {
            if(AdjacentNodes.ContainsKey(disconnectFrom))
            {
                AdjacentNodes.Remove(disconnectFrom);
                disconnectFrom.AdjacentNodes.Remove(this);
                Degree--;
                disconnectFrom.Degree--;
            }
        }
    }
}