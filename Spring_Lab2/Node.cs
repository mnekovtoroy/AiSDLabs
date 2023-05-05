using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab2
{
    public class Node<T> where T : IComparable<T>
    {
        public T Key { get; set; }
        public int Degree { get { return GetDegree(); } }

        public int GetDegree()
        {
            if (Child == null) return 0;
            int i = 1;
            Node<T> curr = Child.Right;

            while(curr != Child)
            {
                i++;
                curr = curr.Right;
            }
            return i;
        }

        public Node<T> Child { get; set; }
        public Node<T> Parent { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T key)
        {
            Key = key;
            Left = this;
            Right = this;
        }

        public override string ToString()
        {
            return Key.ToString();
        }

        public void AddChild(Node<T> child)
        {
            if(Child == null)
            {
                Child = child;
                child.Parent = this;
                child.Left = child;
                child.Right = child;
                //Degree++;
                return;
            }
            child.Left = Child.Left;
            child.Right = Child;
            Child.Left.Right = child;
            Child.Left = child;
            child.Parent = this;
        }

        public List<Node<T>> GetChildsList()
        {
            var childs = new List<Node<T>>();
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
