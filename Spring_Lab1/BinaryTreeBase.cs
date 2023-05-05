using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab1
{
    public abstract class BinaryTreeBase<T> where T : IComparable<T>
    {
        public TimeSpan BalanceTime { get; set; }

        public Node<T> Head { get; set; }

        public abstract void Insert(T key);

        public abstract void Delete(T key);

        public void Clear()
        {
            Head = null;
        }

        public Node<T> Find(T key)
        {
            return Find(Head, key);
        }

        protected Node<T> Find(Node<T> root, T key)
        {
            if (root == null || key.CompareTo(root.Key) == 0)
            {
                return root;
            }
            else if (key.CompareTo(root.Key) < 0)
            {
                return Find(root.Left, key);
            }
            else //if key > head.Key
            {
                return Find(root.Right, key);
            }
        }

        public Node<T> FindNext(T key)
        {
            Node<T> curr = Head;
            Node<T> last_greater = null;
            while (curr != null)
            {
                if (curr.Key.CompareTo(key) > 0)
                {
                    last_greater = curr;
                    curr = curr.Left;
                }
                else
                {
                    curr = curr.Right;
                }
            }
            return last_greater;
        }

        public Node<T> FindPrevious(T key)
        {
            Node<T> curr = Head;
            Node<T> last_smaller = null;
            while (curr != null)
            {
                if (curr.Key.CompareTo(key) < 0)
                {
                    last_smaller = curr;
                    curr = curr.Right;
                }
                else
                {
                    curr = curr.Left;
                }
            }
            return last_smaller;
        }

        public T FindMinimum()
        {
            return FindMinimum(Head).Key;
        }

        protected Node<T> FindMinimum(Node<T> head)
        {
            if (head == null)
            {
                return null;
            }
            Node<T> curr = head;
            while (curr.Left != null)
            {
                curr = curr.Left;
            }
            return curr;
        }

        public T FindMaximum()
        {
            return FindMaximum(Head).Key;
        }

        protected Node<T> FindMaximum(Node<T> head)
        {
            if (head == null)
            {
                return null;
            }
            Node<T> curr = Head;
            while (curr.Right != null)
            {
                curr = curr.Right;
            }
            return curr;
        }

        public List<T> GetPreorderDFS()
        {
            List<T> preorder = new List<T>();
            Preorder(preorder, Head);
            return preorder;
        }

        protected void Preorder(List<T> outlist, Node<T> curr)
        {
            if (curr == null) { return; }
            outlist.Add(curr.Key);
            Preorder(outlist, curr.Left);
            Preorder(outlist, curr.Right);
        }

        public List<T> GetInorderDFS()
        {
            List<T> inorder = new List<T>();
            Inorder(inorder, Head);
            return inorder;
        }

        protected void Inorder(List<T> outlist, Node<T> curr)
        {
            if (curr == null) { return; }
            Inorder(outlist, curr.Left);
            outlist.Add(curr.Key);
            Inorder(outlist, curr.Right);
        }

        public List<T> GetPostorderDFS()
        {
            List<T> postorder = new List<T>();
            Postorder(postorder, Head);
            return postorder;
        }

        protected void Postorder(List<T> outlist, Node<T> curr)
        {
            if (curr == null) { return; }
            Postorder(outlist, curr.Left);
            Postorder(outlist, curr.Right);
            outlist.Add(curr.Key);
        }

        public List<T> GetBFS()
        {
            List<T> list = new List<T>();
            Queue<Node<T>> queue = new Queue<Node<T>>();
            if (Head != null)
            {
                queue.Enqueue(Head);
            }
            while (queue.Any())
            {
                if (queue.Peek().Left != null) { queue.Enqueue(queue.Peek().Left); }
                if (queue.Peek().Right != null) { queue.Enqueue(queue.Peek().Right); }
                list.Add(queue.Dequeue().Key);
            }
            return list;
        }

        public int CalculateDepth(Node<T> head)
        {
            int maxdepth = 0;
            int depth = 0;
            CalculateDepth(head, ref maxdepth, ref depth);
            return maxdepth;
        }

        protected void CalculateDepth(Node<T> curr, ref int maxdepth, ref int depth)
        {
            if (curr == null)
            {
                if (depth > maxdepth)
                {
                    maxdepth = depth;
                }
                depth--;
                return;
            }
            depth++;
            CalculateDepth(curr.Left, ref maxdepth, ref depth);
            depth++;
            CalculateDepth(curr.Right, ref maxdepth, ref depth);
            depth--;
            return;
        }

        public void Print()
        {
            int depth = CalculateDepth(Head);

            List<Node<T>> list = new List<Node<T>>();
            Queue<Node<T>> queue = new Queue<Node<T>>();
            if (Head != null)
            {
                queue.Enqueue(Head);
            }
            while (queue.Any(x => x != null))
            {
                if (queue.Peek() == null)
                {
                    queue.Enqueue(null);
                    queue.Enqueue(null);
                }
                else
                {
                    queue.Enqueue(queue.Peek().Left);
                    queue.Enqueue(queue.Peek().Right);
                }
                list.Add(queue.Dequeue());
            }

            //visualising part
            for (int i = 0; (i < depth) && list.Any(); i++)
            {
                string line = new string(' ', ((int)Math.Pow(2, depth - 1 - i)) - 1);
                for (int j = 0; (j < Math.Pow(2, i)) && list.Any(); j++)
                {
                    if (list[0] == null)
                    {
                        line += " ";
                    }
                    else
                    {
                        line += list[0].Key.ToString();
                    }
                    line += new string(' ', ((int)Math.Pow(2, depth - i)) - 1);
                    list.RemoveAt(0);
                }
                Console.WriteLine(line);
            }
        }
    }
}