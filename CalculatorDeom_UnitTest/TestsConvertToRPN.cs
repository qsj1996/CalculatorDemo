using NUnit.Framework;
using CalculatorDemo;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CalculatorDeom_UnitTest
{
    public class TestsConvertToRPN
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ConvertToRPN_Case1()
        {
            var result = Calculator.ConvertToRPN("1+2");
            var resultString = ConvertRPNQueueTOString(result);
            Assert.AreEqual("12+", resultString);
        }
        [Test]
        public void Test_ConvertToRPN_Case2()
        {
            var result = Calculator.ConvertToRPN("(1+2)*(3+4)");//12+34+*
            var resultString = ConvertRPNQueueTOString(result);
            Assert.AreEqual("12+34+*", resultString);

        }
        [Test]
        public void Test_ConvertToRPN_Case3()
        {
            var result = Calculator.ConvertToRPN("1+3+4*2");
            var resultString = ConvertRPNQueueTOString(result);
            Assert.AreEqual("13+42*+", resultString);
        }

        [Test]
        public void Test_ConvertToRPN_Case4()
        {
            var result = Calculator.ConvertToRPN("(1+4*2)*3"); //142*+3*
            var resultString = ConvertRPNQueueTOString(result);
            Assert.AreEqual("142*+3*", resultString);
        }

        [Test]
        public void Test_ConvertToRPN_Case5()
        {
            var result = Calculator.ConvertToRPN("(1+4*(2+5))*(3+2)"); //1425+*+32+*
            var resultString = ConvertRPNQueueTOString(result);
            Assert.AreEqual("1425+*+32+*", resultString);
        }

        [Test]
        public void Test_ConvertToRPN_Case6()
        {
            var result = Calculator.ConvertToRPN("(1.2+4*(2+5))"); //1425+*+32+*
            var resultString = ConvertRPNQueueTOString(result);
            Assert.AreEqual("1.2425+*+", resultString);
        }

        [Test]
        public void Test_ConvertToRPN_Case7()
        {
            var e = Assert.Throws<Exception>(() => Calculator.CalculateExp("s+1"));
            Assert.AreEqual("Input Expression can only include number & operator!", e.Message);

        }

        [Test]
        public void Test_ConvertToRPN_Case8()
        {
            var e = Assert.Throws<Exception>(() => Calculator.CalculateExp("1+"));
            Assert.AreEqual("Invalid Expression Format! - last char can not be operator! ", e.Message);
        }

        [Test]
        public void Test_ConvertToRPN_Case9()
        {
            var result = Calculator.ConvertToRPN("(1.2)");
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("1.2", result.Dequeue());
        }

        

        [Test]
        public void Test_ConvertToRPN_Case11()
        {
            var e = Assert.Throws<Exception>(() => Calculator.CalculateExp("2(3+1)"));
            Assert.AreEqual("Invalid Expression Format! - ( ", e.Message);

        }

        [Test]
        public void Test_ConvertToRPN_Case12()
        {
            var e = Assert.Throws<Exception>(() => Calculator.CalculateExp("(3+1)3"));
            Assert.AreEqual("Invalid Expression Format! - ) ", e.Message);

        }

        [Test]
        public void Test_ConvertToRPN_Case13()
        {
            var e = Assert.Throws<Exception>(() => Calculator.CalculateExp("*1+2"));
            Assert.AreEqual("Invalid Expression Format! - first char can not be operator! ", e.Message);

        }

        [Test]
        public void Test_ConvertToRPN_Case14()
        {
            var e = Assert.Throws<Exception>(() => Calculator.CalculateExp("1+2*"));
            Assert.AreEqual("Invalid Expression Format! - last char can not be operator! ", e.Message);

        }

        [Test]
        public void Test_ConvertToRPN_Case15()
        {
            var e = Assert.Throws<Exception>(() => Calculator.CalculateExp("(*2)"));
        }

        [Test]
        public void Test_ConvertToRPN_Case16()
        {
            var e = Assert.Throws<Exception>(() => Calculator.CalculateExp("(2*)"));
        }

        [Test]
        public void Test_ConvertToRPN_Case17()
        {
            var e = Assert.Throws<Exception>(() => Calculator.CalculateExp("(1++2)"));
        }

        [Test]
        public void Test_ConvertToRPN_Case18()
        {
            var e = Assert.Throws<Exception>(() => Calculator.CalculateExp("(1))"));
            Assert.AreEqual("Invalid Expression Format! - ( ) count <>", e.Message);

        }

        [Test]
        public void Test_ConvertToRPN_Case19()
        {
            var e = Assert.Throws<Exception>(() => Calculator.CalculateExp("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111+1"));
            Assert.AreEqual("Number too big!", e.Message);

        }

        public string ConvertRPNQueueTOString(Queue<string> queue)
        {
            string result ="";
            while (queue.Any()) 
            {
                result += queue.Dequeue();
            }
            return result;
        }
    }
}