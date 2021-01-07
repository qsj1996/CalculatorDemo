using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorDemo
{
    public class Calculator
    {

        public static Queue<string> ConvertToRPN(string exp)
        {
            try { VerifyExpression(exp); }
            catch (Exception e) { throw e; }
            
            var RPNQueue = new Queue<string>();
            var OperatorStack = new Stack<char>();
            string numStr="";

            foreach (var c in exp)
            {
                if (int.TryParse(c.ToString(), out _) || c.Equals('.'))
                {
                    numStr += c;
                }
                else if (Operator.IsOperator(c))
                {
                    if (numStr.Length > 0)
                    {
                        RPNQueue.Enqueue(numStr);
                        numStr = "";
                    }
                    if (!OperatorStack.Any())
                    {
                        OperatorStack.Push(c);
                    }
                    else if (c.Equals('('))
                        OperatorStack.Push(c);
                    else if (c.Equals(')'))
                    {
                        while (OperatorStack.Any())
                        {
                            var s = OperatorStack.Pop();
                            if (!s.Equals('('))
                                RPNQueue.Enqueue(s.ToString());
                            else
                                break;
                        }
                    }
                    else
                    {
                        while (OperatorStack.Any())
                        {
                            if (Operator.ComparePriority(OperatorStack.Peek(), c) >= 0)
                            {
                                RPNQueue.Enqueue(OperatorStack.Pop().ToString());
                            }
                            else
                            {
                                break;
                            }
                        }
                        OperatorStack.Push(c);
                    }
                }
                else
                {
                    throw new Exception("Input Expression can only include number & operator!");
                }
            }
            if (numStr.Length > 0)
            {
                RPNQueue.Enqueue(numStr);
            }
            while (OperatorStack.Any()) 
                RPNQueue.Enqueue(OperatorStack.Pop().ToString());
            return RPNQueue;

        }

        public static decimal CalculateRPN(Queue<string> RPNQueue)
        {
            var stack = new Stack<decimal>();
            decimal d;
            while (RPNQueue.Any())
            {
                var s = RPNQueue.Dequeue();
                if (decimal.TryParse(s, out d))
                {
                    stack.Push(d);
                }
                else if (s.Length>1) 
                {
                    throw new Exception("Number too big!");
                }
                else if (Operator.IsOperator(char.Parse(s)))
                {
                    var a = stack.Pop();
                    var b = stack.Pop();
                    if (char.Parse(s).Equals('/') && a == 0) {
                        throw new Exception("can not /0");
                    }
                    var result = Operator.Calculate(b, a, char.Parse(s));
                    stack.Push(result);
                }
            }
            return stack.Pop();
        }

        public static decimal CalculateExp(string exp)
        {
            try { 
            var queue = ConvertToRPN(exp);
            return CalculateRPN(queue);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void VerifyExpression(string exp)
        {
            var count = 0;
            foreach (var c in exp)
            {
                var index = exp.IndexOf(c);
                if(index ==0 && Operator.IsOperator(c) && !c.Equals('('))
                {
                    throw new Exception("Invalid Expression Format! - first char can not be operator! ");
                }
                else if(index.Equals(exp.Count()-1) && Operator.IsOperator(c) && !c.Equals(')'))
                {
                    throw new Exception("Invalid Expression Format! - last char can not be operator! ");
                }
                else if (c.Equals('('))
                {
                    count++;
                    if (index>0 &&!Operator.IsOperator(exp[index - 1]))
                    {
                        throw new Exception("Invalid Expression Format! - ( ");
                    }
                    else if (Operator.IsOperator(exp[index + 1]) && !exp[index + 1].Equals('('))
                    {
                        throw new Exception("Invalid Expression Format! - ( ");
                    }
                }
                else if (c.Equals(')'))
                {
                    count--;
                    if (!index.Equals(exp.Count()-1) && !Operator.IsOperator(exp[index + 1]))
                    {
                        throw new Exception("Invalid Expression Format! - ) ");
                    }
                    else if (Operator.IsOperator(exp[index - 1]) && !exp[index - 1].Equals(')'))
                    {
                        throw new Exception("Invalid Expression Format! - ) ");
                    }
                }
                else if (Operator.IsOperator(c))
                {
                    if("+-*/".Contains(exp[index - 1])|| "+-*/".Contains(exp[index + 1]))
                    {
                        throw new Exception("Invalid Expression Format!");
                    }
                }
            }
            if (count != 0)
            {
                throw new Exception("Invalid Expression Format! - ( ) count <>");

            }
        }
    }
}
