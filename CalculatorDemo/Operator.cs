using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorDemo
{
    public class Operator
    {
        private static readonly Dictionary<char, Operator> Dict = new Dictionary<char, Operator>{
            { '+', new  Operator{ Name = "+", Priority=1,Func=(x,y) => x+y }},
            { '-', new  Operator{ Name = "-", Priority=1,Func=(x,y) => x-y }},
            { '*', new  Operator{ Name = "*", Priority=2,Func=(x,y) => x*y }},
            { '/', new  Operator{ Name = "/", Priority=2,Func=(x,y) => x/y }},
            { '(', new  Operator{ Name = "(", Priority=0}},
            { ')', new  Operator{ Name = ")", Priority=0}},
        };

        public string Name { get; set; }
        public int Priority { get; set; }
        public Func<decimal, decimal, decimal> Func { get; set; }

        public static bool IsOperator(char s)
        {
            if (Dict.ContainsKey(s))
                return true;
            else 
                return false;
        }

        public static int ComparePriority(char a, char b)
        {
            return ComparePriority(Dict[a], Dict[b]);
        }

        public static int ComparePriority(Operator a, Operator b)
        {
            return a.Priority - b.Priority;
        }

        public static decimal Calculate(decimal a,decimal b, char o)
        {
            decimal result =  Dict[o].Func(a, b);
            return result;
        }
    }

}
