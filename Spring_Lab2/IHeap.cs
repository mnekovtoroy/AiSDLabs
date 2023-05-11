namespace Spring_Lab2
{
    public interface IHeap<T> 
    {
        public int Count { get; }

        public void Insert(T element, int key);

        public KeyValuePair<T, int> ExtractMin();

        public void DecreaseKey(T element, int new_key);

        public void Clear();
    }
}
