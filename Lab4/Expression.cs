namespace Lab4
{
    public class Expression
    {
        private BinarySearchTree<ExpressionBit> ExpressionTree { get; set; }

        public Expression()
        {
            ExpressionTree = new BinarySearchTree<ExpressionBit>();
        }

        private bool IsOperator(string bit)
        {
            string[] operators = {"+", "-", "*", "/", "^"};
            if(operators.Any(op => op == bit))
            {
                return true;
            }
            return false;
        }

        private int GetPriority(string bit)
        {
            if(bit == "+" || bit== "-")
            {
                return 1;
            }
            if(bit == "*" || bit == "/")
            {
                return 2;
            }
            if(bit == "^")
            {
                return 3;
            }
            return -1;
        }

        private List<ExpressionBit> ToList(string expression)
        {
            string[] bits = expression.Split(' ');
            var expression_list = new List<ExpressionBit>();
            int key = 0;
            foreach (string bit in bits)
            {
                expression_list.Add(new ExpressionBit() { 
                    Data = bit, 
                    Key = key, 
                    isOperator = IsOperator(bit), 
                    Priority = GetPriority(bit)
                });
            }

            return expression_list;
        }

        private void DetermineInfixDepth(List<ExpressionBit> expression_list, int left_border, int right_border, ref int recursion_depth, ref int max_depth)
        {
            recursion_depth++;
            if (recursion_depth > max_depth)
            {
                max_depth = recursion_depth;
            }

            if (right_border == left_border)
            {
                recursion_depth--;
                return;
            }

            int lowest_priority = int.MaxValue;
            int hang_point_ind = -1;
            while (hang_point_ind == -1 && right_border - left_border > -1) //until we find hanging point or left with just one operand
            {
                for (int i = left_border; i < right_border + 1; i++)
                {
                    //skip everything in the brackets
                    if (expression_list[i].Data == "(")
                    {
                        int bracket_count = 1;
                        while (bracket_count > 0)
                        {
                            i++;
                            if (expression_list[i].Data == ")")
                            {
                                bracket_count--;
                            }
                            else if (expression_list[i].Data == "(")
                            {
                                bracket_count++;
                            }
                        }
                    }
                    if (expression_list[i].isOperator && expression_list[i].Priority < lowest_priority)
                    {
                        hang_point_ind = i;
                        lowest_priority = expression_list[i].Priority;
                    }
                }
                left_border++;
                right_border--;
            }
            left_border--;
            right_border++;

            if (right_border == left_border)
            {
                recursion_depth--;
                return;
            }
            DetermineInfixDepth(expression_list, left_border, hang_point_ind - 1, ref recursion_depth, ref max_depth);
            DetermineInfixDepth(expression_list, hang_point_ind + 1, right_border, ref recursion_depth, ref max_depth);

            recursion_depth--;
            return;
        }

        public Expression InfixToTree(string infix_expression)
        {
            ExpressionTree.Clear();
            List<ExpressionBit> expression_list = ToList(infix_expression);
            int depth = 0;
            int max_depth = 0;
            DetermineInfixDepth(expression_list, 0, expression_list.Count - 1, ref depth, ref max_depth);
            InfixToTree(expression_list, 0, expression_list.Count - 1, 1, ref depth, max_depth);
            return this;
        }

        private void InfixToTree(List<ExpressionBit> expression_list, int left_border, int right_border, int base_key, ref int recursion_depth, int max_depth)
        {
            recursion_depth++;
            for (int i = left_border; i < right_border + 1; i++)
            {
                expression_list[i].Key = base_key;
            }
            if(right_border == left_border)
            {
                ExpressionTree.Insert(expression_list[left_border]);
                recursion_depth--;
                return;
            }

            int lowest_priority = int.MaxValue;
            int hang_point_ind = -1; 
            while(hang_point_ind == -1 && right_border - left_border > -1) //until we find hanging point or left with just one operand
            {
                for (int i = left_border; i < right_border + 1; i++)
                {
                    //skip everything in the brackets
                    if (expression_list[i].Data == "(")
                    {
                        int bracket_count = 1;
                        while(bracket_count > 0)
                        {
                            i++;
                            if (expression_list[i].Data == ")")
                            {
                                bracket_count--;
                            }
                            else if (expression_list[i].Data == "(")
                            {
                                bracket_count++;
                            }
                        }
                    }
                    if (expression_list[i].isOperator && expression_list[i].Priority < lowest_priority)
                    {
                        hang_point_ind = i;
                        lowest_priority = expression_list[i].Priority;
                    }
                }
                left_border++;
                right_border--;
            }
            left_border--;
            right_border++;

            if (right_border == left_border)
            {
                ExpressionTree.Insert(expression_list[left_border]);
                recursion_depth--;
                return;
            }

            ExpressionTree.Insert(expression_list[hang_point_ind]); //insert that point

            //Recursion
            InfixToTree(expression_list, left_border, hang_point_ind - 1, base_key - (int)Math.Pow(2, (max_depth - recursion_depth)), ref recursion_depth, max_depth);
            InfixToTree(expression_list, hang_point_ind + 1, right_border, base_key + (int)Math.Pow(2, (max_depth - recursion_depth)), ref recursion_depth, max_depth);
            recursion_depth--;
            return;
        }

        private void ChangeBranchKey(Node<ExpressionBit> curr, int delta)
        {
            if (curr == null) { return; }
            curr.Key.Key += delta;
            ChangeBranchKey(curr.Left, delta);
            ChangeBranchKey(curr.Right, delta);
        }

        public Expression PostfixToTree(string postfix_expression)
        {
            ExpressionTree.Clear();
            List<ExpressionBit> expression_list = ToList(postfix_expression);
            Stack<Node<ExpressionBit>> stack = new Stack<Node<ExpressionBit>>();
            for (int i = 0; i < expression_list.Count; i ++)
            {
                expression_list[i].Key = 0;
                if (!expression_list[i].isOperator)
                {
                    stack.Push(new Node<ExpressionBit>() { Key = expression_list[i], Left = null, Right = null });
                } 
                else
                {
                    var right = stack.Pop();
                    var left = stack.Pop();
                    ChangeBranchKey(right, (int)Math.Pow(2, i + 1));
                    ChangeBranchKey(left, -(int)Math.Pow(2, i + 1));
                    var node = new Node<ExpressionBit> { Key = expression_list[i], Left = left, Right = right };
                    stack.Push(node);
                }
            }
            ExpressionTree.Insert(stack.Pop());
            return this;
        }

        public Expression PrefixToTree(string prefix_expression)
        {
            ExpressionTree.Clear();
            List<ExpressionBit> expression_list = ToList(prefix_expression);
            Stack<Node<ExpressionBit>> stack = new Stack<Node<ExpressionBit>>();
            for (int i = expression_list.Count - 1; i > -1; i--)
            {
                expression_list[i].Key = 0;
                if (!expression_list[i].isOperator)
                {
                    stack.Push(new Node<ExpressionBit>() { Key = expression_list[i], Left = null, Right = null });
                }
                else
                {
                    var left = stack.Pop();
                    var right = stack.Pop();
                    ChangeBranchKey(right, (int) Math.Pow(2, expression_list.Count - i));
                    ChangeBranchKey(left, (int)-Math.Pow(2,expression_list.Count - i));
                    var node = new Node<ExpressionBit> { Key = expression_list[i], Left = left, Right = right };
                    stack.Push(node);
                }
            }
            ExpressionTree.Insert(stack.Pop());
            return this;
        }

        private string ConverToString(List<ExpressionBit> expression_list)
        {
            string line = "";
            foreach (ExpressionBit bit in expression_list)
            {
                line += bit.Data + " ";
            }
            line = line.Remove(line.Length - 1);
            return line;
        }

        private List<ExpressionBit> GetIndorderDFSPlusBrackets()
        {
            var list = new List<ExpressionBit>();
            InorderDFSPlusBrackets(list, ExpressionTree.Head);
            return list;
        }

        private void InorderDFSPlusBrackets(List<ExpressionBit> outlist, Node<ExpressionBit> curr)
        {
            if (curr == null)
            {
                return; 
            }
            if(curr.Left != null && curr.Left.Key.isOperator)
            {
                outlist.Add(new ExpressionBit() { Data = "(" });
            }
            InorderDFSPlusBrackets(outlist, curr.Left);
            if (curr.Left != null && curr.Left.Key.isOperator)
            {
                outlist.Add(new ExpressionBit() { Data = ")" });
            }
            outlist.Add(curr.Key);
            if (curr.Right != null && curr.Right.Key.isOperator)
            {
                outlist.Add(new ExpressionBit() { Data = "(" });
            }
            InorderDFSPlusBrackets(outlist, curr.Right);
            if (curr.Right != null && curr.Right.Key.isOperator)
            {
                outlist.Add(new ExpressionBit() { Data = ")" });
            }
        }

        public string ToInfix()
        {
            return ConverToString(GetIndorderDFSPlusBrackets());
        }

        public string ToPostfix()
        {
            return ConverToString(ExpressionTree.GetPostorderDFS());
        }

        public string ToPrefix()
        {
            return ConverToString(ExpressionTree.GetPreorderDFS());
        }

        public void VisualiseExpressionTree()
        {
            ExpressionTree.Print();
            return;
        }
    }
}
