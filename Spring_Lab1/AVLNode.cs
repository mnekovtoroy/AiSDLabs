namespace Spring_Lab1
{
    public class AVLNode<T> : Node<T> where T : IComparable<T>
    {
        public int Height { get; set; }
    }
}