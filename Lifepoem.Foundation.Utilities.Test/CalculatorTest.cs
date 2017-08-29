using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lifepoem.Foundation.Utilities.Test
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void TestSimpleExpression()
        {
            Calculator c = new Calculator();
            string exp = "1 + 2 + 3";
            int result = 6;
            Assert.AreEqual(c.Evaluate(exp), result);

            exp = "1 + 2 * 3 + 4";
            result = 11;
            Assert.AreEqual(c.Evaluate(exp), result);

        }

        [TestMethod]
        public void TestExpressionWithBrace()
        {
            Calculator c = new Calculator();
            string exp = "1 + 2 * (3 + 4)";
            int result = 15;
            Assert.AreEqual(c.Evaluate(exp), result);
        }

        [TestMethod]
        public void TestExpressionWithVariable()
        {
            Calculator c = new Calculator();
            c.SetVariable("x", 2);
            c.SetVariable("y", 3);
            string exp = "1 + x * (y + 4)";
            int result = 15;
            Assert.AreEqual(c.Evaluate(exp), result);
        }

        [TestMethod]
        public void TestExpressionWithMathematics()
        {
            Calculator c = new Calculator();
            double x = Math.PI * 90 / 180;
            string exp = "1 + sin(x)";
            c.SetVariable("x", x);
            int result = 2;
            Assert.AreEqual(c.Evaluate(exp), result);
        }
    }
}
