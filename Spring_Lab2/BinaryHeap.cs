using System;

namespace Spring_Lab2
{
    public class BinaryHeap<T> where T : IComparable<T>
    {
        private List<T> Heap = new List<T>();

        public int Count { get { return Heap.Count; } }

        public void Push(T key)
        {
            Heap.Add(key);
            HeapifyUp(Heap.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            int parent = (index - 1) / 2;
            while (index > 0 && Heap[index].CompareTo(Heap[parent]) < 0)
            {
                T temp = Heap[index];
                Heap[index] = Heap[parent];
                Heap[parent] = temp;
                index = parent;
                parent = (index - 1) / 2;
            }
        }

        public T Pop()
        {
            if(Heap.Count == 0)
            {
                throw new InvalidOperationException("Pop: Heap is empty");
            }

            T item = Heap[0];
            Heap[0] = Heap[Heap.Count - 1];
            Heap.RemoveAt(Heap.Count - 1);
            if(Heap.Count > 0)
            {
                HeapifyDown(0);
            }

            return item;
        }

        private void HeapifyDown(int index)
        {
            while(true)
            {
                int leftChild = index * 2 + 1;
                int rightChild = index * 2 + 2;
                int lowest = index;
                if(leftChild < Heap.Count && Heap[leftChild].CompareTo(Heap[lowest]) < 0)
                {
                    lowest = leftChild;
                }
                if(rightChild < Heap.Count && Heap[rightChild].CompareTo(Heap[lowest]) < 0)
                {
                    lowest = rightChild;
                }
                if(lowest == index)
                {
                    break;
                }
                T temp = Heap[index];
                Heap[index] = Heap[lowest];
                Heap[lowest] = temp;
                index = lowest;
            }
        }

        public T Peek()
        {
            if(Heap.Count == 0)
            {
                throw new InvalidOperationException("Peek: Heap is empty");
            }
            return Heap[0];
        }

        public void Clear()
        {
            Heap.Clear();
        }

        public Graph<T> ToGraph(out GraphNode<T> root)
        {
            var graph = new Graph<T>();
            List<GraphNode<T>> nodes = new List<GraphNode<T>>(Heap.Count);
            nodes.Insert(0, graph.AddNode(Heap[0]));
            for(int i = 0; i < Heap.Count; i++)
            {
                int leftChild = i * 2 + 1;
                int rightChild = i * 2 + 2;
                if (leftChild < Heap.Count)
                {
                    nodes.Insert(leftChild, graph.AddNode(Heap[leftChild]));
                    graph.ConnectNodes(nodes[i], nodes[leftChild], 1);
                }
                if(rightChild < Heap.Count)
                {
                    nodes.Insert(rightChild, graph.AddNode(Heap[rightChild]));
                    graph.ConnectNodes(nodes[i], nodes[rightChild], 1);
                }
            }
            root = nodes[0];
            return graph;
        }
    }
}
