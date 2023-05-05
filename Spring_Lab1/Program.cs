namespace Spring_Lab1
{
    internal class Program
    {
        static double ExpInRange(Random R, double A, double B, double Rate = 1.0) 
        { 
            double ERA = Math.Exp(-Rate * A); 
            return -Math.Log(ERA - R.NextDouble() * (ERA - Math.Exp(-Rate * B))) / Rate; 
        }

        static void TestTrees(int N, string output_AVL, string output_BSTree, string output_RBTree, bool exponential)
        {
            var AVLTree = new AVLTree<double>();
            var BinarySearchTree = new BinarySearchTree<double>();
            var RBTree = new RBTree<double>();
            Random r = new Random(DateTime.Now.Second);
            double[] array = new double[N];
            for (int i = 0; i < N; i++)
            {
                array[i] = exponential ? ExpInRange(r, 0.0, 1.0) : r.NextDouble();
            }
            using (StreamWriter sw_AVL = new StreamWriter(output_AVL))
            using (StreamWriter sw_BS = new StreamWriter(output_BSTree))
            using (StreamWriter sw_RB = new StreamWriter(output_RBTree))
            using (StreamWriter sw_Elements = new StreamWriter($"elements_exp_{exponential}.txt"))
            {
                sw_AVL.WriteLine("Action N_of_elements tree_length balance_time");
                sw_BS.WriteLine("Action N_of_elements tree_length");
                sw_RB.WriteLine("Action N_of_elements tree_length balance_time");
                sw_Elements.Write("i array[i]");

                for (int i = 0; i < N; i++)
                {
                    AVLTree.Insert(array[i]);
                    BinarySearchTree.Insert(array[i]);
                    RBTree.Insert(array[i]);

                    sw_Elements.WriteLine($"{i + 1} {array[i]}");
                    if(i % 100 == 0 || i < 100)
                    {
                        sw_AVL.WriteLine($"AVL_Insert {i + 1} {AVLTree.CalculateDepth()} {AVLTree.BalanceTime.TotalMilliseconds}");
                        sw_BS.WriteLine($"BS_Insert {i + 1} {BinarySearchTree.CalculateDepth()}");
                        sw_RB.WriteLine($"RB_Insert {i + 1} {RBTree.CalculateDepth()} {RBTree.BalanceTime.TotalMilliseconds}");
                    }
                    if(i % 10000 == 0)
                    {
                        Console.WriteLine($"{i} out of {2 * N} done (exponential = {exponential})");
                    }
                }
                for (int i = 0; i < N; i++)
                {
                    AVLTree.Delete(array[i]);
                    RBTree.Delete(array[i]);

                    if(i % 100 == 0 || N - i < 100)
                    {
                        sw_AVL.WriteLine($"AVL_Delete {i + 1} {AVLTree.CalculateDepth()} {AVLTree.BalanceTime.TotalMilliseconds}");
                        sw_RB.WriteLine($"RB_Delete {i + 1} {RBTree.CalculateDepth()} {RBTree.BalanceTime.TotalMilliseconds}");
                    }

                    if (i % 10000 == 0)
                    {
                        Console.WriteLine($"{N + i} out of {2 * N} done (exponential = {exponential})");
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int N = 100000;
            string outputAVL_linear = "AVL_L.txt";
            string outputBS_linear = "BS_L.txt";
            string outputRB_linear = "RB_L.txt";
            string outputAVL_exp = "AVL_E.txt";
            string outputBS_exp = "BS_E.txt";
            string outputRB_exp = "RB_E.txt";

            TestTrees(N, outputAVL_linear, outputBS_linear, outputRB_linear, exponential: false);
            TestTrees(N, outputAVL_exp, outputBS_exp, outputRB_exp, exponential: true);
        }
    }
}