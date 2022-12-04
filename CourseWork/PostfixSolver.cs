namespace CourseWork
{
    public static class PostfixSolver
    {
        private static string[] operators = { "+", "-", "*", "/", "^"};

        private static bool IsOperator(string s)
        {
            return operators.Any(op => op == s);
        }

        private static double Calculate(double x, double y, string oper)
        {
            switch (oper)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                case "^":
                    return Math.Pow(x, y);
                default:
                    break;
            }
            return 0;
        }

        public static double Solve(string expression)
        {
            List<string> expression_list = expression.Split(' ').ToList();
            Stack<string> stack = new Stack<string>();
            for (int i = 0; i < expression_list.Count; i++)
            {
                if (!IsOperator(expression_list[i]))
                {
                    stack.Push(expression_list[i]);
                }
                else
                {
                    var y = stack.Pop();
                    var x = stack.Pop();
                    var node = Calculate(double.Parse(x), double.Parse(y), expression_list[i]).ToString();
                    stack.Push(node);
                }
            }
            var result = stack.Pop();
            return double.Parse(result);
        }
    }
}
