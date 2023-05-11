namespace Spring_Lab2
{
    public class GraphNode<T>
    {
        public T Data { get; set; }
        public Dictionary<GraphNode<T>, int> AdjacentNodes { get; }
        public int Degree { get; set; }

        public GraphNode(T data)
        {
            Data = data;
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