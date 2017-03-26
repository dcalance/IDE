using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_midps_logic
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> exp = new List<string> { "3", "+", "4", "*", "(", "5", "%", "3", ")", "^", "2" };
            Console.WriteLine("Original expression:");
            foreach (var item in exp)
            {
                Console.Write(item);
            }
            Console.WriteLine();
            Console.WriteLine($"Result: {Calculator.eval(exp)}");
            Console.ReadKey();
        }
    }
}
