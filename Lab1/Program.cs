namespace Lab1
{
    static class Program
    {
        static void WriteList(MyList list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                Console.Write(list.At(i) + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            
            MyList list = new MyList();
            for (int i = 0; i < 10; i++)
            {
                list.PushBack(i+1);
            }
            Console.WriteLine("Initial list:");
            WriteList(list);

            list.PushFront(11);
            list.PushFront(12);
            list.PopFront();
            list.PopBack();
            list.Add(12, 4);
            list.DeleteAt(9);
            list.Edit(new_data: 15, index: 3);
            Console.WriteLine("Changed list:");
            WriteList(list);
            Console.WriteLine("IsEmpty: " + list.IsEmpty());

            MyList list2 = new MyList();
            list2.PushBack(15);
            list2.PushBack(12);
            list2.PushBack(4);
            Console.WriteLine("Second list:");
            WriteList(list2);
            Console.WriteLine("Occurence index:" + list.FindOccurence(list2));
            Console.WriteLine("Third list:");
            list2.PushBack(0);
            WriteList(list2);
            Console.WriteLine("Occurence index:" + list.FindOccurence(list2));

            list.Clear();
            Console.WriteLine("IsEmpty after clear: " + list.IsEmpty());
            
        }
    }
}
