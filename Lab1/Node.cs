using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Node
    {
        public int Data { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node(int data, Node next = null, Node previous = null)
        {
            Data = data;
            Next = next;
            Previous = previous;
        }
    }
}
