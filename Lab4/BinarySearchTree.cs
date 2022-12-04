namespace Lab4
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public Node<T> Head { get; set; }

        public BinarySearchTree()
        {
            Head = null;
        }
        public BinarySearchTree(T key)
        {
            Head = new Node<T>() { Key = key, Left = null, Right = null};
        }

        public BinarySearchTree(IEnumerable<T> list)
        {
            foreach(var item in list)
            {
                Insert(item);
            }
        }

        public void Insert(Node<T> node)
        {
            Head = Insert(Head, node);
        }

        private Node<T> Insert(Node<T> root, Node<T> node)
        {
            if (root == null)
            {
                return node;
            }
            else if (node.Key.CompareTo(root.Key) < 0)
            {
                root.Left = Insert(root.Left, node);
            }
            else if (node.Key.CompareTo(root.Key) >= 0)
            {
                root.Right = Insert(root.Right, node);
            }
            return root;
        }

        public void Insert(T key)
        {
            Head = Insert(Head, key);
        }

        private Node<T> Insert(Node<T> root, T key)
        {
            if (root == null)
            {
                return new Node<T>() { Key = key };
            }
            else if(key.CompareTo(root.Key) < 0)
            {
                root.Left = Insert(root.Left, key);
            }
            else if(key.CompareTo(root.Key) >= 0)
            {
                root.Right = Insert(root.Right, key);
            }
            return root;
        }

        public void Clear()
        {
            Head = null;
        }

        public void Delete(T key)
        {
            Head = Delete(Head, key);
        }

        private Node<T> Delete(Node<T> root, T key)
        {
            if(root == null) { return null; }

            if(key.CompareTo(root.Key) < 0) { root.Left = Delete(root.Left, key); }
            else if (key.CompareTo(root.Key) > 0) { root.Right = Delete(root.Right, key); }

            else if (root.Left != null && root.Right != null)
            {
                root.Key = FindMinimum(root.Right).Key;
                root.Right = Delete(root.Right, root.Key);
            }
            else if (root.Left != null) { root = root.Left; }
            else if (root.Right != null) { root = root.Right; }
            else { root = null; }
            return root;
        }

        public Node<T> Find(T key)
        {
            return Find(Head, key);
        }

        private Node<T> Find(Node<T> root, T key)
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
            while(curr != null)
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

        private Node<T> FindMinimum(Node<T> head)
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

        private Node<T> FindMaximum(Node<T> head)
        {
            if (head == null)
            {
                return null;
            }
            Node<T> curr = Head;
            while(curr.Right != null)
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

        private void Preorder(List<T> outlist, Node<T> curr)
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

        private void Inorder(List<T> outlist, Node<T> curr)
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

        private void Postorder(List<T> outlist, Node<T> curr)
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
            while(queue.Any())
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

        private void CalculateDepth(Node<T> curr, ref int maxdepth, ref int depth)
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
                if(queue.Peek() == null)
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
            for(int i = 0; (i < depth) && list.Any(); i++)
            {
                string line = new string(' ', ((int)Math.Pow(2,depth - 1 - i)) - 1);
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
