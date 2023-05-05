using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab3
{
    public class HuffmanNode
    {
        public int Frequency { get; set; }
        public char? Character { get; set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }

        public List<bool> Traverse(char c)
        {
            //if this is the leaf of the tree
            if (Left == null && Right == null)
            {
                if(Character == c)
                {
                    return new List<bool>();
                } else
                {
                    return null;
                }
            }

            List<bool> left = null;
            List<bool> right = null;
            //if the character is to the left add 0 to the path
            if(Left != null)
            {
                left = Left.Traverse(c);
            }
            if(left != null)
            {
                List<bool> path = left.ToList();
                path.Insert(0, false);
                return path;
            }

            //if the character is to the right add 1 to the path
            if (Right != null)
            {
                right = Right.Traverse(c);
            }
            if (right != null)
            {
                List<bool> path = right.ToList();
                path.Insert(0, true);
                return path;
            }

            //if there is no character c in the tree
            return null;
        }

        public string Serialize()
        {
            if(Left == null && Right == null)
            {
                return $"{Character}";
            }
            string left = Left == null ? "00" : Left.Serialize();
            string right = Right == null ? "00" : Right.Serialize();
            return $"[[ {left} || {right} ]]";
        }
    }
}
