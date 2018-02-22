using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPractice;
using NodePractice;

namespace GraphsTests
{
    [TestClass]
    public class MyGraphTestSuite
    {
        [TestMethod]
        public void TestMethod1()
        {
            Node<string, int> node = new Node<string, int>("7");
            Graph<string, int> graph = new Graph<string, int>();
        }
    }
}
