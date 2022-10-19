using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class MyList
    {
        private Node Head { get; set; }
        private Node Tail { get; set; }

        private int length;
        public int Length
        {
            get { return length; }
        }

        private Node NodeAt(int index)
        {
            if (index > Length - 1)
            {
                throw new IndexOutOfRangeException();
            }
            Node curr = Head;

            for (int i = 0; i < index; i++)
            {
                curr = curr.Next;
            }

            return curr;
        }

        public MyList()
        {
            Head = null;
            Tail = null;
            length = 0;
        }

        public void PushBack(int data)
        {
            if (Head == null)
            {
                Head = new Node(data);
                Tail = Head;
            } else
            {
                Tail.Next = new Node(data, previous: Tail);
                Tail = Tail.Next;
            }
            length++;
        }

        public void PushFront(int data)
        {
            if (Head == null)
            {
                Head = new Node(data);
                Tail = Head;
            }
            else
            {
                Head.Previous = new Node(data, next: Head);
                Head = Head.Previous;
            }
            length++;
        }

        public void PopBack()
        {
            if (length <= 1)
            {
                Clear();
                return;
            }
            Tail = Tail.Previous;
            Tail.Next = null;
            length--;
            if (length == 1) { Head = Tail; }
        }

        public void PopFront()
        {
            if(length <= 1) { 
                Clear();
                return;
            }
            Head = Head.Next;
            Head.Previous = null;
            length--;
            if (length == 1) { Tail = Head; }
        }

        public void Add(int data, int index)
        {
            if (index == 0)
            {
                PushFront(data);
                return;
            }
            if (index == Length - 1)
            {
                PushBack(data);
                return;
            }
            Node curr = NodeAt(index);
            Node new_node = new Node(data, next: curr, previous: curr.Previous);
            curr.Previous.Next = new_node;
            curr.Previous = new_node;
            length++;
        }

        public int At(int index)
        {            
            return NodeAt(index).Data;
        }

        public void DeleteAt(int index)
        {
            Node toDelete = NodeAt(index);
            if(toDelete.Previous == null)
            {
                PopFront();
                return;
            }
            if(toDelete.Next == null)
            {
                PopBack();
                return;
            }
            toDelete.Previous.Next = toDelete.Next;
            toDelete.Next.Previous = toDelete.Previous;
            length--;
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            length = 0;
        }

        public void Edit(int new_data, int index)
        {
            NodeAt(index).Data = new_data;
        }

        public bool IsEmpty()
        {
            return length == 0;
        }

        public int FindOccurence(MyList list)
        {
            int i = 0;
            while(i < length)
            {
                if (list.At(0) == this.At(i))
                {
                    int occ_start = i;
                    bool flag = true;
                    int j = 0;
                    while((i < length) && (j < list.length))
                    {
                        if(list.At(j) != this.At(i))
                        {
                            flag = false;
                            break;
                        }
                        i++;
                        j++;
                    }
                    if(flag)
                    {
                        return occ_start;
                    }
                }
                i++;
            }
            return -1;
        }
    }
}