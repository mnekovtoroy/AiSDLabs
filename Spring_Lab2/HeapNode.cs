namespace Spring_Lab2
{
    public class HeapNode<T>
    {
        public T Data { get; set; }
        public int Key { get; set; }
        public int Degree { get { return GetDegree(); } }
        public bool Mark { get; set; }

        public int GetDegree()
        {
            if (Child == null) return 0;
            int i = 1;
            HeapNode<T> curr = Child.Right;

            while(curr != Child)
            {
                i++;
                curr = curr.Right;
            }
            return i;
        }

        public HeapNode<T> Child { get; set; }
        public HeapNode<T> Parent { get; set; }
        public HeapNode<T> Left { get; set; }
        public HeapNode<T> Right { get; set; }

        public HeapNode(T data, int key )
        {
            Data = data;
            Key = key;
            Left = this;
            Right = this;
            Mark = false;
        }

        public override string ToString()
        {
            return Key.ToString();
        }

        public void AddChild(HeapNode<T> child)
        {
            if(Child == null)
            {
                Child = child;
                child.Parent = this;
                child.Left = child;
                child.Right = child;
                return;
            }
            child.Left = Child.Left;
            child.Right = Child;
            Child.Left.Right = child;
            Child.Left = child;
            child.Parent = this;
        }

        public HeapNode<T> RemoveChild(HeapNode<T> child)
        {
            if(child.Right == child)
            {
                child.Parent = null;
                Child = null;
                return child;
            }
            child.Right.Left = child.Left;
            child.Left.Right = child.Right;
            child.Parent = null;
            if(Child == child)
            {
                Child = child.Right;
            }
            child.Left = child;
            child.Right = child;
            return child;
        }

        public List<HeapNode<T>> GetChildsList()
        {
            var childs = new List<HeapNode<T>>();
            if(Child == null)
            {
                return childs;
            }
            var curr = Child;
            do
            {
                childs.Add(curr);
                curr = curr.Left;
            } while (curr != Child);
            return childs;
        }
    }
}
