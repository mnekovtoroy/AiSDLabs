using System.Collections.Immutable;

namespace Lab3
{
    internal class Program
    {
        delegate void SortingAlgoritm(int[] array);

        static void TestSort(int array_size, StreamWriter stream, params SortingAlgoritm[] algoritms)
        {
            string report = $"{array_size} ";
            var rand = new Random(DateTime.Now.Millisecond);
            foreach (SortingAlgoritm algoritm in algoritms)
            {
                var array = new int[array_size];
                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = rand.Next() % 100000;

                }
                DateTime start = DateTime.Now;
                algoritm.Invoke(array);
                DateTime end = DateTime.Now;
                TimeSpan duration = end - start;
                report += $"{Math.Round(duration.TotalMilliseconds,3)} ";
            }
            stream.WriteLine(report);
            Console.WriteLine(report);
        }

        static void Main(string[] args)
        {
            using(var resultFile = new StreamWriter("../results.txt"))
            {
                resultFile.WriteLine("Elements InsertionSort SelectionSort BubbleSort MergeSort ShellSort QuickSort StandartSort");
                for(int i = 1; i <= 100; i++)
                {
                    TestSort(i*100, resultFile,
                            Sorting.InsertionSort,
                            Sorting.SelectionSort,
                            Sorting.BubbleSort,
                            Sorting.MergeSort,
                            Sorting.ShellSort,
                            Sorting.QuickSort,
                            Array.Sort);

                }
                resultFile.Close();
            }
        }
    }
}