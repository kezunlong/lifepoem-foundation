using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lifepoem.Foundation.Utilities.Test
{
    [TestClass]
    public class ParameterParserTest
    {
        [TestMethod]
        public void TestParameters()
        {
            string[] items = { "-p1", "value1", "-p2", "value2", "-p3" };
            ParameterParser parser = new ParameterParser(items);

            Assert.AreEqual(parser.Parameters.Count, 3);
            Assert.AreEqual(parser.GetParameterValue("-p1"), "value1");
            Assert.AreEqual(parser.GetParameterValue("-p2"), "value2");
            Assert.AreEqual(parser.GetParameterValue("-p3"), "");
        }

    }
}
