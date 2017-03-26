using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_midps_logic
{
    public class Calculator
    {
        private static void generateTransformedExp(ref List<string> listExp, ref List<string> transformedExp)
        {
            List<string> stack = new List<string>();
            Dictionary<string, int> operators = new Dictionary<string, int>();
            operators["+"] = 1;
            operators["-"] = 1;
            operators["*"] = 2;
            operators["/"] = 2;
            operators["%"] = 2;
            operators["^"] = 3;
            operators["Exp"] = 3;
            operators["y√x"] = 3;
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
            

        }
        public static double eval(List<string> input)
        {
            List<string> listExp = new List<string>(input);
            List<string> transformedExp = new List<string>();
            List<string> stack = new List<string>();
            generateTransformedExp(ref listExp, ref transformedExp);
            Console.WriteLine("Postfix Expression : ");
            foreach (var item in transformedExp)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            while (transformedExp.Count > 0)
            {
                List<string> operators = new List<string>
                {"*", "-", "+", "/", "%", "^", "Exp", "y√x"};
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
                    switch (transformedExp[0])
                    {
                        case "+":
                            res = b + a;
                            break;
                        case "-":
                            res = b - a;
                            break;
                        case "*":
                            res = b * a;
                            break;
                        case "/":
                            res = b / a;
                            break;
                        case "%":
                            res = b % a;
                            break;
                        case "^":
                            res = Math.Pow(b, a);
                            break;
                        case "Exp":
                            res = b * Math.Pow(10, a);
                            break;
                        case "y√x":
                            res = Math.Pow(b, 1 / a);
                            break;
                    }
                    stack.Insert(0, res.ToString());
                }
                transformedExp.RemoveAt(0);
            }
            return double.Parse(stack[0]);
        }
        public static double unaryEval(string input)
        {
            //operations should be in the format unOp:number
            var ar = input.Split(':');
            double number = double.Parse(ar[1]);
            switch (ar[0])
            {
                case "fact":
                    double result = factorial(ar[1]);
                    return result;
                case "exp":
                    return Math.Exp(number);
                case "cube":
                    return Math.Pow(number, 3);
                case "invert":
                    return 1 / number;
                case "ln":
                    return Math.Log(number);
                case "asin":
                    return Math.Asin(number);
                case "atan":
                    return Math.Atan(number);
                case "acos":
                    return Math.Acos(number);
                case "dms":
                    return dms(number);
                case "deg":
                    return deg(number);
                case "sin":
                    return Math.Sin(number);
                case "cos":
                    return Math.Cos(number);
                case "sqr":
                    return Math.Pow(number, 2);
                case "sqrt":
                    return Math.Sqrt(number);
                case "tan":
                    return Math.Tan(number);
                case "10to":
                    return Math.Pow(10, number);
                case "lg":
                    return Math.Log10(number);
            }
            return 0;
        }
        private static double deg(double angleInDms)
        {
            double deegrees;
            double minutes;
            double seconds;
            deegrees = (int)Math.Floor(angleInDms);
            angleInDms -= deegrees;
            angleInDms *= 100;
            minutes = (int)Math.Floor(angleInDms);
            angleInDms -= minutes;
            angleInDms *= 100;
            seconds = (int)Math.Floor(angleInDms);

            return deegrees + minutes / 60 + seconds / 3600;
        }
        private static double dms(double angleInDegrees)
        {
            //ensure the value will fall within the primary range [-180.0..+180.0]
            while (angleInDegrees < -180.0)
                angleInDegrees += 360.0;

            while (angleInDegrees > 180.0)
                angleInDegrees -= 360.0;

            //switch the value to positive

            angleInDegrees = Math.Abs(angleInDegrees);
            double result;
            double minutes;
            double seconds;

            //gets the degree
            result = (int)Math.Floor(angleInDegrees);
            var delta = angleInDegrees - result;

            //gets minutes and seconds
            var sec = (int)Math.Round(3600.0 * delta);
            seconds = sec % 60;
            minutes = (int)Math.Round(sec / 60.0);

            result += minutes / 100 + seconds / 10000;
            return result;
        }
        private static double factorial(string input)
        {
            int n;
            int resultInt = 1;
            bool isInt = int.TryParse(input, out n);
            if (isInt && n > 0)
            {
                for (int i = 2; i <= n; i++)
                {
                    resultInt *= i;
                }
                return resultInt;
            }
            else
            {
                //here we use Striling's approximation to aproximate factorial of an double number.
                double doubleN;
                bool isDouble = double.TryParse(input, out doubleN);
                if (isDouble)
                {
                    return Math.Sqrt(2 * Math.PI * doubleN) * Math.Pow(doubleN, doubleN) * Math.Exp(doubleN * -1);
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
