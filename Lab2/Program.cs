using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Lab2
{
    static class Program
    {
        static void DoTesting()
        {
            var field = new Field();

            using (StreamReader file = new StreamReader("../snails.txt"))
            {
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                int dataAmount = int.Parse(file.ReadLine());
                int milestone = 2;
                using (StreamWriter resultFile = new StreamWriter("../results.txt", append: true))
                {
                    for (int i = 0; i < dataAmount; i++)
                    {
                        string line = file.ReadLine();
                        double x = double.Parse(line.Split(' ')[0], formatter);
                        double y = double.Parse(line.Split(' ')[1], formatter);
                        field.AddSnail(x, y);
                        if ((i + 1) % 10000 == 0)
                        {
                            milestone *= 2;
                            DateTime start = DateTime.Now;
                            double solveRes = field.Solve();
                            DateTime end = DateTime.Now;
                            TimeSpan duration = end.Subtract(start);
                            Console.WriteLine($"Snail count = {i + 1}, result = {Math.Round(solveRes, 6)}, time to solve = {Math.Round(duration.TotalMilliseconds, 3)} milliseconds");
                            resultFile.WriteLine($"{i + 1} {Math.Round(duration.TotalMilliseconds, 3)}");

                        }
                    }
                    resultFile.Close();
                }
                file.Close();
            }
        }

        static void Main(string[] args)
        {
            Field field = new Field();
            Console.WriteLine("Type number of snails on field:");
            int N = int.Parse(Console.ReadLine());
            Console.WriteLine("Type snails coordinates one by one. Example: 123,4 567,8");
            for(int i = 0; i < N; i++)
            {
                string input = Console.ReadLine();
                double x = double.Parse(input.Split(' ')[0]);
                double y = double.Parse(input.Split(' ')[1]);
                field.AddSnail(x, y);
            }
            Console.WriteLine($"Snails will meet each other in {Math.Round(field.Solve()*100, 2)} seconds");
        }
    }

}