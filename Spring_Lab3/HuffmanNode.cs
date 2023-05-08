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
        public Byte? Character { get; set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }

        public List<bool> Traverse(Byte c)
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

        public Byte? GetByte(List<bool> encoded_bits)
        {
            if(Character != null)
            {
                return Character;
            }
            if(encoded_bits.Count == 0)
            {
                throw new InvalidOperationException("GetByte: Not enough data to decode character");
            }
            bool direction = encoded_bits[0];
            encoded_bits.RemoveAt(0);
            if(direction)
            {
                return Right.GetByte(encoded_bits);
            } else
            {
                return Left.GetByte(encoded_bits);
            }
        }

        public Byte[] Serialize()
        {
            if(Left == null && Right == null)
            {
                if (!Character.HasValue) { throw new InvalidOperationException("Non-serializable tree"); }
                return new Byte[] { Character.Value };
            }
            Byte[] left = Left == null ? new Byte[] { 251 } : Left.Serialize();
            Byte[] right = Right == null ? new Byte[] { 251 } : Right.Serialize();
            Byte[] result = new Byte[] {252};
            result = result.Concat(left).Concat(right).Append(Convert.ToByte(253)).ToArray();
            return result;
        }
    }
}
