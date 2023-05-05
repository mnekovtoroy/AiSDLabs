namespace Spring_Lab1
{
    public class BinarySearchTree<T> : BinaryTreeBase<T> where T : IComparable<T>
    {
        public BinarySearchTree()
        {
            Head = null;
        }

        public BinarySearchTree(T key)
        {
            Head = new Node<T>() { Key = key };
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

        public override void Insert(T key)
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

        public override void Delete(T key)
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

        public int CalculateDepth()
        {
            return CalculateDepth(Head);
        }
    }
}