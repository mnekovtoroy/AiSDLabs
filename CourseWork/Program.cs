namespace CourseWork
{
    static class Program
    {
        //variant 2
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("In which form would you enter the expression (1-3)?\n" +
                    "1. Infix form.\n" +
                    "2. Prefix form.\n" +
                    "3. Postfix form.");
                string answer = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Write expression:");
                string expression = Console.ReadLine();

                if (answer == "1")
                {
                    expression = new Expression().InfixToTree(expression).ToPostfix();
                }
                else if (answer == "2")
                {
                    expression = new Expression().PrefixToTree(expression).ToPostfix();
                }
                Console.WriteLine("Result:\n" +
                    $"{Math.Round(PostfixSolver.Solve(expression), 5)}");
                Console.WriteLine();
                Console.WriteLine("Do you want to calculate another expression (y/n)?");
                if (Console.ReadLine() == "n")
                {
                    break;
                }
            }
        }
    }
}