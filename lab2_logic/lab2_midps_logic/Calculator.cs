using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_midps_logic
{
    class Calculator
    {
        private static void generateTransformedExp(ref List<string> listExp, ref List<string> transformedExp)
        {
            List<string> stack = new List<string>();
            //List<string> operators = new List<string> { "+", "-", "*", "/", "%", "^"};
            Dictionary<string, int> operators = new Dictionary<string, int>();
            operators["+"] = 1;
            operators["-"] = 1;
            operators["*"] = 2;
            operators["/"] = 2;
            operators["%"] = 2;
            operators["^"] = 3;
            while (listExp.Count > 0)
            {
                double nr;
                bool isNumber = double.TryParse(listExp[0], out nr);
                if (isNumber)
                {
                    transformedExp.Add(listExp[0]);
                }

                if (listExp[0][0] == '(')
                {
                    stack.Insert(0, listExp[0]);
                }

                if (listExp[0][0] == ')')
                {
                    while (stack.Count > 0 && stack[0][0] != '(')
                    {
                        transformedExp.Add(stack[0]);
                        stack.RemoveAt(0);
                    }
                    stack.RemoveAt(0);
                }

                if (operators.ContainsKey(listExp[0]))
                {
                    if (stack.Count == 0 || stack[0][0] == '(')
                    {
                        stack.Insert(0, listExp[0]);
                    }
                    else
                    {
                        while (stack.Count > 0 && stack[0][0] != '(' && operators[listExp[0]] <= operators[stack[0]])
                        {
                            transformedExp.Add(stack[0]);
                            stack.RemoveAt(0);
                        }
                        stack.Insert(0, listExp[0]);
                    }
                }
                listExp.RemoveAt(0);
            }
            while (stack.Count > 0)
            {
                transformedExp.Add(stack[0]);
                stack.RemoveAt(0);
            }
            // *, %, /, +, -, ^, root of order, (, ), exp

        }
        public static double eval(List<string> input)
        {
            List<string> listExp = new List<string>();
            List<string> transformedExp = new List<string>();
            List<string> stack = new List<string>();
            listExp = input;
            generateTransformedExp(ref listExp, ref transformedExp);
            while (transformedExp.Count > 0)
            {
                List<string> operators = new List<string>
                {"*", "-", "+", "/", "%", "^" };
                double nr;
                bool isNumber = double.TryParse(transformedExp[0], out nr);
                if (isNumber)
                {
                    stack.Insert(0, transformedExp[0]);
                }
                if (operators.Contains(transformedExp[0]))
                {
                    double a, b;
                    double res = 0;
                    a = double.Parse(stack[0]);
                    stack.RemoveAt(0);
                    b = double.Parse(stack[0]);
                    stack.RemoveAt(0);

                    if (transformedExp[0] == "+")
                    {
                        res = b + a;
                    }
                    if (transformedExp[0] == "-")
                    {
                        res = b - a;
                    }
                    if (transformedExp[0] == "*")
                    {
                        res = b * a;
                    }
                    if (transformedExp[0] == "/")
                    {
                        res = b / a;
                    }
                    if (transformedExp[0] == "%")
                    {
                        res = b % a;
                    }
                    if (transformedExp[0] == "^")
                    {
                        res = Math.Pow(b, a);
                    }
                    stack.Insert(0, res.ToString());
                }
                transformedExp.RemoveAt(0);
            }
            return double.Parse(stack[0]);
        }
    }
}
