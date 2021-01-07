using System;

namespace CalculatorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please Input Calculate Expression:");
                var exp = Console.ReadLine();
                try
                {
                    var result = Calculator.CalculateExp(exp);
                    Console.WriteLine("Calculate Result: " + result);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

            }
        }
    }
}
