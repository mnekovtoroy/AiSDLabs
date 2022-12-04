namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("What format of expression do you wish to transfer? (1-3)\n" +
                "1. Infix expression.\n" +
                "2. Prefix expression.\n" +
                "3. Postfix expression.");
                string answer = Console.ReadLine();
                Console.WriteLine("Write your expression (separate operators and operands with spacebar):");
                string string_expression = Console.ReadLine();
                Expression expression = new Expression();
                if (answer == "1")
                {
                    expression.InfixToTree(string_expression);
                    Console.WriteLine();
                    Console.WriteLine($"Prefix respresentation: {expression.ToPrefix()}");
                    Console.WriteLine($"Postfix respresentation: {expression.ToPostfix()}");
                }
                else if (answer == "2")
                {
                    expression.PrefixToTree(string_expression);
                    Console.WriteLine();
                    Console.WriteLine($"Infix respresentation: {expression.ToInfix()}");
                    Console.WriteLine($"Postfix respresentation: {expression.ToPostfix()}");
                }
                else if (answer == "3")
                {
                    expression.PostfixToTree(string_expression);
                    Console.WriteLine();
                    Console.WriteLine($"Infix respresentation: {expression.ToInfix()}");
                    Console.WriteLine($"Prefix respresentation: {expression.ToPrefix()}");
                }

                Console.WriteLine("Expression tree: ");
                expression.VisualiseExpressionTree();

                Console.WriteLine("\nTransfer another expression (y/n)?");
                if(Console.ReadLine() == "n")
                {
                    break;
                }
            }
        }
    }
}