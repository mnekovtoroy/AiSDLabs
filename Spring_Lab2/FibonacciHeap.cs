using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab2
{
    public class FibonacciHeap<T> where T : IComparable<T>
    {
        private Node<T> _minNode;
        private int _count;

        public int Count { get { return _count; } }

        public FibonacciHeap()
        {
            _minNode = null;
            _count = 0;
        }

        public void Insert(T key)
        {
            Node<T> node = new Node<T>(key);
            _minNode = MergeLists(_minNode, node);
            _count++;
        }

        private Node<T> MergeLists(Node<T> list1, Node<T> list2)
        {
            if (list1 == null && list2 == null)
                return null;
            if (list1 == null)
                return list2;
            if (list2 == null)
                return list1;

            Node<T> temp = list1.Right;
            list1.Right = list2.Right;
            list1.Right.Left = list1;
            list2.Right = temp;
            list2.Right.Left = list2;

            return list1.Key.CompareTo(list2.Key) < 0 ? list1 : list2;
        }

        public T ExtractMin()
        {
            if(_count == 0)
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
            _minNode = MergeLists(min == min.Right ? null : min.Right, child);

            if(_minNode == null)
            {
                _count = 0;
                return min.Key;
            }

            Consolidate();
            _count--;
            return min.Key;
        }

        public void Consolidate()
        {
            var treeList = new Node<T>[Convert.ToInt32(Math.Log2(_count)) + 1];
            Node<T> curr = _minNode;
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
                Node<T> next = curr.Left;
                //checking if there is root of that size already
                while (treeList[deg] != null)
                {
                    Node<T> x = treeList[deg];
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

        public Graph<Node<T>> ToGraph(out GraphNode<Node<T>> root)
        {
            root = null;
            var graph = new Graph<Node<T>>();
            if(_count == 0)
            {
                return graph;
            }
            Queue<Node<T>> queue = new Queue<Node<T>>();
            Dictionary<Node<T>, GraphNode<Node<T>>> dict = new Dictionary<Node<T>, GraphNode<Node<T>>>();

            queue.Enqueue(_minNode);
            root = graph.AddNode(_minNode);
            dict.Add(_minNode, root);
            var curr = _minNode.Right;
            while(curr != _minNode)
            {
                queue.Enqueue(curr);
                var cg = graph.AddNode(curr);
                dict.Add(curr, cg);
                graph.ConnectNodes(root, cg, 1);
                curr = curr.Right;
            }
            while(queue.Count > 0)
            {
                curr = queue.Dequeue();
                GraphNode<Node<T>> curr_gn; 
                if (!dict.ContainsKey(curr))
                {
                    curr_gn = graph.AddNode(curr);
                    dict.Add(curr, curr_gn);
                } else
                {
                    curr_gn = dict[curr];
                }
                var children = curr.GetChildsList();
                foreach(var child in children)
                {
                    var child_gn = graph.AddNode(child);
                    queue.Enqueue(child);
                    dict.Add(child, child_gn);
                    graph.ConnectNodes(curr_gn, child_gn, 1);
                }
            }
            return graph;
        }
    }
}
