using CalculatorDemo;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorDeom_UnitTest
{
    class TestCalculateExp
    {
        [Test]
        public void Test_CalculateExp_Case1()
        {
            var result = Calculator.CalculateExp("1+2");
            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test_CalculateExp_Case2()
        {
            var result = Calculator.CalculateExp("(1+2)*(3+4)");
            Assert.AreEqual(21, result);
        }

        [Test]
        public void Test_CalculateExp_Case3()
        {
            var result = Calculator.CalculateExp("(1.2+2)*(3+4)");
            Assert.AreEqual(22.4, result);
        }

        [Test]
        public void Test_CalculateExp_Case5()
        {
            Assert.Throws<Exception>(() => Calculator.CalculateExp("s+3"));
        }

        [Test]
        public void Test_CalculateExp_Case6()
        {
            Assert.Throws<Exception>(() => Calculator.CalculateExp("1+"));
        }
    }
}
