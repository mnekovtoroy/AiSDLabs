namespace Spring_Lab1
{
    public class AVLTree<T> : BinaryTreeBase<T> where T : IComparable<T>
    {
        public AVLTree()
        {
            Head = null;
        }

        public AVLTree(T key)
        {
            Head = new AVLNode<T>() { Key = key, Height = 1 };
        }
        public AVLTree(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                Insert(item);
            }
        }

        public override void Insert(T key)
        {
            Head = Insert(Head as AVLNode<T>, key);
        }

        private AVLNode<T> Insert(AVLNode<T> node, T key)
        {
            if(node == null)
            {
                return new AVLNode<T>() { Key = key , Height = 1};
            }
            
            if(key.CompareTo(node.Key) < 0)
            {
                node.Left = Insert(node.Left as AVLNode<T>, key);
            } else
            {
                node.Right = Insert(node.Right as AVLNode<T>, key);
            }

            node.Height = Math.Max(NodeHeight(node.Right as AVLNode<T>), NodeHeight(node.Left as AVLNode<T>)) + 1;

            return InsertBalance(node, key);
        }

        private AVLNode<T> InsertBalance(AVLNode<T> node, T key)
        {
            DateTime b_start = DateTime.Now;
            int balance = NodeBalance(node);
            if (balance > 1 && key.CompareTo(node.Left.Key) <= 0)
            {
                return RotateRight(node);
            }
            if (balance < -1 && key.CompareTo(node.Right.Key) >= 0)
            {
                return RotateLeft(node);
            }
            if (balance > 1 && key.CompareTo(node.Left.Key) > 0)
            {
                node.Left = RotateLeft(node.Left as AVLNode<T>);
                return RotateRight(node);
            }
            if (balance < -1 && key.CompareTo(node.Right.Key) < 0)
            {
                node.Right = RotateRight(node.Right as AVLNode<T>);
                return RotateLeft(node);
            }
            BalanceTime += DateTime.Now - b_start;
            return node;
        }

        public override void Delete(T key)
        {
            Head = Delete(Head as AVLNode<T>, key);
        }

        private AVLNode<T> Delete(AVLNode<T> node, T key)
        {
            if(node == null) return null;

            if (node.Key.CompareTo(key) > 0)
            {
                node.Left = Delete(node.Left as AVLNode<T>, key);
            }
            else if (node.Key.CompareTo(key) < 0)
            {
                node.Right = Delete(node.Right as AVLNode<T>, key);
            }
            else //this is the node
            {
                if(node.Left == null && node.Right == null)
                {
                    node = null;
                } else if (node.Left == null)
                {
                    node = node.Right as AVLNode<T>;
                } else if (node.Right == null)
                {
                    node = node.Left as AVLNode<T>;
                } else
                {
                    AVLNode<T> temp = null;
                    temp = FindMinimum(node.Right) as AVLNode<T>;
                    node.Key = temp.Key;
                    node.Right = Delete(node.Right as AVLNode<T>, temp.Key);
                }
            }

            if (node == null) return null;

            node.Height = Math.Max(NodeHeight(node.Right as AVLNode<T>), NodeHeight(node.Left as AVLNode<T>)) + 1;

            return DeleteBalance(node);       
        }

        private AVLNode<T> DeleteBalance(AVLNode<T> node)
        {
            DateTime b_start = DateTime.Now;
            int balance = NodeBalance(node);

            if (balance > 1 && NodeBalance(node.Left as AVLNode<T>) >= 0)
            {
                return RotateRight(node);
            }
            if (balance > 1 && NodeBalance(node.Left as AVLNode<T>) < 0)
            {
                node.Left = RotateLeft(node.Left as AVLNode<T>);
                return RotateRight(node);
            }
            if (balance < -1 && NodeBalance(node.Right as AVLNode<T>) <= 0)
            {
                return RotateLeft(node);
            }
            if (balance < -1 && NodeBalance(node.Right as AVLNode<T>) > 0)
            {
                node.Right = RotateRight(node.Right as AVLNode<T>);
                return RotateLeft(node);
            }
            BalanceTime += DateTime.Now - b_start;
            return node;
        }

        private int NodeHeight(AVLNode<T> node)
        {
            return node == null ? 0 : node.Height;
        }

        private int NodeBalance(AVLNode<T> node)
        {
            return node == null ? 0 : NodeHeight(node.Left as AVLNode<T>) - NodeHeight(node.Right as AVLNode<T>);
        }

        private AVLNode<T> RotateLeft(AVLNode<T> node)
        {
            AVLNode<T> newnode = node.Right as AVLNode<T>;
            AVLNode<T> temp = newnode.Left as AVLNode<T>;

            newnode.Left = node;
            node.Right = temp;

            node.Height = Math.Max(NodeHeight(node.Right as AVLNode<T>), NodeHeight(node.Left as AVLNode<T>)) + 1;
            newnode.Height = Math.Max(NodeHeight(newnode.Right as AVLNode<T>), NodeHeight(newnode.Left as AVLNode<T>)) + 1;

            return newnode;
        }

        private AVLNode<T> RotateRight(AVLNode<T> node)
        {
            AVLNode<T> newnode = node.Left as AVLNode<T>;
            AVLNode<T> temp = newnode.Right as AVLNode<T>;

            newnode.Right = node;
            node.Left = temp;

            node.Height = Math.Max(NodeHeight(node.Right as AVLNode<T>), NodeHeight(node.Left as AVLNode<T>)) + 1;
            newnode.Height = Math.Max(NodeHeight(newnode.Right as AVLNode<T>), NodeHeight(newnode.Left as AVLNode<T>)) + 1;

            return newnode;
        }

        public int CalculateDepth()
        {
            return Head == null ? 0 : (Head as AVLNode<T>).Height;
        }
    }
}