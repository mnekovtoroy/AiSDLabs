namespace Spring_Lab2
{
    public class BinaryHeap<T> : IHeap<T>
    {
        private List<KeyValuePair<T, int>> Heap = new List<KeyValuePair<T, int>>();

        public int Count { get { return Heap.Count; } }

        public void Insert(T element, int key)
        {
            Heap.Add(new KeyValuePair<T, int>(element, key));
            HeapifyUp(Heap.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            int parent = (index - 1) / 2;
            while (index > 0 && Heap[index].Value < Heap[parent].Value)
            {
                var temp = Heap[index];
                Heap[index] = Heap[parent];
                Heap[parent] = temp;
                index = parent;
                parent = (index - 1) / 2;
            }
        }

        public KeyValuePair<T, int> ExtractMin()
        {
            if(Heap.Count == 0)
            {
                throw new InvalidOperationException("Pop: Heap is empty");
            }

            var item = Heap[0];
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
                if(leftChild < Heap.Count && Heap[leftChild].Value < Heap[lowest].Value)
                {
                    lowest = leftChild;
                }
                if(rightChild < Heap.Count && Heap[rightChild].Value < Heap[lowest].Value)
                {
                    lowest = rightChild;
                }
                if(lowest == index)
                {
                    break;
                }
                var temp = Heap[index];
                Heap[index] = Heap[lowest];
                Heap[lowest] = temp;
                index = lowest;
            }
        }

        public KeyValuePair<T, int> Peek()
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

        public void DecreaseKey(T element, int new_key)
        {
            int index = Heap.FindIndex(x => x.Key.Equals(element));
            int old_key = Heap[index].Value;
            Heap[index] = new KeyValuePair<T, int>(element, new_key);
            if(new_key < old_key)
            {
                HeapifyUp(index);
            } else
            {
                HeapifyDown(index);
            }
        }
    }
}
