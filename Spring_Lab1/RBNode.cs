using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring_Lab1
{
    public class RBNode<T> : Node<T> where T : IComparable<T>
    {
        public RBNode<T> Parent { get; set; }
        public Colors Color { get; set; }
    }
}