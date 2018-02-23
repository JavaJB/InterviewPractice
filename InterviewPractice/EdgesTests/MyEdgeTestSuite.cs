using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Edges;
using NodePractice;

namespace EdgesTests
{
    [TestClass]
    public class MyEdgeTestSuite
    {
        [TestMethod]
        public void Edge_Test_OnlyNodesConstructor_()
        {
            MyNode<string> n1 = new MyNode<string>("1");
            MyNode<string> n2 = new MyNode<string>("2");
            Edge<int, string> e1 = new Edge<int, string>(n1, n2);
            Assert.AreEqual(e1.Node1, n1);
            Assert.AreEqual(e1.Node2, n2);
            Assert.AreEqual(e1.Node1.VALUE, "1"); //kind of redundant
            Assert.AreEqual(e1.Node2.VALUE, "2"); //kind of redundant
        }

        [TestMethod]
        public void Edge_Test_ValueAndNodesConstructor_()
        {
            MyNode<int> n1 = new MyNode<int>(1);
            MyNode<int> n2 = new MyNode<int>(2);
            Edge<int, int> e1 = new Edge<int, int>(7, n1, n2);
            Assert.AreEqual(e1.value, 7);
            Assert.AreEqual(e1.Node1, n1);
            Assert.AreEqual(e1.Node2, n2);
            Assert.AreEqual(e1.Node1.VALUE, 1); //kind of redundant
            Assert.AreEqual(e1.Node2.VALUE, 2); //kind of redundant
        }

        [TestMethod]
        public void Edge_Test_HasSameNodes_()
        {
            MyNode<int> n1 = new MyNode<int>(1);
            MyNode<int> n2 = new MyNode<int>(2);
            Edge<int, int> e1 = new Edge<int, int>(7, n1, n2);
            MyNode<int> n3 = new MyNode<int>(3);
            MyNode<int> n4 = new MyNode<int>(4);
            Edge<int, int> e2 = new Edge<int, int>(8, n1, n2);
            Edge<int, int> e3 = new Edge<int, int>(8, n3, n4);

            //TODO: this will fail as is, fix it so that it checks that the components of each node match one-another
            Assert.IsTrue(e1.HasSameNodes(e2));
            Assert.IsTrue(e2.HasSameNodes(e1));
            Assert.IsFalse(e1.HasSameNodes(e3));
            Assert.IsFalse(e2.HasSameNodes(e3));
        }

        [TestMethod]
        public void Edge_Test_Equals_()
        {
            MyNode<int> n1 = new MyNode<int>(1);
            MyNode<int> n2 = new MyNode<int>(2);
            Edge<int, int> e1 = new Edge<int, int>(7, n1, n2);
            MyNode<int> n3 = new MyNode<int>(3);
            MyNode<int> n4 = new MyNode<int>(4);
            Edge<int, int> e2 = new Edge<int, int>(8, n1, n2);
            Edge<int, int> e3 = new Edge<int, int>(8, n3, n4);
            Edge<int, int> e4 = new Edge<int, int>(8, n1, n2);
            Edge<int, int> e5 = new Edge<int, int>(8, n2, n1);

            Assert.IsTrue(e2.Equals(e4));
            Assert.IsFalse(e1.Equals(e2));
            Assert.IsFalse(e1.Equals(e3));
            Assert.IsFalse(e3.Equals(e4));
            Assert.IsFalse(e4.Equals(e5));
        }

        [TestMethod]
        public void Edge_Test_CompareTo_()
        {
            MyNode<int> n1 = new MyNode<int>(1);
            MyNode<int> n2 = new MyNode<int>(2);
            Edge<int, int> e1 = new Edge<int, int>(7, n1, n2);
            MyNode<int> n3 = new MyNode<int>(3);
            MyNode<int> n4 = new MyNode<int>(4);
            Edge<int, int> e2 = new Edge<int, int>(8, n1, n2);
            Edge<int, int> e3 = new Edge<int, int>(9, n1, n2);
            Edge<int, int> e4 = new Edge<int, int>(8, n1, n2);
            Edge<int, int> e5 = new Edge<int, int>(7, n3, n4);
            Edge<int, int> e6 = new Edge<int, int>(8, n3, n4);
            Edge<int, int> e7 = new Edge<int, int>(9, n3, n4);

            Assert.AreEqual(e2.CompareTo(e1), 1);
            Assert.AreEqual(e2.CompareTo(e3), -1);
            Assert.AreEqual(e2.CompareTo(e4), 0);
            Assert.AreEqual(e2.CompareTo(e5), -2);
            Assert.AreEqual(e2.CompareTo(e6), -2);
            Assert.AreEqual(e2.CompareTo(e7), -2);


        }
    }
}
