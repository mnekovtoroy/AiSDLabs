namespace Spring_Lab2
{
    public class FibonacciHeap<T> : IHeap<T>
    {
        private Dictionary<T, HeapNode<T>> _nodes;
        private HeapNode<T> _minNode;

        public int Count { get { return _nodes.Count; } }

        public FibonacciHeap()
        {
            _minNode = null;
            _nodes = new Dictionary<T, HeapNode<T>>();
        }

        public void Insert(T element, int key)
        {
            HeapNode<T> node = new HeapNode<T>(element, key);
            _minNode = MergeLists(_minNode, node);
            _nodes.Add(element, node);
        }

        private HeapNode<T> MergeLists(HeapNode<T> list1, HeapNode<T> list2)
        {
            if (list1 == null && list2 == null)
                return null;
            if (list1 == null)
                return list2;
            if (list2 == null)
                return list1;

            HeapNode<T> temp = list1.Right;
            list1.Right = list2.Right;
            list1.Right.Left = list1;
            list2.Right = temp;
            list2.Right.Left = list2;

            return list1.Key.CompareTo(list2.Key) < 0 ? list1 : list2;
        }

        public KeyValuePair<T, int> ExtractMin()
        {
            if(Count == 0)
            {
                throw new InvalidOperationException("ExtractMin: FibonacciHeap is empty");
            }
            //Delete the minNode
            var min = _minNode;
            int num_of_childs = min.Degree;
            var child = min.Child;
            while(num_of_childs > 0)
            {
                child.Parent = null;
                child = child.Right;
                num_of_childs--;
            }
            min.Left.Right = min.Right;
            min.Right.Left = min.Left;
            _minNode = MergeLists(min == min.Left ? null : min.Left, child);

            if(_minNode == null)
            {
                _nodes.Clear();
                return new KeyValuePair<T, int>(min.Data, min.Key);
            }

            Consolidate();
            _nodes.Remove(min.Data);
            return new KeyValuePair<T, int>(min.Data, min.Key);
        }

        public void Consolidate()
        {
            var treeList = new HeapNode<T>[Convert.ToInt32(Math.Log2(Count)) + 2];
            HeapNode<T> curr = _minNode;
            int n_Roots = 0;
            //checking how many roots we have
            do
            {
                n_Roots++;
                curr = curr.Left;
            } while (curr != _minNode);

            //handling every root
            while(n_Roots > 0)
            {
                int deg = curr.Degree;
                HeapNode<T> next = curr.Left;
                //checking if there is root of that size already
                while (treeList[deg] != null)
                {
                    HeapNode<T> x = treeList[deg];
                    if(curr.Key.CompareTo(x.Key) > 0)
                    {
                        var temp = x;
                        x = curr;
                        curr = temp;
                    }

                    x.Right.Left = x.Left;
                    x.Left.Right = x.Right;
                    curr.AddChild(x);

                    treeList[deg] = null;
                    deg++;
                }

                treeList[deg] = curr;

                curr = next;
                n_Roots--;
            }

            //finding new minimum
            _minNode = null;
            for(int i = 0; i < treeList.Count(); i++)
            {
                if (treeList[i] != null)
                {
                    if (_minNode == null)
                    {
                        _minNode = treeList[i];
                    }
                    else
                    {
                        if (treeList[i].Key.CompareTo(_minNode.Key) < 0)
                        {
                            _minNode = treeList[i];
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            _minNode = null;
            _nodes.Clear();
        }

        public void DecreaseKey(T element, int new_key)
        {
            var curr = _nodes[element];
            if(new_key > curr.Key) { return; }

            curr.Key = new_key;
            var parent = curr.Parent;

            if(parent != null && parent.Key < new_key) { return; }
            else if(parent == null)
            {
                _minNode = _minNode.Key < curr.Key ? _minNode : curr;
                return;
            }  else
            {
                CutOut(curr);
            }
        }

        private void CutOut(HeapNode<T> node)
        {
            var parent = node.Parent;

            //removing node from parents list
            if(parent != null) { node = node.Parent.RemoveChild(node); }
            //adding it into root list
            _minNode = MergeLists(_minNode, node);

            node.Mark = false;
            if(parent != null && !parent.Mark)
            {
                parent.Mark = true;
            } else if(parent != null)
            {
                CutOut(parent);
            }
        }
    }
}