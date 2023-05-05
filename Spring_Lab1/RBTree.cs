using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab1
{
    public class RBTree<T> : BinaryTreeBase<T> where T : IComparable<T>
    {
        public RBTree()
        {
            Head = null;
        }

        public RBTree(T key)
        {
            Head = new RBNode<T>() { Key = key, Color = Colors.Black };
        }

        public RBTree(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                Insert(item);
            }
        }

        public override void Insert(T key)
        {
            if (Head == null)
            {
                Head = new RBNode<T>() { Key = key, Color = Colors.Black };
                return;
            }

            RBNode<T> node = Head as RBNode<T>;
            RBNode<T> parent = null;
            while(node != null)
            {
                parent = node;
                node = key.CompareTo(node.Key) >= 0 ? node.Right as RBNode<T> : node.Left as RBNode<T>;
            }

            RBNode<T> newnode = new RBNode<T>() { Key = key, Color = Colors.Red };
            newnode.Parent = parent;
            if (key.CompareTo(parent.Key) >= 0)
            {
                parent.Right = newnode;
            } else
            {
                parent.Left = newnode;
            }
            InsertBalance(newnode);
        }

        public void InsertBalance(RBNode<T> node)
        {
            DateTime b_start = DateTime.Now;
            while (node.Parent != null && node.Parent.Color == Colors.Red)
            {
                if(node.Parent == node.Parent.Parent.Left)
                {
                    RBNode<T> uncle = node.Parent.Parent.Right as RBNode<T>;
                    if(uncle != null && uncle.Color == Colors.Red)
                    {
                        uncle.Color = Colors.Black;
                        node.Parent.Color = Colors.Black;
                        uncle.Parent.Color = Colors.Red;
                        node = node.Parent.Parent;
                        continue;
                    } else {
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            RotateLeft(node);
                        }
                        node.Parent.Color = Colors.Black;
                        node.Parent.Parent.Color = Colors.Red;
                        RotateRight(node.Parent.Parent);
                        continue;
                    }
                } else
                {
                    RBNode<T> uncle = node.Parent.Parent.Left as RBNode<T>;
                    if (uncle != null && uncle.Color == Colors.Red)
                    {
                        node.Parent.Color = Colors.Black;
                        uncle.Color = Colors.Black;
                        uncle.Parent.Color = Colors.Red;
                        node = node.Parent.Parent;
                        continue;
                    } else
                    {
                        if(node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RotateRight(node);
                        }
                        node.Parent.Color = Colors.Black;
                        node.Parent.Parent.Color = Colors.Red;
                        RotateLeft(node.Parent.Parent);
                        continue;
                    }
                }
            }

            (Head as RBNode<T>).Color = Colors.Black;
            BalanceTime += DateTime.Now - b_start;
        }

        public override void Delete(T key)
        {
            RBNode<T> toDelete = Find(key) as RBNode<T>;

            if (toDelete == null)
            {
                return;
            }

            RBNode<T> toReplace;
            RBNode<T> toReplace_child;
            if (toDelete.Left == null || toDelete.Right == null)
            {
                toReplace = toDelete;
            }
            else
            {
                toReplace = FindNext(key) as RBNode<T>;
            }

            if (toReplace.Left != null)
            {
                toReplace_child = toReplace.Left as RBNode<T>;
            }
            else
            {
                toReplace_child = toReplace.Right as RBNode<T>;
            }
            if (toReplace_child != null)
            {
                toReplace_child.Parent = toReplace.Parent;
            }

            if (toReplace.Parent == null)
            {
                Head = toReplace_child;
            }
            else if (toReplace == toReplace.Parent.Left)
            {
                toReplace.Parent.Left = toReplace_child;
            }
            else
            {
                toReplace.Parent.Right = toReplace_child;
            }

            if (toDelete != toReplace)
            {
                toDelete.Key = toReplace.Key;
            }

            if (toReplace.Color == Colors.Black)
            {
                DeleteBalance(toReplace_child, toReplace.Parent as RBNode<T>);
            }
        }

        private void DeleteBalance(RBNode<T> node, RBNode<T> parent)
        {
            DateTime b_start = DateTime.Now;
            RBNode<T> brother;
            while(node != Head && (node == null || node.Color == Colors.Black))
            {
                if(node == parent.Left)
                {
                    brother = parent.Right as RBNode<T>;
                    if (brother.Color == Colors.Red)
                    {
                        brother.Color = Colors.Black;
                        parent.Color = Colors.Red;
                        RotateLeft(parent);
                        brother = parent.Right as RBNode<T>;
                    }

                    if ((brother.Left == null || (brother.Left as RBNode<T>).Color == Colors.Black)
                        && (brother.Right == null || (brother.Right as RBNode<T>).Color == Colors.Black))
                    {
                        brother.Color = Colors.Red;
                        node = parent;
                        parent = node.Parent;
                    } else
                    {
                        if (brother.Right == null || (brother.Right as RBNode<T>).Color == Colors.Black)
                        {
                            (brother.Left as RBNode<T>).Color = Colors.Black;
                            brother.Color = Colors.Red;
                            RotateRight(brother);
                            brother = parent.Right as RBNode<T>;
                        }
                        brother.Color = parent.Color;
                        parent.Color = Colors.Black;
                        (brother.Right as RBNode<T>).Color = Colors.Black;
                        RotateLeft(parent);
                        node = Head as RBNode<T>;
                    }
                } else
                {
                    brother = parent.Left as RBNode<T>;
                    if (brother.Color == Colors.Red)
                    {
                        brother.Color = Colors.Black;
                        parent.Color = Colors.Red;
                        RotateRight(parent);
                        brother = parent.Left as RBNode<T>;
                    }

                    if ((brother.Left == null || (brother.Left as RBNode<T>).Color == Colors.Black)
                        && (brother.Right == null || (brother.Right as RBNode<T>).Color == Colors.Black))
                    {
                        brother.Color = Colors.Red;
                        node = parent;
                        parent = node.Parent;
                    }
                    else
                    {
                        if (brother.Left == null || (brother.Left as RBNode<T>).Color == Colors.Black)
                        {
                            (brother.Right as RBNode<T>).Color = Colors.Black;
                            brother.Color = Colors.Red;
                            RotateLeft(brother);
                            brother = parent.Left as RBNode<T>;
                        }
                        brother.Color = parent.Color;
                        parent.Color = Colors.Black;
                        (brother.Left as RBNode<T>).Color = Colors.Black;
                        RotateRight(parent);
                        node = Head as RBNode<T>;
                    }
                }
            }

            if (node != null)
            {
                node.Color = Colors.Black;
            }
            BalanceTime += DateTime.Now - b_start;
        }

        private RBNode<T> RotateLeft(RBNode<T> node)
        {
            RBNode<T> newnode = node.Right as RBNode<T>;
            RBNode<T> temp = newnode.Left as RBNode<T>;

            newnode.Left = node;
            newnode.Parent = node.Parent;
            if (newnode.Parent == null)
            {
                Head = newnode;
            } else if (node == newnode.Parent.Left)
            {
                newnode.Parent.Left = newnode;
            } else
            {
                newnode.Parent.Right = newnode;
            }
            (newnode.Left as RBNode<T>).Parent = newnode;
            node.Right = temp;
            if(node.Right != null)
            {
                (node.Right as RBNode<T>).Parent = node;
            }

            return newnode;
        }

        private RBNode<T> RotateRight(RBNode<T> node)
        {
            RBNode<T> newnode = node.Left as RBNode<T>;
            RBNode<T> temp = newnode.Right as RBNode<T>;

            newnode.Right = node;
            newnode.Parent = node.Parent;
            if (newnode.Parent == null)
            {
                Head = newnode;
            }
            else if (node == newnode.Parent.Left)
            {
                newnode.Parent.Left = newnode;
            }
            else
            {
                newnode.Parent.Right = newnode;
            }
            (newnode.Right as RBNode<T>).Parent = newnode;
            node.Left = temp;
            if (node.Left != null)
            {
                (node.Left as RBNode<T>).Parent = node;
            }

            return newnode;
        }

        public int CalculateDepth()
        {
            return CalculateDepth(Head);
        }
    }
}